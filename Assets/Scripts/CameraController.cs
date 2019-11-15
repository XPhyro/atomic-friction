using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    private Transform target;
    
    private readonly Vector3 offset = new Vector3(0, 0, -10);

    private void LateUpdate()
    {
        if(!target)
        {
            target = NodeManager.MainMovingNode.transform;

            if(!target)
            {
                return;
            }
        }

        transform.position = target.position + offset;
    }
}