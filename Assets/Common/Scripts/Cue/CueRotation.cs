using UnityEngine;

public class CueRotation : MonoBehaviour
{
    [SerializeField] private PlayerAim _aim;

    void Update()
    {
        var angle = Mathf.Atan2(_aim.CurrentAim.y,_aim.CurrentAim.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,angle);
    }
}