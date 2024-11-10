using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private static readonly int StrFire = Animator.StringToHash("Fire");
    
    // 基本属性
    public bool allowContinuousShooting = false;
    
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    
    private Vector3 _originalLocalPosition;
    private Vector3 _flippedLocalPosition;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        
        _originalLocalPosition = _transform.localPosition;
        _flippedLocalPosition = _transform.localPosition;
        _flippedLocalPosition.x *= -1;
    }
    
    /// <summary>
    /// 修改枪的朝向
    /// </summary>
    /// <param name="direction">枪口朝向，来自人物瞄准的位置</param>
    public void PointTo(Vector3 direction)
    {
        _transform.right = direction;

        if (direction.x < 0)
        {
            _transform.localPosition = _flippedLocalPosition;
            _spriteRenderer.flipY = true;
        }
        else
        {
            _transform.localPosition = _originalLocalPosition;
            _spriteRenderer.flipY = false;
        }
    }

    /// <summary>
    /// 开一次火
    /// </summary>
    public void Fire(bool isGoingOn)
    {
        if (allowContinuousShooting)
            _animator.SetBool(StrFire, isGoingOn);
        else if (isGoingOn) 
            _animator.SetTrigger(StrFire);
    }

    /// <summary>
    /// 生成一枚子弹并发射出去
    /// </summary>
    public void FireBullet()
    {
        Debug.Log(gameObject.name + " is going to bullet");
    }
}
