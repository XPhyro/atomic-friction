using System;
using UnityEngine;

public class MovingNode : Node
{
    private void Update()
    {
        UpdateForce();
    }

    private void FixedUpdate()
    {
        Debug.LogWarning("Not yet implemented.");
    }

    private void UpdateForce()
    {
        
    }
}
