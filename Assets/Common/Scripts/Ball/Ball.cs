using UnityEngine;

public class Ball : MonoBehaviour
{
    [field:SerializeField] public Color _Color { get; private set; }= Color.Stripe;

    public void Pocket()
    {
        gameObject.SetActive(false);
        EventHandler.Instance.PocketEvents.BallPocketed?.Invoke(this);
    }

    public enum Color
    {
        Stripe,
        Solid,
        Eight,
        White
    }

}