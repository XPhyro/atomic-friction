//#define WILL_DELAY_SIMULATION

using System.Collections;
using UnityEngine;

public class DelayedSimulationManager : MonoBehaviour
{
    private const float StartingDelay = 3f;
    
    private void Start()
    {
#if WILL_DELAY_SIMULATION
        NodeManager.MovingNodes.ForEach(x => x.AllowComputation());
        SimulationManager.Singleton.enabled = false;
        DelayedEnableSimulationManager();
#endif
    }
    
    private static IEnumerator DelayedEnableSimulationManager()
    {
        yield return new WaitForSecondsRealtime(StartingDelay);
        SimulationManager.Singleton.enabled = true;
    }
}
