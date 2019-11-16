using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    /// <summary>
    /// Epsilon in eV.
    /// </summary>
    public const float Epsilon = 0.84f;
    /// <summary>
    /// R0 in Angstrom/*m*/.
    /// </summary>
    public const float R0 = 2.56f/*e-10f*/;
    /// <summary>
    /// Spring constant.
    /// </summary>
    public const float K = 1f;

    public static SimulationManager Singleton;
    
    [SerializeField]
    private float ComputationPeriod = 0.25f;

    [SerializeField]
    [ReadOnly]
    private float timer = 0f;
    
    private void Start()
    {
        if(Singleton)
        {
            Destroy(this);
        }
        else
        {
            Singleton = this;
        }
        
        NodeManager.Singleton.Initialise();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer < ComputationPeriod)
        {
            return;
        }
        
        OnComputationTimeReached();
        timer = 0;
    }

    private static void OnComputationTimeReached()
    {
        NodeManager.MovingNodes.ForEach(x => x.AllowComputation());
    }
}
