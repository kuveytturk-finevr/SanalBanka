using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public GameObject Value;
    public GameObject DecrementButton;
    public GameObject IncrementButton;
 public   int changeVal = 1;
    public string delimeter = "";
    public int MaxVal ;
    public int MinVal;
    // Start is called before the first frame update
    void Start()
    {
        SetValue(Int32.Parse(Value.GetComponent<Text>().text));
    }


    public void SetValue(int value)
    {
        Value.GetComponent<Text>().text = value.ToString() + " " + delimeter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Decrement()
    {
        if(Int32.Parse(Value.GetComponent<Text>().text.Split(' ')[0]) - changeVal>=MinVal)
        SetValue(Int32.Parse(Value.GetComponent<Text>().text.Split(' ')[0]) - changeVal);
    }

    public void İncrement()
    {
        if (Int32.Parse(Value.GetComponent<Text>().text.Split(' ')[0]) + changeVal <= MaxVal)
            SetValue(Int32.Parse(Value.GetComponent<Text>().text.Split(' ')[0]) + changeVal);
    }

    public float GetValue()
    {
        return float.Parse(Value.GetComponent<Text>().text.Split(' ')[0]);
    }
}
