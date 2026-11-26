using System;
using UnityEngine;

public class Player_Model : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private float _currentMoveSpeed;

    void Awake()
    {
        _currentMoveSpeed = _moveSpeed;
    }
    public float moveSpeed
    {
        get => _currentMoveSpeed;
        set => _currentMoveSpeed = value;
    }
    public void ResetMoveSpeed() => _currentMoveSpeed = _moveSpeed;
    
    public Vector2 moveValue { get; set; }
    public Vector2 lastMoveValue { get; set; }
    public GameObject holdObject { get; set; }
    public bool isHolding { get; set; }

    private Zone _currentZone;
    public Zone currentZone
    {
        get => _currentZone;
        set
        {
            _currentZone = value;
            if(value == null && _currentZone != null)
            {
                lastZone = _currentZone;
            }
            _currentZone = value;
        }
        
    }

    public Zone lastZone { get; set; }
    public bool isOnZone => currentZone != null;
    [SerializeField] private LayerMask _mask;
    public LayerMask layerMask
    {
        get => _mask;
        set => _mask = value;
    }
}