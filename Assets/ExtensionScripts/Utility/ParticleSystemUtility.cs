using UnityEngine;

namespace Extension.Utility
{
    public class ParticleSystemUtility
    {
        public static void ScaleParticleSystem(ParticleSystem system, float multiplier)
        {
            var mainModule = system.main;
            mainModule.startSizeMultiplier *= multiplier;
            mainModule.startSpeedMultiplier *= multiplier;
            mainModule.startLifetimeMultiplier *= Mathf.Lerp(multiplier, 1, 0.5f);
        }

        public static void ScaleParticleSystemsUnder(GameObject holder, float multiplier)
        {
            var systems = holder.GetComponentsInChildren<ParticleSystem>();

            foreach(var system in systems)
            {
                var mainModule = system.main;
                mainModule.startSizeMultiplier *= multiplier;
                mainModule.startSpeedMultiplier *= multiplier;
                mainModule.startLifetimeMultiplier *= Mathf.Lerp(multiplier, 1, 0.5f);
            }
        }

        public static void ScaleParticleSystemsUnder(ParticleSystem[] systems, float multiplier)
        {
            foreach(var system in systems)
            {
                var mainModule = system.main;
                mainModule.startSizeMultiplier *= multiplier;
                mainModule.startSpeedMultiplier *= multiplier;
                mainModule.startLifetimeMultiplier *= Mathf.Lerp(multiplier, 1, 0.5f);
            }
        }
    }
}
