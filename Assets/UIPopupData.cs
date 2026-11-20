using UnityEngine;

public class UIPopupData : MonoBehaviour
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    public string Title => title;
   public string Description => description;
}
