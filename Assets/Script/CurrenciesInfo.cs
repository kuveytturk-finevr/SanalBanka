using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrenciesInfo : MonoBehaviour
{
    public ApiManager apiManager;
    private bool isUpdateText = false;

    // Update is called once per frame
    void Update()
    {
        if (!String.IsNullOrEmpty(apiManager.APIResult) && !isUpdateText)
        {
            if (apiManager.AppKey == "currency")
            {
                Text textMeshPro = gameObject.GetComponent<Text>();
                if (apiManager.rootObjCurrency != null)
                {
                    if (apiManager.rootObjCurrency.value != null)
                    {
                        isUpdateText = true;
                        textMeshPro.text += "Döviz Kurları\n";
                        for (int i = 0; i < apiManager.rootObjCurrency.value.Count; i++)
                        {
                            string fxCode = apiManager.rootObjCurrency.value[i].fxCode;
                            if (fxCode.Equals("GBP") || fxCode.Equals("USD") || fxCode.Equals("EUR") || fxCode.Equals("AUS") || fxCode.Equals("KWD"))
                            {
                                textMeshPro.text += "\n" + apiManager.rootObjCurrency.value[i].fxCode + "   " +
                                  apiManager.rootObjCurrency.value[i].buyRate + "   " + apiManager.rootObjCurrency.value[i].sellRate;
                            }
                        }
                    }
                }
            }
        }
    }
}
