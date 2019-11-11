using UnityEngine;

public class PhysicsMaths
{
    public class LennardJonesPotential
    {
        private static float N;
        private static float Epsilon;
        private static float K;
        private static float R0;

        public static void SetConstants(float k, float epsilon, float r0)
        {
            Epsilon = epsilon;
            K = k;
            R0 = r0;
            N = r0 * Mathf.Sqrt(k / (2 * epsilon));
        }

        public static float GetPotential(float r)
        {
            return Epsilon * (Mathf.Pow(R0 / r, 2 * N) - 2 * Mathf.Pow(R0 / r, N));
        }
    }
}
