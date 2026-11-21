using System.Linq;
using TMPro;
using UnityEngine;
using Audio.Scripts;


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
    public static UIPopupManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private readonly UICheckManager _uiCheck = new UICheckManager();
    [SerializeField] private GameObject popupUI;

    [SerializeField] private GameObject[] uiData;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void Start()
    { 
        uiData = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            uiData[i] = transform.GetChild(i).gameObject;
        }
    }

    private UIPopupData targetData;

    public bool OpenUI(string uiName)
    {
        if (CheckList(uiName)) return false;

        Debug.Log(uiName);
        GameObject targetObject = uiData.FirstOrDefault(ui => ui != null && ui.name == uiName);

        if (targetObject != null)
        {
            targetData = targetObject.GetComponent<UIPopupData>();
            titleText.text = targetData.Title;
            descriptionText.text = targetData.Description;
            popupUI.SetActive(true);
            
            UiAudio.OnUIAudioEvent?.Invoke("UI Menu Select");
            
            MarkAsOpened(uiName);
            return true;
        }
        Debug.LogError("something went wrong");
        return false;
    }


    private bool CheckList(string uiName)
    {
        switch (uiName)
        {
            case "PlantNutrients":
                return _uiCheck.IsPickupPlantNutrients;

            case "SoilNutrients":
                return _uiCheck.isPickupSoilNutrients;

            case "RootZone":
                Debug.Log(_uiCheck.IsInRootZone);
                return _uiCheck.IsInRootZone;

            case "FungalZone":
                return _uiCheck.IsInFungalHub;

            case "PollutionHazard":
                return _uiCheck.IsNearPollutionHazard;

            case "SoilHazard":
                return _uiCheck.IsNearSoilHazard;

            default:
                return false;
        }
    }

    private void MarkAsOpened(string uiName)
    {
        switch (uiName)
        {
            case "PlantNutrients":
                _uiCheck.IsPickupPlantNutrients = true;
                break;

            case "SoilNutrients":
                _uiCheck.isPickupSoilNutrients = true;
                break;

            case "RootZone":
                _uiCheck.IsInRootZone = true;
                break;

            case "FungalZone":
                _uiCheck.IsInFungalHub = true;
                break;

            case "PollutionHazard":
                _uiCheck.IsNearPollutionHazard = true;
                break;

            case "SoilHazard":
                _uiCheck.IsNearSoilHazard = true;
                break;
        }
    }

    public void CloseUi()
    {
        popupUI.SetActive(false);
       UiAudio.OnUIAudioEvent?.Invoke("UI Menu Exit");
    }
}