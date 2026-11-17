using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;


public class UICheckManager
{
    public bool IsPickupPlantNutrients { set; get; }
    public bool isPickupSoilNutrients { set; get; }
    public bool IsInRootZone { set; get; }
    public bool IsInFungalHub { set; get; }
    public bool IsNearPollutionHazard { set; get; }
    public bool IsNearSoilHazard { set; get; }
}

public class UIPopupManager : MonoBehaviour
{
    public static UIPopupManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    UICheckManager uiCheck = new UICheckManager();
    [SerializeField] private Transform uiCanvas;
    [SerializeField] private GameObject[] uiObjects;

    private void Start()
    {
        uiObjects = new GameObject[uiCanvas.childCount];
        for (int i = 0; i < uiCanvas.childCount; i++)
        {
            uiObjects[i] = uiCanvas.GetChild(i).gameObject;
        }
    }

    private GameObject targetUi;

    public bool OpenUI(string uiName)
    {
        if (CheckList(uiName)) return false;

        Debug.Log(uiName);
        targetUi = uiObjects.FirstOrDefault(ui => ui != null && ui.name == uiName + " UI");

        if (targetUi != null)
        {
            Debug.Log(targetUi.name);
            targetUi.SetActive(true);
            MarkAsOpened(uiName);
            return true;
        }

        return false;
    }


    private bool CheckList(string uiName)
    {
        switch (uiName)
        {
            case "PlantNutrients":
                return uiCheck.IsPickupPlantNutrients;

            case "SoilNutrients":
                return uiCheck.isPickupSoilNutrients;

            case "RootZone":
                Debug.Log(uiCheck.IsInRootZone);
                return uiCheck.IsInRootZone;

            case "FungalZone":
                return uiCheck.IsInFungalHub;

            case "PollutionHazard":
                return uiCheck.IsNearPollutionHazard;

            case "SoilHazard":
                return uiCheck.IsNearSoilHazard;

            default:
                return false;
        }
    }

    private void MarkAsOpened(string uiName)
    {
        switch (uiName)
        {
            case "PlantNutrients":
                uiCheck.IsPickupPlantNutrients = true;
                break;

            case "SoilNutrients":
                uiCheck.isPickupSoilNutrients = true;
                break;

            case "RootZone":
                uiCheck.IsInRootZone = true;
                break;

            case "FungalZone":
                uiCheck.IsInFungalHub = true;
                break;

            case "PollutionHazard":
                uiCheck.IsNearPollutionHazard = true;
                break;

            case "SoilHazard":
                uiCheck.IsNearSoilHazard = true;
                break;
        }
    }

    public void CloseUi()
    {
        targetUi.SetActive(false);
    }
}