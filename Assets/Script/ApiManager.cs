using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;
using System.Threading;

public class ApiManager : MonoBehaviour
{
    const string Token_Client = "93dc8c5e1072b8417e23663463fb629071a86b0d76d8adc8b048f50cb2fb954d";
    const string Token_Authorization = "ac4d9d6da3f386375e11d29dae416a73c059bfa41acb3a04018040eec3943512";

    const string URL_Finance = "https://apitest.kuveytturk.com.tr/prep/v1/calculations/loan";

    //Suffix should be provided (e.g. 1)
    const string URL_Account = "https://apitest.kuveytturk.com.tr/prep/v1/accounts?suffix={0}&onlyOpen=true&onlyWithNoBalance=false&onlyCurrent=true&sharedWithMultiSignature=true";
    const string URL_Currency = "https://apitest.kuveytturk.com.tr/prep/v1/fx/rates";


    // FINANCE CALCULATION BEGIN
    #region FINANCE
    [System.Serializable]
    public class Installment
    {
        public int order;
        public double amount;
        public double principalAmount;
        public double profitAmount;
        public double bittAmount;
        public int rusfAmount;
        public double remainingPrincipalAmount;
    }

    [System.Serializable]
    public class Value_FINANCE
    {
        public double monthlyProfitRate;
        public int fundingAmount;
        public int installmentCount;
        public double totalInstallmentAmount;
        public double totalProfitAmount;
        public int totalRUSFAmount;
        public double totalBITTAmount;
        public List<Installment> installments;
    }

    [System.Serializable]
    public class RootObject_FINANCE
    {
        public Value_FINANCE value;
        public bool success;
        public List<object> results;
    }
    #endregion
    // FINANCE CALCULATION END

    // ACCOUNT BEGIN
    #region ACCOUNT

    [System.Serializable]
    public class Value_ACCOUNT
    {
        public string name;
        public int suffix;
        public double balance;
        public double avaibleBalance;
        public int fxId;
        public string iban;
        public string type;
        public string openDate;
        public string branchName;
        public int branchId;
        public double withHoldingAmount;
        public string customerName;
        public string maturityBeginDate;
        public string maturityEndDate;
        public string isActive;
        public string id;
    }

    [System.Serializable]
    public class RootObject_ACCOUNT
    {
        public List<Value_ACCOUNT> value;
        public bool success;
        public List<object> results;
    }
    #endregion
    // ACCOUNT END

    // CURRENCY BEGIN
    #region CURRENCY

    [System.Serializable]
    public class Value_CURRENCY
    {
        public string name;
        public string fxCode;
        public int fxId;
        public double buyRate;
        public double sellRate;
        public double parityBuyRate;
        public double paritySellRate;
    }

    [System.Serializable]
    public class RootObject_CURRENCY
    {
        public List<Value_CURRENCY> value;
        public bool success;
        public List<object> results;
    }
    #endregion
    // CURRENCY END



    #region APIRESULT_HANDLER
    private string _apiresult;

    public string APIResult
    {
        get { return _apiresult; }
        set { _apiresult = value; }
    }


    private string _templateFinance;

    public string TemplateFinance
    {
        get
        {
            return @"{{'contract': {{
                            'productCode': '{0}',
                            'installmentCount': {1},
                            'amount': {2},
                            'isCalculatedByInstallmentAmount': {3}
                          }}
                        }}"; ;
        }
        set { _templateFinance = value; }
    }




    private RootObject_FINANCE _robjFinance;

    public RootObject_FINANCE rootObjFinance
    {
        get { return _robjFinance; }
        set { _robjFinance = value; }
    }


    private RootObject_ACCOUNT _robjAccount;

    public RootObject_ACCOUNT rootObjAccount
    {
        get { return _robjAccount; }
        set { _robjAccount = value; }
    }


    private RootObject_CURRENCY _robjCurrency;

    public RootObject_CURRENCY rootObjCurrency
    {
        get { return _robjCurrency; }
        set { _robjCurrency = value; }
    }

    private string _app_key;

    public string AppKey
    {
        get { return _app_key; }
        set { _app_key = value; }
    }


    private bool responseReturned;

    public bool ResponseReturned
    {
        get { return responseReturned; }
        set { responseReturned = value; }
    }


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ResponseReturned = false;

        //ResetAPIConfig("account");
        //Debug.Log(String.Format(URL_Account, 1));
        //StartCoroutine(GET(String.Format(URL_Account, 1), false));


        //ResetAPIConfig("currency");
        //StartCoroutine(GET(URL_Currency, true));

        //ResetAPIConfig("finance");
        //StartCoroutine(POST(String.Format(TemplateFinance, "ARACTICARIYENI", "12", "10000", "false"), URL_Finance, true));


    }

    // Update is called once per frame
    void Update()
    {
        if (!String.IsNullOrEmpty(APIResult) && !ResponseReturned)
        {
            ResponseReturned = true;

            if (AppKey == "finance")
            {
                rootObjFinance = new RootObject_FINANCE();

                JsonUtility.FromJsonOverwrite(APIResult, rootObjFinance);
                Debug.Log(rootObjFinance.value.monthlyProfitRate);
                Debug.Log(rootObjFinance.value.installmentCount);
            }
            else if (AppKey == "account")
            {
                rootObjAccount = new RootObject_ACCOUNT();
                Debug.Log(APIResult);
                JsonUtility.FromJsonOverwrite(APIResult, rootObjAccount);
                Debug.Log(rootObjAccount.success);
            }
            else if (AppKey == "currency")
            {
                rootObjCurrency = new RootObject_CURRENCY();

                JsonUtility.FromJsonOverwrite(APIResult, rootObjCurrency);
            }


        }
    }

    public void StartAccount()
    {
        ResetAPIConfig("account");
        StartCoroutine(GET(String.Format(URL_Account, 1), false));
    }

    public void StartCurrency()
    {
        ResetAPIConfig("currency");
        StartCoroutine(GET(URL_Currency, true));
    }

    public void StartFinance(String vade, String tutar)
    {
        ResetAPIConfig("finance");
        StartCoroutine(POST(String.Format(TemplateFinance, "GMENKULKONUTYENI", vade, tutar, "false"), URL_Finance, true));
    }

    public void ResetAPIConfig(string key)
    {
        AppKey = key;
        ResponseReturned = false;
        APIResult = "";
        if (AppKey == "finance")
        {
            rootObjFinance = null;
        }
        else if (AppKey == "account")
        {
            rootObjAccount = null;
        }
        else if (AppKey == "currency")
        {
            rootObjCurrency = null;
        }
    }
    IEnumerator GET(string url, bool IsClientToken)
    {
        //UnityWebRequest request = new UnityWebRequest(url, "GET");
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", "Bearer " + (IsClientToken ? Token_Client : Token_Authorization));


        //UnityWebRequest www = UnityWebRequest.Get(url); -> OLD ONE
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            APIResult = request.downloadHandler.text;

            // Or retrieve results as binary data
            //byte[] results = request.downloadHandler.data;
        }

    }

    IEnumerator POST(string jsonBody, string Url, bool IsClientToken)
    {
        UnityWebRequest request = new UnityWebRequest(Url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", "Bearer " + (IsClientToken ? Token_Client : Token_Authorization));
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("post ok");
            APIResult = request.downloadHandler.text;
        }
    }
}
