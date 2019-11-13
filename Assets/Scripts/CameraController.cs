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
            target = GameObject.FindGameObjectWithTag(TagHashes.MovingNode).transform;
        }

        transform.position = target.position + offset;
    }
}