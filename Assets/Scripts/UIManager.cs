using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton;
    
    [SerializeField]
    private TextMeshProUGUI forceText;
    [SerializeField]
    private TextMeshProUGUI forceValText;
    [SerializeField]
    private TextMeshProUGUI posText;
    [SerializeField]
    private TextMeshProUGUI posValText;
    [SerializeField]
    private TextMeshProUGUI limitText;
    [SerializeField]
    private TextMeshProUGUI limitValText;
    [SerializeField]
    private TextMeshProUGUI interpolationText;
    [SerializeField]
    private TextMeshProUGUI interpolationValText;
    [SerializeField]
    private TextMeshProUGUI velocityText;
    [SerializeField]
    private TextMeshProUGUI velocityValText;

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
        
        forceText.text = "Force* (eV/A)=";
        posText.text = "Position (A)=";
        limitText.text = "Limiting:";
        interpolationText.text = "Interpolation:";
        velocityText.text = "Vel. (A/s)=";
    }

    public void UpdateMovingNodeProps(object[] args, Type[] types)
    {
        if(args.Count() != types.Length)
        {
            return;
        }

        var s = new string[args.Length];
        
        for(int i = 0; i < args.Length; i++)
        {
            try
            {
                s[i] = Convert.ChangeType(args[i], types[i]).ToString();
            }
            catch(Exception e)
            {
                Debug.LogError($"{args[i]}, {types[i]}");
                throw;
            }
        }

        forceValText.text = s[0];
        posValText.text = s[1];
        limitValText.text = s[2];
        interpolationValText.text = s[3];
        velocityValText.text = s[4];
    }
}
