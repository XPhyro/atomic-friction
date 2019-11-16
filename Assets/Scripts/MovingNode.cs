﻿#define LIMIT_COMPUTATION
//#define LIMIT_MOVEMENT

using System;
using System.Linq;
using UnityEngine;
using LJ = PMaths.LennardJonesPotential;

public class MovingNode : Node
{
    private const float TimeStep = 1e-6f;
    private const float PositionStep = 1e-6f;
    /// <summary>
    /// Scaling constant st. we can show the data better, while still computing accurately.
    /// </summary>
    public const float ScalingConst = 1e-9f;

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

    private void Awake()
    {
        LJ.SetConstants(SimulationManager.K, SimulationManager.Epsilon, SimulationManager.R0);        
    }

    private void Start()
    {
        recentPositions[0] = recentPositions[1] = transform.position;
        
        SendUIProps();
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
        SendUIProps();

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

    public void AllowComputation()
    {
        isStopped = false;
    }
    
    private void UpdateForce()
    {
        var staticNodes = NodeManager.StaticNodes;
        var movPos = transform.position.x;
        //compute LJ potentials list (where LJ potential is computed in 1D)
        var diffUs = staticNodes.Select(node => node.transform.position.x)
            .Select(pos => Maths.Differentiate(LJ.GetPotential, Math.Abs(pos - movPos) * PMaths.MToAngstrom * ScalingConst, PositionStep));
        force = -diffUs.Sum();
    }

    private void UpdateNextPosition()
    {
        var p0 = recentPositions[0];
        var p1 = recentPositions[1];
        nextPosition = new Vector2(force / Mass * PMaths.AngstromToM / ScalingConst - p1.x + 2 * p0.x , Maths.Avg(p0.y, p1.y));
    }

    private void Move()
    {
        if(!interpolateMovement && nextPosition == recentPositions[1])
        {
            return;
        }
        
        recentPositions[0] = recentPositions[1];
        var newPos = recentPositions[1] = nextPosition;

        var delta = newPos - (Vector2)transform.position;

        transform.position += new Vector3(delta.x * Time.fixedDeltaTime, 0);
    }

    private void SendUIProps()
    {
        var args = new object[]
        {
            force, transform.position.x
        };
        var types = new[]
        {
            typeof(float), typeof(float)
        };
        
        UIManager.Singleton.UpdateMovingNodeProps(args, types);
    }
}
