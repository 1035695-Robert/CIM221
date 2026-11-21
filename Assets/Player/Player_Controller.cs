using System;
using Audio.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    private PlayerControls _controls;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] Player_Model playerModel;
    [SerializeField] Player_View playerView;

    private Action<bool> PickupAction;
    private Action<Vector2> MoveActionPerformed;
    private Action<Vector2> _moveActionCanceled;


    private void Awake()
    {
        _controls = new PlayerControls();
        if (playerModel == null) playerModel = GetComponent<Player_Model>();
    }

    private void OnEnable()
    {
        _controls.Player.Movement.performed += MovementInput;
        _controls.Player.Movement.canceled += MovementInput;
        _controls.Player.Pickup.performed += Pickup;
        _controls.UI.Close.performed += CloseUI;
        _controls.Player.Enable();

        if (playerView != null)
        {
            _moveActionCanceled += playerView.StopMove;
            MoveActionPerformed += playerView.Move;
            PickupAction += playerView.Pickup;
        }
    }


    private void Pickup(InputAction.CallbackContext obj)
    {
        if (!obj.performed) return;
        if (!playerModel.isHolding)
        {
            Vector2 rayDirection = playerModel.lastMoveValue != Vector2.zero
                ? playerModel.lastMoveValue
                : Vector2.zero;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 1f, playerModel.layerMask);
            if (hit.collider != null)
            {
                Debug.Log(playerModel.moveValue);
                playerModel.holdObject = hit.collider.gameObject;

                if (UIPopupManager.Instance.OpenUI(playerModel.holdObject.name)) SwitchtoUI();
                playerModel.isHolding = true;

                PickupAction?.Invoke(playerModel.isHolding);
            }
        }
        else if (playerModel.isOnZone)
        {
            if (playerModel.currentZone.DeliveredItem(playerModel.holdObject))
            {
                PickupAction?.Invoke(playerModel.isHolding = false);
                PlayerAudio.OnPlayerAudioEvent?.Invoke("happy");
                playerModel.holdObject.SetActive(false);
                NutrientsPool.instance.spawnNutrient(playerModel.currentZone.transform.position,
                    playerModel.holdObject.name);
                playerModel.holdObject = null;
            }
        }
    }

    public void SwitchtoUI()
    {
        Debug.Log("Switch to UI");
        _controls.Player.Disable();
        _controls.UI.Enable();
    }

    private void CloseUI(InputAction.CallbackContext obj)
    {
        UIPopupManager.Instance.CloseUi();
        _controls.Player.Enable();
        _controls.UI.Disable();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + playerModel.moveValue * (playerModel.moveSpeed * Time.fixedDeltaTime));
    }

    public void MovementInput(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            Vector2 input = obj.ReadValue<Vector2>();
            playerModel.moveValue = input;
            if (input != Vector2.zero)
            {
                playerModel.lastMoveValue = input;
            }

            MoveActionPerformed?.Invoke(playerModel.moveValue);
        }

        if (obj.canceled)
        {
            playerModel.moveValue = Vector2.zero;
            _moveActionCanceled?.Invoke(playerModel.lastMoveValue);
        }
    }


    private void OnDisable()
    {
        _controls.Player.Movement.performed -= MovementInput;
        _controls.Player.Movement.canceled -= MovementInput;
        _controls.Player.Pickup.performed -= Pickup;

        _controls.Disable();

        if (playerView != null)
        {
            MoveActionPerformed -= playerView.Move;
            PickupAction -= playerView.Pickup;
        }
    }
}