using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HUDPlayerDisplay : MonoBehaviour
{
    [SerializeField] private Animator _player1Display, _player2Display;
    private Dictionary<TurnHandler.Player, Animator> _playersDisplay;
    void Start()
    {
        _playersDisplay = new()
        {
            {TurnHandler.Player.One, _player1Display},
            {TurnHandler.Player.Two, _player2Display}
        };
        
        EventHandler.Instance.TurnEvents.TurnChanged += TurnChanged;
        _player1Display.CrossFade("Play", 0f);
    }

    private void TurnChanged(TurnHandler.Player player)
    {
        _playersDisplay.ToList().Find(x => x.Key != player).Value.Play("Idle");
        _playersDisplay[player].Play("Play");
    }
}