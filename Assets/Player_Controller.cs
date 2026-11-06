using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    private PlayerControls _controls;
    private Vector2 moveValue;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;


    [Header("Pickup")] [SerializeField] private GameObject holdObject;

    private void Awake()
    {
        _controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _controls.Player.Movement.performed += MovementInput;
        _controls.Player.Movement.canceled += MovementInput;
        _controls.Player.Pickup.performed += Pickup;
        _controls.Enable();
    }


    private void MovementInput(InputAction.CallbackContext obj)
    {
        moveValue = obj.ReadValue<Vector2>();
    }

    private void Update()
    {
        rb.linearVelocity = moveValue * moveSpeed;
    }

    private void Pickup(InputAction.CallbackContext obj)
    {
        Debug.Log("Pickup");
      if(holdObject ==null)
      {
          RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1);

          if (hit.collider != null)
          {
              holdObject = hit.collider.gameObject;
              holdObject.transform.SetParent(transform);
              holdObject.transform.localPosition = transform.up;
          }
      }
      else
      {
           holdObject.transform.SetParent(null);
           holdObject = null;
      }
    }

    private void OnDisable()
    {
        _controls.Player.Movement.performed += MovementInput;
        _controls.Player.Movement.canceled += MovementInput;
        _controls.Disable();
    }
}