using UnityEngine;

public class UIPopupData : MonoBehaviour
{
    [SerializeField] private string title;
   [TextArea]
    [SerializeField] private string description;
    public string Title => title;
   public string Description => description;
}
