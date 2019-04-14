using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderValueManager : MonoBehaviour
{
    public string formatter;
    private string formatText = "{0} ";

    private TextMeshProUGUI tmproText;

    private void Start()
    {
        formatText = "{0} " + formatter;
        tmproText = GetComponent<TextMeshProUGUI>();

        GetComponentInParent<Slider>().onValueChanged.AddListener(HandleValueChanged);
    }

    private void HandleValueChanged(float value)
    {
        tmproText.text = string.Format(formatText, value);
    }
}

