using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountSliderManager : MonoBehaviour
{
    public GameObject ValueText;
    public float Max = 200;
    // Start is called before the first frame update
    void Start()
    {
     //   ValueText = transform.Find("Value").gameObject;
     //   ValueText.GetComponent<Text>().text = this.GetComponent<Slider>().value.ToString() + " ₺";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetValue()
    {
       // ValueText.GetComponent<Text>().text = this.GetComponent<Slider>().value.ToString() + " ay";

    }

    public void SetMax()
    {
        this.GetComponent<Slider>().maxValue = Max * 2;
        this.GetComponent<Slider>().value = Max;
       // SetValue();
    }
}
