#define LIMIT_COMPUTATION
//#define LIMIT_MOVEMENT

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using LJ = PMaths.LennardJonesPotential;

public class MovingNode : Node
{
    private const float FullMinPosLimit = 0.005f;
    private const float ComputationMinPosLimit = 0.2f;//0.005f;
    private const float MovementMinPosLimit = 0.000f;
    private const float NoneMinPosLimit = 0.000f;
    private const float VelocityToPosLimitConversion = 0.001f;
    
    private const float TimeStep = 1e-6f;
    private const float PositionStep = 1e-6f;
    /// <summary>
    /// Scaling constant st. we can show the data better, while still computing accurately.
    /// </summary>
    public const float ScalingConst = 1e-9f;

    private enum LimitationMethod
    {
        Full, Computation, Movement, None
    }
    private enum InterpolationMethod
    {
        Movement, None
    }

    [SerializeField]
    private bool interpolateMovement;
    
    [SerializeField]
    [ReadOnly]
    private bool isStopped = true;
    [SerializeField]
    [ReadOnly]
    private bool canMove = false;

    [SerializeField]
    [ReadOnly]
    private float force;
    [SerializeField]
    [ReadOnly]
    private Vector2 nextPosition;
    [SerializeField]
    [ReadOnly]
    private Vector2[] recentPositions = new Vector2[2];
    [SerializeField]
    [ReadOnly]
    private float velocity;

    [SerializeField]
    [ReadOnly]
    private LimitationMethod limitationMethod;
    [SerializeField]
    [ReadOnly]
    private InterpolationMethod interpolationMethod;

    [SerializeField] 
    [ReadOnly] 
    private float minPosLimit = 0.005f;

    private float prevPos;
    
    private void Awake()
    {
        LJ.SetConstants(SimulationManager.K, SimulationManager.Epsilon, SimulationManager.R0);

        //TESTING
//        for(int i = 0; i < 100; i++)
//        {
//            Debug.Log(Maths.Differentiate(LJ.GetPotential, 
//                Math.Abs(0.001f * i) * PMaths.MToAngstrom * ScalingConst, PositionStep));
//        }
    }

    private void Start()
    {
        recentPositions[0] = recentPositions[1] = transform.position;
        
#if LIMIT_COMPUTATION
    #if LIMIT_MOVEMENT
        limitationMethod = LimitationMethod.Full;
    #else
        limitationMethod = LimitationMethod.Computation;
    #endif
#elif LIMIT_MOVEMENT
        limitationMethod = LimitationMethod.Movement;
#else
        limitationMethod = LimitationMethod.None;
#endif

        interpolationMethod = interpolateMovement ? InterpolationMethod.Movement : InterpolationMethod.None;

        switch(limitationMethod)
        {
            case LimitationMethod.Computation:
                minPosLimit = ComputationMinPosLimit;
                break;
            case LimitationMethod.None:
                minPosLimit = NoneMinPosLimit;
                break;
            default:
                minPosLimit = ComputationMinPosLimit;
                break;
        }
        
        SendUIProperties();
    }

    private void Update()
    {
#if LIMIT_COMPUTATION  
        if(isStopped)
        {
            return;
        }
#endif
        
        UpdateForce();
        UpdateNextPosition();

#if LIMIT_MOVEMENT
        canMove = true;
#endif
#if LIMIT_COMPUTATION
        isStopped = true;
#endif
    }

    private void FixedUpdate()
    {
#if LIMIT_MOVEMENT
        if(!canMove)
        {
            return;
        }
#endif
        
        Move();
    
#if LIMIT_MOVEMENT
        canMove = false;
#endif
    }

    private void LateUpdate()
    {
        ComputeCurrentVelocity();
        SendUIProperties();
    }

    public void AllowComputation()
    {
        isStopped = false;
    }
    
    private void UpdateForce()
    {
        var staticNodes = NodeManager.StaticNodes;
        var movPos = transform.position.x;
        
        var minPosLim = minPosLimit;
        
        if(limitationMethod == LimitationMethod.Movement)
        {
            minPosLim = MovementMinPosLimit + velocity * VelocityToPosLimitConversion;
        }
        else if(limitationMethod == LimitationMethod.Full)
        {
            minPosLim = FullMinPosLimit + velocity * VelocityToPosLimitConversion;
        }
        
        //compute LJ potentials list (in 1D)
        var diffUs = staticNodes
            .Select(node => node.transform.position.x)
            .Select(pos => Maths.Differentiate(
                LJ.GetPotential,
                Mathf.Clamp(
                    Math.Abs(pos - movPos), minPosLim, float.PositiveInfinity)
                * PMaths.MToAngstrom * ScalingConst,
                PositionStep));
        force = -diffUs.Sum();

#if UNITY_EDITOR
        if(force > 100)
        {
            Debug.Log(force);
        }
#endif
    }

    private void UpdateNextPosition()
    {
        var p0 = recentPositions[0];
        var p1 = recentPositions[1];
        nextPosition = new Vector2(force / Mass * PMaths.AngstromToM / ScalingConst - p1.x + 2 * p0.x , Maths.Avg(p0.y, p1.y));
    }

    private void ComputeCurrentVelocity()
    {
        var currentPos = transform.position.x;
        velocity = (currentPos - prevPos) / Time.fixedDeltaTime;
        prevPos = currentPos;
    }

    private void Move()
    {
        if(!interpolateMovement && nextPosition == recentPositions[1])
        {
            return;
        }
        
        recentPositions[0] = recentPositions[1];
        var newPos = recentPositions[1] = nextPosition;

        var t = transform;
        var pos = t.position;
        var delta = newPos - (Vector2)pos;

        pos += new Vector3(delta.x * Time.fixedDeltaTime, 0);
        t.position = pos;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void SendUIProperties()
    {
        var args = new object[]
        {
            force, transform.position.x, limitationMethod, interpolationMethod, velocity
        };
        var types = new[]
        {
            typeof(float), typeof(float), typeof(LimitationMethod), typeof(InterpolationMethod), typeof(float)
        };
        
        UIManager.Singleton.UpdateMovingNodeProps(args, types);
    }
}
