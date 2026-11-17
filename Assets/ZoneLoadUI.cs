using System;
using UnityEngine;

public class ZoneLoadUI : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.TryGetComponent(out Player_Controller controller) && other.TryGetComponent(out Player_Model model))
         if (UIPopupManager.instance.OpenUI(model.currentZone.zoneType.ToString()))
            
            controller.SwitchtoUI();
   }
}
