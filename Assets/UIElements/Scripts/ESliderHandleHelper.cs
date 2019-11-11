using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ESliderHandleHelper : MonoBehaviour
{
    [SerializeField]
    private uint decimalDigits;

    [SerializeField]
    private Slider slider;

    private TextMeshProUGUI text;

    private string formatting = "";

    private void Awake()
    {
        SetFormatting();
    }

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

        OnSliderValueChanged();
    }

    private void SetFormatting()
    {
        var digits = decimalDigits;
        var formatting = this.formatting = "";

        if(digits == 0)
        {
            formatting = "0";
        }
        else
        {
            formatting = "0.";
            for(int i = 0; i < digits; i++)
            {
                formatting += '0';
            }
        }

        this.formatting += formatting;
    }

    private void OnSliderValueChanged()
    {
        var value = slider.value;

        var str = value.ToString(formatting);

        text.text = str;
    }
}
