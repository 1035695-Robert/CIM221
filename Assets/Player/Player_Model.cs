using System;
using UnityEngine;

public class Player_Model : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    public float moveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }
    public Vector2 moveValue { get; set; }
    [Header("Pickup")] 
    public GameObject holdObject { get; set; }
    public bool isHolding { get; set; }
    public Zone currentZone { get; set; }
    public bool isOnZone => currentZone != null;
    [SerializeField] private LayerMask _mask;
    public LayerMask layerMask
    {
        get => _mask;
        set => _mask = value;
    }
}