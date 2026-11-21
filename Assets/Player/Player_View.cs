using UnityEngine;
using System;

public class Player_View : MonoBehaviour
{
    [SerializeField] private Player_Model model;
    [SerializeField] private Animator _animator;

    public void Move(Vector2 moveValue)
    {
        if (moveValue.x != 0 || moveValue.y != 0)
            _animator.SetBool("isMoving", moveValue != Vector2.zero);

        _animator.SetFloat("moveX", moveValue.x);
        _animator.SetFloat("moveY", moveValue.y);
    }

    public void Pickup(bool state)
    {
        _animator.SetBool("isHolding", state);

        if (state && model.holdObject != null)
        {
            model.holdObject.transform.parent = transform;
            model.holdObject.transform.position = transform.position + transform.up;
        }
        else if (!state && model.holdObject != null)
        {
            model.holdObject.transform.SetParent(null);
        }
    }

    public void StopMove(Vector2 moveValue)
    {
        _animator.SetBool("isMoving", false);
        _animator.SetFloat("lastX", moveValue.x);
        _animator.SetFloat("lastY", moveValue.y);
    }
}