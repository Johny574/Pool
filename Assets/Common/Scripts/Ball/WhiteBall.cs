using UnityEngine;

public class WhiteBall : MonoBehaviour
{
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearDamping = 1f;
    }

    void Start()
    {
        EventHandler.Instance.AimEvents.QueStrike += QueHit;
    }

    private void QueHit(Vector2 aim, Vector2 hitPoint, float power) => _rb.AddForce((Vector2)transform.position - aim * power);
}