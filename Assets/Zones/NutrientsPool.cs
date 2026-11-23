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
        nutrient.name = nutrientObj.name.Replace("(Clone)", "");
        nutrient.name = nutrient.name.TrimEnd();
        nutrient.SetActive(false);
        nutrientsPool.Add(nutrient);
      }
    }
  }

  public GameObject SpawnNutrient(Vector2 spawnPos, string nutrientName)
  {
    foreach (GameObject nutrient in nutrientsPool)
    {
      if(nutrient.name == nutrientName) continue;
      if(nutrient.activeInHierarchy) continue;
      
      nutrient.transform.position = spawnPos;
      nutrient.SetActive(true);
      return nutrient;
    }
    return null;
  }
  
}
