using UnityEngine;

namespace Extension.Utility
{
    public class Vector3Utility
    {
        public static float SqrDistance(Vector3 lhs, Vector3 rhs)
        {
            return (lhs - rhs).sqrMagnitude;
        }
    }
}
