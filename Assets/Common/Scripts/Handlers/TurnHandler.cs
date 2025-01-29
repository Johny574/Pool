using System.Collections;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public static TurnHandler Instance;
    private int _currentPlayer = 0;
    private bool _turnCheck = false;
    [SerializeField] private Balls[] _playerBalls;
    [SerializeField] private WhiteBall _whiteBall;
    private Player[] _players = new Player[2]
    {
        Player.One,
        Player.Two
    };

    void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    void Start()
    {
        EventHandler.Instance.AimEvents.QueStrike += OnQueHit;
        EventHandler.Instance.PocketEvents.BallPocketed += OnBallPocketed;
    }

    private void OnBallPocketed(Ball ball)
    {
        if (ball._Color != _playerBalls[_currentPlayer].Color)
        {
            EventHandler.Instance.TurnEvents.TwoShots?.Invoke(_players[_currentPlayer], false);
            EventHandler.Instance.TurnEvents.TwoShots?.Invoke(_players[(_currentPlayer + 1) % _players.Length], true);
            return;
        }
        EventHandler.Instance.TurnEvents.TwoShots?.Invoke(_players[_currentPlayer], true);
    }

    void Update()
    {
        if (!_turnCheck)
        {
            return;
        }

        if (EventHandler.Instance.BallControllerEvents.Stopped?.Invoke() == true)
        {
            ChangeTurn();
            _turnCheck = false;
        }
    }

    private void OnQueHit(Vector2 vector1, Vector2 vector2, float arg3)
    {
        StartCoroutine(StartTurnCheck());
    }

    public void ChangeTurn()
    {
        if (_playerBalls[_currentPlayer].TwoShots)
        {
            EventHandler.Instance.TurnEvents.TurnChanged?.Invoke(_players[_currentPlayer]);
            Debug.Log("deez nuts");
            return;
        }

        _currentPlayer = (_currentPlayer + 1) % _players.Length;
        EventHandler.Instance.TurnEvents.TurnChanged?.Invoke(_players[_currentPlayer]);
    }

    IEnumerator StartTurnCheck()
    {
        yield return new WaitForSeconds(1f);
        _turnCheck = true;
    }

    public enum Player
    { 
        One,
        Two
    }
}