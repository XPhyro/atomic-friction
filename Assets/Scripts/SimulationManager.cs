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

    [SerializeField]
    private float ComputationPeriod = 1f;

    private float timer = 0f;
    
    private void Start()
    {
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
        var nodes = NodeManager.MovingNodes;

        foreach (var node in nodes)
        {
            node.AllowComputation();
        }
    }
}
