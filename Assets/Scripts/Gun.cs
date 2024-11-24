using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private static readonly int StrFire = Animator.StringToHash("Fire");
    
    // 基本属性
    public bool allowContinuousShooting = false;
    
    public GameObject bulletPrefab;
    public GameObject bulletShellPrefab;
    
    private Transform _transform;
    private Animator _animator;
    
    private Vector3 _originalLocalPosition;
    private Vector3 _flippedLocalPosition;

    private Transform _muzzleTransform;
    private Transform _ejectionPortTransform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        
        _originalLocalPosition = _transform.localPosition;
        _flippedLocalPosition = _transform.localPosition;
        _flippedLocalPosition.x *= -1;
        
        _muzzleTransform = _transform.Find("Muzzle");
        _ejectionPortTransform = _transform.Find("EjectionPort");
    }
    
    /// <summary>
    /// 修改枪的朝向
    /// </summary>
    /// <param name="direction">枪口朝向，来自人物瞄准的位置</param>
    public void PointTo(Vector3 direction)
    {
        _transform.right = direction;

        if (_transform.eulerAngles.y != 0)
        {
            _transform.eulerAngles = new Vector3(0, 0, 180);
        }

        if (direction.x < 0)
        {
            _transform.localPosition = _flippedLocalPosition;
            _transform.Rotate(new Vector3(180, 0, 0));
        }
        else
        {
            _transform.localPosition = _originalLocalPosition;
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
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.GetComponent<Bullet>().Eject(_muzzleTransform);
        
        GameObject bulletShell = ObjectPool.Instance.GetObject(bulletShellPrefab);
        bulletShell.GetComponent<BulletShell>().Eject(_ejectionPortTransform);
    }
}
