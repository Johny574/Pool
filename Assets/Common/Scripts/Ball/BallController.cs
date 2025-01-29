using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private List<Rigidbody2D> _balls;

    private void Start()
    { 
        EventHandler.Instance.BallControllerEvents.Stopped += BallsStopped;
    }

    private bool BallsStopped()
    {
        foreach (var ball in _balls)
        {
            if (ball.linearVelocity.magnitude > .01f)
            {
                return false;
            }
        }
        return true; 
    }
}