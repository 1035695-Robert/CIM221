using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    private PlayerControls _controls;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] Player_Model playerModel;
    [SerializeField] Player_View playerView;
    
    public Action<bool> PickupAction;
    public Action<Vector2> MoveAction;

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
        _controls.Enable();

        if (playerView != null)
        {
            MoveAction += playerView.Move;
            PickupAction += playerView.Pickup;
        }
    }

    private void Pickup(InputAction.CallbackContext obj)
    {
        if (!playerModel.isHolding)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);
            if (hit.collider != null)
            {
                playerModel.holdObject = hit.collider.gameObject;
                playerModel.isHolding = true;
                
                PickupAction?.Invoke(playerModel.isHolding);
            }
        }
        else
        {
            playerModel.isHolding = false;
            PickupAction?.Invoke(playerModel.isHolding);
            playerModel.holdObject = null;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = playerModel.moveValue * playerModel.moveSpeed;
    }

    public void MovementInput(InputAction.CallbackContext obj)
    {
        playerModel.moveValue = obj.ReadValue<Vector2>();
        MoveAction?.Invoke(playerModel.moveValue);
    }


    private void OnDisable()
    {
        _controls.Player.Movement.performed += MovementInput;
        _controls.Player.Movement.canceled += MovementInput;
        _controls.Disable();

        if (playerView != null)
        {
            MoveAction -= playerView.Move;
            PickupAction -= playerView.Pickup;
        }
    }
}