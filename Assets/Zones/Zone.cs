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
    public static Action<bool> isOnZone;
    public static Func<GameObject, bool> onDelivery;
    [SerializeField] private ZoneType zoneType;

    private void OnEnable()
    {
        onDelivery += DeliveredItem;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isOnZone?.Invoke(true);
            Debug.Log("entered Zone");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isOnZone?.Invoke(false);
            Debug.Log("exited Zone");
        }
    }

    private bool DeliveredItem(GameObject nutrients)
    {
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