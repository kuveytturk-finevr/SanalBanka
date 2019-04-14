using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountInfo : MonoBehaviour
{
    public ApiManager apiManager;
    private bool isUpdateText = false;

    // Start is called before the first frame update
    void Start()
    {
        apiManager.StartAccount();
    }

    private void Update()
    {
        if (!String.IsNullOrEmpty(apiManager.APIResult) && !isUpdateText)
        {
            if (apiManager.AppKey == "account")
            {
                if (apiManager.rootObjAccount != null && apiManager.rootObjAccount.value != null)
                {
                    isUpdateText = true;
                    Debug.Log(apiManager.rootObjAccount.value[0].customerName);
                    Text textMeshPro = gameObject.GetComponent<Text>();
                    textMeshPro.text = "\n" + apiManager.rootObjAccount.value[0].customerName + "\n" +
                    "Iban : " + apiManager.rootObjAccount.value[0].iban + "\n" + "Bakiye : " + apiManager.rootObjAccount.value[0].avaibleBalance;
                    apiManager.StartCurrency();
                }
            }
        }
    }
}
