using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditAPI : MonoBehaviour
{
    public ApiManager apiManager;
    private bool isUpdateText = false;

    // Start is called before the first frame update
    void ButonaBastirdigindaCagir()
    {
        apiManager.StartFinance("12", "10000");
    }

    // Update is called once per frame
    void Update()
    {
        if (!String.IsNullOrEmpty(apiManager.APIResult) && !isUpdateText)
        {
            if (apiManager.AppKey == "finance")
            {
                if (apiManager.rootObjFinance != null && apiManager.rootObjFinance.value != null)
                {
                    isUpdateText = true;
                    /* TODO: Yapmak istediğin şeyi burda yap

                    apiManager.rootObjFinance.value.monthlyProfitRate;
                    apiManager.rootObjFinance.value.fundingAmount;
                    apiManager.rootObjFinance.value.installmentCount;
                    apiManager.rootObjFinance.value.totalInstallmentAmount;
                    apiManager.rootObjFinance.value.totalProfitAmount;
                    */

                }
            }
        }
    }
}
