using UnityEngine;

public enum NutrientsType
{
    SoilNutrient,
    PlantNutrient,
}

public class Nutrients : MonoBehaviour
{
    [SerializeField] private NutrientsType type;
    public NutrientsType Type => type;
}