using System;
using Audio.Scripts;
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
    [SerializeField] public ZoneType zoneType;


    private void OnEnable()
    {
        onDelivery += DeliveredItem;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player_Model model))
            model.currentZone = this;
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
        NutrientsType type = nutrients.GetComponent<Nutrients>().Type;

        switch (type)
        {
            case NutrientsType.PlantNutrient:
                if (zoneType == ZoneType.FungalZone)
                {
                    Debug.Log("Host Nutrient");
                   
                    Hazards.OnHazardClear?.Invoke("pollution");
                    Sfx.OnSfxAudioEvent?.Invoke("clearHazard");

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