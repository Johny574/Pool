using System;
using System.Linq;
using UnityEngine;

public class Balls : MonoBehaviour
{
    [SerializeField] private Ball[] _balls;
    [field:SerializeField] public Ball.Color Color { get; private set; }
    [field:SerializeField] public TurnHandler.Player Player { get; private set; }
    public bool TwoShots { get; private set; } = false;

    void Start()
    {
        EventHandler.Instance.PocketEvents.BallPocketed += BallPocketed;
        EventHandler.Instance.TurnEvents.TwoShots += OnTwoShots;
        EventHandler.Instance.TurnEvents.TurnChanged += OnTurnChanged;
    }

    private void OnTurnChanged(TurnHandler.Player player)
    {
        if (player != Player)
        {
            return;
        }

        TwoShots = false;
    }

    private void OnTwoShots(TurnHandler.Player player, bool value)
    {
        if (player != Player)
        {
            return;
        }

        TwoShots = value;
    }

    private void BallPocketed(Ball ball)
    {
        if (!_balls.Contains(ball))
        {
            return;
        }

        _balls[Array.IndexOf(_balls, ball)] = null;
    
        EventHandler.Instance.PlayerBallEvents.Update?.Invoke(Player, _balls);
    }
}