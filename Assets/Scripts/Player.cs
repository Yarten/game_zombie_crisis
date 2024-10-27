using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    public float moveSpeed = 1.0f;
    
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 _inputMovement;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMove(InputValue value)
    {
        _inputMovement = value.Get<Vector2>().normalized;

        if (_inputMovement != Vector2.zero)
        {
            _animator.SetBool(IsRunning, true);

            if (_inputMovement.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_inputMovement.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
        else
        {
            _animator.SetBool(IsRunning, false);
        }
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_inputMovement * moveSpeed);
    }
}
