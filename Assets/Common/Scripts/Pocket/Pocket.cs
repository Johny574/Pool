using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    string _ballLayer = "Ball";   
    List<CircleCollider2D> _collisions = new();
    CircleCollider2D _collider;

    void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (LayerMask.LayerToName(collider.gameObject.layer) != "Ball")
        {
            return;
        }

        _collisions.Remove((CircleCollider2D)collider);
    }

    void OnTriggerEnter2D(Collider2D collider)
    { 
        if (LayerMask.LayerToName(collider.gameObject.layer) != "Ball")
        {
            return;
        }
        _collisions.Add((CircleCollider2D)collider);
    }

    void Update()
    {
        if (_collisions.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _collisions.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, _collisions[i].transform.position);

            if (distance < _collider.radius)
            {
                _collisions[i].GetComponent<Ball>().Pocket();
            }
        }
    }
}