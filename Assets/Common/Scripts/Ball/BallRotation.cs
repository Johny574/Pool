using UnityEngine;

public class BallRotation : MonoBehaviour
{
    private Rigidbody2D _rb;
    float angle = 0f;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {        
        angle = Mathf.Atan2(_rb.linearVelocity.magnitude, _rb.linearVelocity.magnitude) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
