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
}