using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShell : MonoBehaviour
{
    public float speed = 10.0f;
    public float angleRandomRange = 30.0f;
    public float stopTime = 0.5f;
    public float fadeTime = 0.1f;
    
    private Rigidbody2D _rb;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    
    private float _originalGravityScale;
    private Color _originalColor;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _originalGravityScale = _rb.gravityScale;
        _originalColor = _spriteRenderer.color;
    }

    public void Eject(Transform ejectionPortTransform)
    {
        _transform.SetPositionAndRotation(ejectionPortTransform.position, ejectionPortTransform.rotation);
        _transform.Rotate(Vector3.forward, Random.Range(-angleRandomRange, angleRandomRange));
        
        _rb.velocity = transform.up * speed;
        _rb.gravityScale = _originalGravityScale;
        _spriteRenderer.color = _originalColor;
        
        StartCoroutine(Stop());
    }

    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(stopTime);
        
        _rb.gravityScale = 0;
        _rb.velocity = Vector2.zero;

        while (_spriteRenderer.color.a > 0)
        {
            Color color = _spriteRenderer.color;
            color.a -= fadeTime;
            _spriteRenderer.color = color;
            yield return new WaitForFixedUpdate();
        }
        
        ObjectPool.Instance.ReturnObject(gameObject);
    }
}
