using TMPro;
using UnityEngine;

public class HUDNotificationDisplay : MonoBehaviour
{
    private Animator _animator;
    private TextMeshProUGUI _textField;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _textField = GetComponent<TextMeshProUGUI>();
        EventHandler.Instance.TurnEvents.TurnChanged += OnTurnChanged;
        EventHandler.Instance.TurnEvents.TwoShots += TwoShots;
    }

    private void TwoShots(TurnHandler.Player player, bool value)
    {
        _animator.Play("Play");
        _textField.text = "Another Shot !";
    }

    private void OnTurnChanged(TurnHandler.Player player)
    {
        _animator.Play("Play");
        _textField.text = "Turn Changed !";
    }
}
