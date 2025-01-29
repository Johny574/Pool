using System;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static EventHandler Instance;
    public AimEvents AimEvents { get; private set; } = new();
    public PocketEvents PocketEvents { get; private set; } = new();
    public TurnEvents TurnEvents { get; private set; } = new();
    public BallControllerEvents BallControllerEvents { get; private set; } = new();
    public PlayerBallEvents PlayerBallEvents { get; private set; } = new();

    void Awake()
    {
        if (Instance == null) { Instance = this; }
    }
}

public class TurnEvents
{
    public Action<TurnHandler.Player> TurnChanged;
    public Action<TurnHandler.Player, bool> TwoShots;
    public Func<TurnHandler.Player, TurnHandler.Player> CurrentPlayer;
}

public class PocketEvents
{
    public Action<Ball> BallPocketed;
}

public class AimEvents
{
    public Action<Vector2, float> Shoot;
    public Action<Vector2, Vector2, float> QueStrike;
    public Action<float, float> PowerChange;
}

public class BallControllerEvents
{
    public Func<bool> Stopped;
}

public class PlayerBallEvents
{
    public Action<TurnHandler.Player, Ball[]> Update;
}