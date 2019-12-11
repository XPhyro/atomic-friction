using System;
using UnityEngine;
using UnityEngine.Serialization;

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

    public const float FixedTimeStep = 0.002f;

    public static SimulationManager Singleton;
    
    [SerializeField]
    [FormerlySerializedAs("ComputationPeriod")]
    [Tooltip("Computation period for ALL NODES. Depending on the selected interpolation mode, the " +
             "closer nodes may be computed with a lower period.")]
    private float computationPeriod = 1f;

    [SerializeField]
    [ReadOnly]
    private float timer = 0f;

    private void Awake()
    {
        Time.fixedDeltaTime = FixedTimeStep;
    }

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
        if(timer < computationPeriod)
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
