using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditInfo : MonoBehaviour
{
    public ApiManager apiManager;

    // Update is called once per frame
    void Update()
    {
        if (!String.IsNullOrEmpty(apiManager.APIResult) && !apiManager.ResponseReturned)
        {
            if (apiManager.AppKey == "currency")
            {
                Text textMeshPro = gameObject.GetComponent<Text>();
                if (apiManager.rootObjCurrency != null)
                {
                    if (apiManager.rootObjCurrency.results != null)
                    {
                        textMeshPro.text = "Gelecegin Bankaciligina Hosgeldin \n" + apiManager.rootObjAccount.value.name + "\n" +
                            "Iban : " + apiManager.rootObjAccount.value.iban + "\n" + "Bakiye : " + apiManager.rootObjAccount.value.avaibleBalance;
                    }
                }
            }
        }
    }
}
