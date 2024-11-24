using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private static readonly int StrIsRunning = Animator.StringToHash("isRunning");
    
    // 基本属性
    public float moveSpeed = 1.0f;
    
    // 持有的枪
    public GameObject[] guns;
    private int _selectedGunIndex = 0;
    
    [CanBeNull] private GameObject CurrentGun => _selectedGunIndex < guns.Length ? guns[_selectedGunIndex] : null;

    private Transform _transform;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 _inputMovement;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (guns.Length != 0)
        {
            for(int i = 0; i < guns.Length; i++)
                guns[i].SetActive(i == _selectedGunIndex);
        }
    }

    private void OnLook(InputValue value)
    {
        if (!Camera.main)
            return;
        
        var mousePosition = value.Get<Vector2>();
        var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var lookDirection = worldPosition - _transform.position;

        if (lookDirection.x == 0) 
            return;
        
        lookDirection.z = 0;
        DoLook(worldPosition, lookDirection.normalized);
    }

    private void DoLook(Vector3 position, Vector3 direction)
    {
        _spriteRenderer.flipX = direction.x < 0;
        CurrentGun?.GetComponent<Gun>().PointTo(direction);
    }

    private void OnMove(InputValue value)
    {
        _inputMovement = value.Get<Vector2>().normalized;
        _animator.SetBool(StrIsRunning, _inputMovement != Vector2.zero);
    }

    private void OnSwitchNextGun()
    {
        if (guns.Length == 0)
            return;
        
        CurrentGun?.SetActive(false);
        if (++_selectedGunIndex >= guns.Length)
        {
            _selectedGunIndex = 0;
        }
        
        CurrentGun?.SetActive(true);
    }

    private void OnSwitchLastGun()
    {
        if (guns.Length == 0)
            return;
        
        CurrentGun?.SetActive(false);
        if (--_selectedGunIndex < 0)
        {
            _selectedGunIndex = Math.Max(0, guns.Length - 1);
        }
        
        CurrentGun?.SetActive(true);
    }

    private void OnFire(InputValue value)
    {
        CurrentGun?.GetComponent<Gun>().Fire(value.isPressed);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_inputMovement * moveSpeed);
    }
}
