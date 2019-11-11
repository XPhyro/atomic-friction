using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Epsilon in eV.
    /// </summary>
    public const float Epsilon = 0.84f;
    /// <summary>
    /// R0 in m.
    /// </summary>
    public const float R0 = 2.56e-10f;
    /// <summary>
    /// Spring constant.
    /// </summary>
    public const float K = 1f;

    private void Start()
    {
        NodeManager.Singleton.Initialise();
    }
}
