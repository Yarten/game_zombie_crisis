using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Bullet : MonoBehaviour
{
    public float speed = 30.0f;
    public float maxDistance = 100.0f;
    public float angleRandomRange = 5.0f;
    public LayerMask collisionLayer;
    public GameObject explosionPrefab;
    
    private Rigidbody2D _rb;
    private Transform _transform;
    
    private Vector2 _lastPosition;
    private float _travelledDistance;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }

    public void Eject(Transform muzzleTransform)
    {
        _transform.SetPositionAndRotation(muzzleTransform.position, muzzleTransform.rotation);
        _transform.Rotate(Vector3.forward, Random.Range(-angleRandomRange, angleRandomRange));
        
        _rb.velocity = _transform.right * speed;
        _lastPosition = _transform.position;
        _travelledDistance = 0;
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = _transform.position;
        
        float dis = (currentPosition - _lastPosition).magnitude;
        _travelledDistance += dis;
        
        RaycastHit2D hit = Physics2D.Raycast(_lastPosition, _transform.right, dis, collisionLayer);

        if (hit || _travelledDistance > maxDistance)
        {
            ObjectPool.Instance.ReturnObject(gameObject);

            GameObject explosion = ObjectPool.Instance.GetObject(explosionPrefab);
            explosion.transform.position = _transform.position;
        }
        else
            _lastPosition = currentPosition;
    }
}