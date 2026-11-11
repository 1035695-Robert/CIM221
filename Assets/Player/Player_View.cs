using UnityEngine;
using System;

public class Player_View : MonoBehaviour
{
 [SerializeField] private Player_Model model;
 [SerializeField] private Animator _animator;

 public void Move(Vector2 moveValue)
 {
  _animator.SetFloat("moveX", moveValue.x);
  _animator.SetFloat("moveY", moveValue.y);
 }

 public void Pickup(bool state)
 {  
  _animator.SetBool("isHolding", state);

  if (state & model.holdObject != null)
  {
   model.holdObject.transform.SetParent(transform);
   model.holdObject.transform.localPosition = Vector3.up;
  }
  else if (!state & model.holdObject != null)
  {
   model.holdObject.transform.SetParent(null);
  }
 }
}
