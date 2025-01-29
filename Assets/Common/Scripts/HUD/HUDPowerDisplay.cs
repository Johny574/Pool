using UnityEngine;
using UnityEngine.UI;

public class HUDPowerDisplay : MonoBehaviour
{
    private Image _fillbar;

    void Start()
    {
        _fillbar = GetComponent<Image>();
        EventHandler.Instance.AimEvents.PowerChange += PowerChange;
    }

    private void PowerChange(float arg1, float arg2)
    {
        _fillbar.fillAmount = arg1 / arg2;
    }
}