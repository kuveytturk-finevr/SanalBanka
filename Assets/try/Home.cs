using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality;
using UnityEngine.Experimental.UIElements;
using Microsoft.MixedReality.Toolkit.Input;

public class HomeInfo
{

    public  int RoomCount;
    public int BuildingAge;
    public int Floor;
    public float Price;
    public HomeInfo() { }
    public HomeInfo(int roomCount, int age, int floor, float cost) {
        RoomCount = roomCount;
        BuildingAge = age;
        Floor = floor;
        Price = cost;
    }
}

public class CreditRequest
{

    public string TotalAmount;
    public string PaymentDuration;

    public CreditRequest() { }
    public CreditRequest(string totalAmount,string paymentDuration) {
        TotalAmount = totalAmount;
        PaymentDuration = paymentDuration;
    }
}

public class CreditResponse
{
    public float WholeCreditCost;
    public float NetCost;
    public float InterestCost;
    public float Interest;
    public int PaymentCount;
    public int PaymentDuration;
}



public class Home : MonoBehaviour, IMixedRealityFocusHandler, IMixedRealityPointerHandler
{
    public Camera sceneCamera;
    [SerializeField]
    private MixedRealityInputAction tapAction = MixedRealityInputAction.None;

    [SerializeField]
    private Color color_IdleState =  Color.black;

    [SerializeField]
    private Color color_OnHover = Color.white;

    [SerializeField]
    private Color color_OnSelect = Color.blue;

    private Material material;

    bool PanelOn=false;

    public HomeInfo MyHomeInfo;

   public GameObject HomeInfoPanel;
   public GameObject CreditRequestPanel;
   public GameObject CreditResponsePanel;
    public GameObject PanelKabuk;
    public Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        HomeInfoPanel = PanelKabuk.transform.GetChild(0).gameObject;
        CreditRequestPanel = PanelKabuk.transform.GetChild(1).gameObject;
        CreditResponsePanel = PanelKabuk.transform.GetChild(2).gameObject;

        material = GetComponent<Renderer>().material;
        material.color = Color.green;
        color_IdleState = Color.green;
        defaultColor = gameObject.GetComponent<MeshRenderer>().material.color;
        MyHomeInfo = new HomeInfo(1, 2, 3, 100000);
        //FillHomeInfoPanel();
        //ShowHomeInfoPanel();
   }

    // Update is called once per frame
    void Update()
    {
 
    }

   // void FillHomeInfoPanel()
   // {
   //     HomeInfoPanel.transform.Find("Panel1/TextContent/RoomCount").GetComponent<Text>().text = MyHomeInfo.RoomCount.ToString();
   //     HomeInfoPanel.transform.Find("Panel1/TextContent/BuildingAge").GetComponent<Text>().text = MyHomeInfo.BuildingAge.ToString();
   //     HomeInfoPanel.transform.Find("Panel1/TextContent/Floor").GetComponent<Text>().text = MyHomeInfo.Floor.ToString();
   //     HomeInfoPanel.transform.Find("Panel1/TextContent/Price").GetComponent<Text>().text = MyHomeInfo.Price.ToString() + " ₺";
   // }

   // void GetCreditRequestValues()
   // {
   //     CreditRequest CR = new CreditRequest
   //     {
   //         PaymentDuration = CreditRequestPanel.transform.Find("").GetComponent<Text>().text,
   //         TotalAmount = CreditRequestPanel.transform.Find("").GetComponent<Text>().text
   //     };

   //     //send webrequest
   // }



   // void FillCreditResponsePanel(CreditResponse CreditResponse)
   // {
   //     CreditResponsePanel.transform.Find("WholeCreditCost").GetComponent<Text>().text = CreditResponse.WholeCreditCost.ToString();
   //     CreditResponsePanel.transform.Find("NetCost").GetComponent<Text>().text = CreditResponse.NetCost.ToString();
   //     CreditResponsePanel.transform.Find("InterestCost").GetComponent<Text>().text = CreditResponse.InterestCost.ToString();
   //     CreditResponsePanel.transform.Find("Interest").GetComponent<Text>().text = CreditResponse.Interest.ToString();
   //     CreditResponsePanel.transform.Find("PaymentCount").GetComponent<Text>().text = CreditResponse.PaymentCount.ToString();
   //     CreditResponsePanel.transform.Find("PaymentDuration").GetComponent<Text>().text = CreditResponse.PaymentDuration.ToString();
   // }
   // void ShowHomeInfoPanel()
   // {
   //     FillHomeInfoPanel();
   //     HomeInfoPanel.SetActive(true);
   // }
   // public void ExitFromHouseInfo()
   // {
   //     HomeInfoPanel.SetActive(false);
   // }


   //public void ShowCreditRequestPanel()
   // {
   //     ExitFromHouseInfo();
   //     CreditRequestPanel.SetActive(true);

   // }
   // public void HideCreditRequestPanel()
   // {
   //     //GetCreditRequestValues();
   //     //SendApiCall();
   //     //ShowCreditResponsePanel();
   //     CreditRequestPanel.SetActive(false);
   // }

   //public void ShowCreditResponsePanel()
   // {
   //     HideCreditRequestPanel();
   //     CreditResponse CreditResponse = new CreditResponse();//apiden çekilecek
   //     //FillCreditResponsePanel(CreditResponse);
   //     CreditResponsePanel.SetActive(true);
   // }
   // public void HideCreditResponsePanel()
   // {
   //     CreditResponsePanel.SetActive(false);
   //  }

 







    void IMixedRealityFocusHandler.OnFocusEnter(FocusEventData eventData)
    {
        Debug.Log("focus enter");
        material.color = color_OnHover;

        Debug.Log(HomeInfoPanel.transform.parent.gameObject.name);
        if (!PanelOn) {

           GameObject GO =  Instantiate(PanelKabuk, sceneCamera.transform.GetChild(0).position, sceneCamera.transform.GetChild(0).rotation);

            GO.GetComponent<PanelManager>().FillHomeInfoPanel(MyHomeInfo);
            PanelOn = true;
        }
        Debug.Log("Home info moved");
    }

    void IMixedRealityFocusHandler.OnFocusExit(FocusEventData eventData)
    {
        material.color = color_IdleState;
    }

    void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData)
    {
        if (eventData.MixedRealityInputAction == tapAction)
        {
            material.color = color_OnHover;
        }
    }

    void IMixedRealityPointerHandler.OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (eventData.MixedRealityInputAction == tapAction)
        {
            material.color = color_OnSelect;
        }
    }

    void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData) { }
}
