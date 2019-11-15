using System;
using UnityEngine;

public class StaticNode : Node
{
    [SerializeField] 
    [ReadOnly]
    private float distanceToMainMovingNode = 0f;

    private Transform mn;
    
    private void Update()
    {
        if(!mn)
        {
            mn = NodeManager.MainMovingNode.transform;

            if(!mn)
            {
                return;
            }
        }

        distanceToMainMovingNode = (mn.position.x - transform.position.x) * PMaths.MToAngstrom * MovingNode.ScalingConst;
    }
}
