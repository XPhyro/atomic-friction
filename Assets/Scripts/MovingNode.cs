using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LJ = PhysicsMaths.LennardJonesPotential;

public class MovingNode : Node
{
    private const float TimeStep = 1e-6f;
    private const float PositionStep = 1e-6f;

    private bool isStopped = true;    
    
    private readonly Vector2[] recentPositions = new Vector2[2];
    
    [SerializeField]
    [ReadOnly]
    private float force;
    [SerializeField]
    [ReadOnly]
    private Vector2 nextPosition;

    private void Awake()
    {
        LJ.SetConstants(SimulationManager.K, SimulationManager.Epsilon, SimulationManager.R0);        
    }

    private void Start()
    {
        recentPositions[0] = recentPositions[1] = transform.position;
    }

    private void Update()
    {
        if(isStopped)
        {
            return;
        }
        
        UpdateForce();
        UpdateNextPosition();

        isStopped = true;
    }

    private void FixedUpdate()
    {
        Debug.LogWarning("Not yet implemented.");
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
            .Select(pos => Maths.Differentiate(LJ.GetPotential, pos - movPos, PositionStep));
        force = -diffUs.Sum();
    }

    private void UpdateNextPosition()
    {
        var p0 = recentPositions[0];
        var p1 = recentPositions[1];
        nextPosition = new Vector2(force / Mass - p1.x + 2 * p0.x, Maths.Avg(p0.y, p1.y));
    }
}
