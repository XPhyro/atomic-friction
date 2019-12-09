using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LJ = PMaths.LennardJonesPotential;

public class UIManager : MonoBehaviour
{
    private const float MaxExpectedForce = 2f;
    private const float MaxExpectedPos = 4000f;
    private const float MaxExpectedVelocity = 4000f;
    
    public static UIManager Singleton;
    
    [SerializeField]
    private TextMeshProUGUI forceText;
    [SerializeField]
    private TextMeshProUGUI posText;
    [SerializeField]
    private TextMeshProUGUI limitText;
    [SerializeField]
    private TextMeshProUGUI interpolationText;
    [SerializeField]
    private TextMeshProUGUI velocityText;
    [SerializeField]
    private TextMeshProUGUI staticNodeCountText;

    [SerializeField]
    private TextMeshProUGUI[] valTexts;
    
    [SerializeField]
    private TextMeshProUGUI constantsText;
    [SerializeField]
    private TextMeshProUGUI forceDisclaimerText;

    [SerializeField]
    private Image negForceImage;
    [SerializeField]
    private Image posForceImage;
    [SerializeField]
    private Image negPosImage;
    [SerializeField]
    private Image posPosImage;
    [SerializeField]
    private Image negVelImage;
    [SerializeField]
    private Image posVelImage;
    
    [SerializeField]
    private Image[][] negPosImages;

    private readonly float[] maxExpectedValues = 
    {
        MaxExpectedForce,
        MaxExpectedPos,
        MaxExpectedVelocity
    };

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
        
        forceText.text = "Force* (eV/A)";
        posText.text = "Position (A)";
        velocityText.text = "Velocity (A/s)";
        limitText.text = "Limiting";
        interpolationText.text = "Interpolation";
        staticNodeCountText.text = "Static node count";
        valTexts[5].text = NodeManager.StaticNodeCount.ToString();

        constantsText.text = $"N={LJ.N}, \\epsilon={LJ.Epsilon} eV, K={LJ.K} eV/A^2, R_0={LJ.R0} A";
        forceDisclaimerText.text = "*Force is shown only when it is computed for all nodes. " +
                              "Closer nodes may be updated faster than shown for some " +
                              "limiting/interpolation mode combinations.";
        
        negPosImages = new[]
        {
            new[] {negForceImage, posForceImage},
            new[] {negPosImage, posPosImage},
            new[] {negVelImage, posVelImage}
        };
    }

    public void UpdateMovingNodeProps(object[] args, Type[] types)
    {
        if(args.Length != types.Length)
        {
            return;
        }

        var s = new string[args.Length];

        var props = new float[3];
        
        for(var i = 0; i < args.Length; i++)
        {
            try
            {
                var obj = Convert.ChangeType(args[i], types[i]);
                s[i] = obj.ToString();

                if(i <= 2)
                {
                    props[i] = (float)obj;
                }
            }
            catch(Exception e)
            {
                Debug.LogError($"{args[i]}, {types[i]}");
                throw;
            }
        }

        for(var i = 0; i < s.Length; i++)
        {
            valTexts[i].text = s[i];
        }

        for(var i = 0; i < props.Length; i++)
        {
            var prop = props[i];
            var index = prop >= 0 ? 1 : 0;

            for(var j = 0; j < negPosImages.Length; j++)
            {
                var image = negPosImages[i][index];
                var fill = Mathf.Abs(prop / maxExpectedValues[i]);
                image.fillAmount = index == j ? fill : 0;
                image.color = Color.Lerp(Color.green, Color.red, fill);
            }
        }
    }
}
