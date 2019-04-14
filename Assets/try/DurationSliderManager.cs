using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurationSliderManager : MonoBehaviour
{

    public GameObject ValueText;
    // Start is called before the first frame update
    void Start()
    {
        ValueText = transform.Find("Value").gameObject;

     //   ValueText.GetComponent<Text>().text = this.GetComponent<Slider>().value.ToString() + " ay";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void SetValue()
    {
        ValueText.GetComponent<Text>().text = this.GetComponent<Slider>().value.ToString() + " ay";

    }
}
