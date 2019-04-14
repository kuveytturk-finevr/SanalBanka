using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{


    public GameObject HomeInfoPanel;
    public GameObject CreditRequestPanel;
    public GameObject CreditResponsePanel;
    int HouseCost;
    // Start is called before the first frame update
    void Start()
    {
        HomeInfoPanel = transform.GetChild(0).gameObject;
        CreditRequestPanel = transform.GetChild(1).gameObject;
        CreditResponsePanel = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        CreditResponsePanel.transform.Find("PaymentCount").GetComponent<Text>().text = CreditResponse.PaymentCount.ToString();
        CreditResponsePanel.transform.Find("PaymentDuration").GetComponent<Text>().text = CreditResponse.PaymentDuration.ToString();
    }

    void GetCreditRequestValues()
    {
        CreditRequest CR = new CreditRequest
        {
            TotalAmount = CreditRequestPanel.transform.Find("Amount").GetComponent<SliderManager>().GetValue().ToString(),
         PaymentDuration    = CreditRequestPanel.transform.Find("Months").GetComponent<SliderManager>().ToString()
        };

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
        CreditResponse CreditResponse = new CreditResponse {


        };//apiden çekilecek
        //FillCreditResponsePanel(CreditResponse);
        CreditResponsePanel.SetActive(true);
    }
    public void HideCreditResponsePanel()
    {
        CreditResponsePanel.SetActive(false);
    }


}
