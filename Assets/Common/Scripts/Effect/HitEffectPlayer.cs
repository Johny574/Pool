using UnityEngine;

public class HitEffectPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;


    void Start()
    {
       EventHandler.Instance.AimEvents.QueStrike += QueHit; 
    }

    private void QueHit(Vector2 vector1, Vector2 hitPoint, float arg3)
    {
        var o = GameObject.Instantiate(_prefab);
        o.transform.position = hitPoint;
        o.gameObject.SetActive(true);
        o.GetComponent<Animator>().Play("Play");
    }
}
