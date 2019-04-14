using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{

    public ApiManager apiManager;
    private bool isUpdateText = false;

   

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

                    CreditResponse CR = new CreditResponse {
                        Interest = (float)apiManager.rootObjFinance.value.monthlyProfitRate, //faiz oranı
                        InterestCost = (float)apiManager.rootObjFinance.value.totalInstallmentAmount-apiManager.rootObjFinance.value.fundingAmount,
                        NetCost = apiManager.rootObjFinance.value.fundingAmount, //çekmek istediğim
                        
                        PaymentDuration = apiManager.rootObjFinance.value.installmentCount, //kaç ay ödeceğim
                        WholeCreditCost = (float)apiManager.rootObjFinance.value.totalInstallmentAmount //ödeceğim para

                    };
                    FillCreditResponsePanel(CR);

                    HideCreditRequestPanel();
                    

                }
            }
        }
        else if(!isUpdateText)
        {
            Debug.Log("I'm still waiting");
        }
    }
    public GameObject HomeInfoPanel;
    public GameObject CreditRequestPanel;
    public GameObject CreditResponsePanel;
    int HouseCost;
    // Start is called before the first frame update
    void Start()
    {
        apiManager = GameObject.FindObjectOfType<ApiManager>();
        HomeInfoPanel = transform.GetChild(0).gameObject;
        CreditRequestPanel = transform.GetChild(1).gameObject;
        CreditResponsePanel = transform.GetChild(2).gameObject;
    }




   public void FillHomeInfoPanel(HomeInfo MyHomeInfo)
    {
        HomeInfoPanel.transform.Find("Panel1/TextContent/RoomCount").GetComponent<Text>().text = MyHomeInfo.RoomCount.ToString();
        HomeInfoPanel.transform.Find("Panel1/TextContent/BuildingAge").GetComponent<Text>().text = MyHomeInfo.BuildingAge.ToString();
        HomeInfoPanel.transform.Find("Panel1/TextContent/Floor").GetComponent<Text>().text = MyHomeInfo.Floor.ToString();
        HomeInfoPanel.transform.Find("Panel1/TextContent/Price").GetComponent<Text>().text = MyHomeInfo.Price.ToString() + " ₺";
        HouseCost = (int)MyHomeInfo.Price;
        ShowHomeInfoPanel();
    }



    public void FillCreditResponsePanel(CreditResponse CreditResponse)
    {
        CreditResponsePanel.transform.Find("WholeCreditCost").GetComponent<Text>().text = CreditResponse.WholeCreditCost.ToString();
        CreditResponsePanel.transform.Find("NetCost").GetComponent<Text>().text = CreditResponse.NetCost.ToString();
        CreditResponsePanel.transform.Find("InterestCost").GetComponent<Text>().text = CreditResponse.InterestCost.ToString();
        CreditResponsePanel.transform.Find("Interest").GetComponent<Text>().text = CreditResponse.Interest.ToString();
        CreditResponsePanel.transform.Find("PaymentDuration").GetComponent<Text>().text = CreditResponse.PaymentDuration.ToString();


        ShowCreditResponsePanel();
    }

   public void GetCreditRequestValues()
    {
        CreditRequest CR = new CreditRequest
        {
            TotalAmount = CreditRequestPanel.transform.Find("Amount").GetComponent<SliderManager>().GetValue().ToString(),
         PaymentDuration    = CreditRequestPanel.transform.Find("Months").GetComponent<SliderManager>().ToString()
        };
        isUpdateText = false;
        apiManager.StartFinance("12", "1000");//CR.PaymentDuration, CR.TotalAmount);
        //send webrequest
    }




    public void ShowHomeInfoPanel()
    {
        
        HomeInfoPanel.SetActive(true);
    }
    public void ExitFromHouseInfo()
    {
       
        HomeInfoPanel.SetActive(false);
    }
    public void QuitHouse()
    {

        Destroy(this);

    }

    public void ShowCreditRequestPanel()
    {

        CreditRequestPanel.transform.Find("Amount").GetComponent<SliderManager>().SetValue(HouseCost);
        CreditRequestPanel.transform.Find("Amount").GetComponent<SliderManager>().MaxVal = HouseCost;
        CreditRequestPanel.transform.Find("Months").GetComponent<SliderManager>().SetValue(12);
        ExitFromHouseInfo();
        CreditRequestPanel.SetActive(true);

    }
    public void HideCreditRequestPanel()
    {
        //GetCreditRequestValues();
        //SendApiCall();
        //ShowCreditResponsePanel();
        CreditRequestPanel.SetActive(false);
    }

    public void ShowCreditResponsePanel()
    {
        HideCreditRequestPanel();

        GetCreditRequestValues();
//apiden çekilecek
        //FillCreditResponsePanel(CreditResponse);
        CreditResponsePanel.SetActive(false);
    }
    public void HideCreditResponsePanel()
    {
        CreditResponsePanel.SetActive(false);
    }


}
