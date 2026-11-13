using System;
using UnityEngine;

public enum ZoneType
{
    None,
    RootZone,
    FungalZone
}


public class Zone : MonoBehaviour
{
    public static Func<GameObject, bool> onDelivery;
    [SerializeField] private ZoneType zoneType;

    private void OnEnable()
    {
        onDelivery += DeliveredItem;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player_Model model))
        {
            model.currentZone = this;
            Debug.Log("entered Zone");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player_Model model) && model.currentZone == this)
        {
            model.currentZone = null;
            Debug.Log("exited Zone");
        }
    }

    public bool DeliveredItem(GameObject nutrients)
    {
        Debug.Log("Delivered Item?");
        NutrientsType type = nutrients.GetComponent<Nutrients>().nutrientsType;
        switch (type)
        {
            case NutrientsType.HostPlantNutrient:
                if (zoneType == ZoneType.FungalZone)
                {
                    Debug.Log("Host Nutrient");
                    return true;
                }
                return false;
            
            case NutrientsType.SoilNutrient:
                if (zoneType == ZoneType.RootZone)
                {
                    Debug.Log("Soil Nutrient");
                    return true;
                }
                return false;
        }

        return false;
    }
}