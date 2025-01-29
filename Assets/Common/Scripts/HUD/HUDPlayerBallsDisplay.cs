using System.Collections.Generic;
using UnityEngine;

public class HUDPlayerBallsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] _player1BallIcons, _player2BallIcons;
    private Dictionary<TurnHandler.Player, GameObject[]> _ballIcons;

    void Awake()
    {
        _ballIcons = new()
        {
            {TurnHandler.Player.One, _player1BallIcons},
            {TurnHandler.Player.Two, _player2BallIcons}
        };
    }

    void Start()
    {
        EventHandler.Instance.PlayerBallEvents.Update += OnPlayerBallsUpdate;
    }

    private void OnPlayerBallsUpdate(TurnHandler.Player player, Ball[] balls)
    {
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i] == null)
            {
                _ballIcons[player][i].gameObject.SetActive(false);
            }
        }
    }
}