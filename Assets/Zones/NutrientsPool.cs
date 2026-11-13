using System.Collections.Generic;
using UnityEngine;



public class NutrientsPool : MonoBehaviour
{
  public static NutrientsPool instance;
  public GameObject[] nutrientPrefab;
  public int poolSize = 10;

  private List<GameObject> nutrientsPool;

  private void Awake()
  {
    
    instance = this;
      InitializeNutrientsPool(); 
  }

  private void InitializeNutrientsPool()
  {
    nutrientsPool = new List<GameObject>();
    foreach(var nutrientObj in  nutrientPrefab)
    {
      for (int i = 0; i < poolSize; i++)
      {
        GameObject nutrient = Instantiate(nutrientObj);
        nutrient.SetActive(false);
        nutrientsPool.Add(nutrient);
      }
    }
  }

  public GameObject GetPooledNutrient()
  {
    foreach ( var nutrient in  nutrientsPool)
    {
      if (!nutrient.activeInHierarchy)
      {
        return nutrient;
      }
    }

    return null;
  }
}
