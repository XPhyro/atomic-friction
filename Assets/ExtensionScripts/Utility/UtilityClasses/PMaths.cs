using UnityEngine;

public class PMaths
{
    public const float MToAngstrom = 1e10f;
    public const float AngstromToM = 1e-10f;
    public const float EVToJ = 1.60218e-19f;
    public const float JToEV = 6.242e18f;
    
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
            
            Debug.Log($"Setting N={N}, Epsilon={epsilon}, K={k}, R0={r0}");
        }

        /// <summary>
        /// Computes the Lennard-Jones potential.
        /// </summary>
        /// <param name="r">r is in Angstrom</param>
        /// <returns>Returns the Lennard-Jones potential.</returns>
        public static float GetPotential(float r)
        {
            if(Mathf.Approximately(r, 0))
            {
                const float newR = 0.001f;

                if(r == 0)
                {
                    Debug.LogError("r cannot be 0.");
                    return 0;
                }
                
                Debug.LogError($"r cannot be 0, substituting r={newR}f instead.");
                r = newR;
            }
            if(r < 0)
            {
                Debug.LogError("r cannot be negative, substituting r=abs(r) instead.");
                r = Mathf.Abs(r);
            }

            return Epsilon * (Mathf.Pow(R0 / r, 2 * N) - 2 * Mathf.Pow(R0 / r, N));
        }
    }
}
