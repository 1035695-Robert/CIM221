using UnityEngine;
using System;
using Audio.Scripts;

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
        CheckNutrients(state);

        if (state && model.holdObject != null)
        {
            model.holdObject.transform.parent = transform;
            model.holdObject.transform.position = transform.position;
            model.holdObject.SetActive(false);
            Sfx.OnSfxAudioEvent?.Invoke("pickup");
        }
        else if (!state && model.holdObject != null)
        {
            Sfx.OnSfxAudioEvent?.Invoke("drop");
            model.holdObject.transform.SetParent(null);
        }
    }

    private void CheckNutrients(bool state)
    {
        Nutrients nutrients = model.holdObject.GetComponent<Nutrients>();
        switch (nutrients.Type)
        {
            case NutrientsType.PlantNutrient:
                _animator.SetBool("isPlantHold", state);
                break;
            case NutrientsType.SoilNutrient:
                _animator.SetBool("isSoilHold", state);
                break;
        }
    }

    public void StopMove(Vector2 moveValue)
    {
        _animator.SetBool("isMoving", false);
        _animator.SetFloat("lastX", moveValue.x);
        _animator.SetFloat("lastY", moveValue.y);
    }

    public void HurtAnim()
    {
        _animator.SetTrigger("isHurt");
    }
}