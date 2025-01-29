using System.Collections;
using UnityEngine;

public class Cue : MonoBehaviour
{
    [SerializeField] private PlayerAim _aim;
    [SerializeField] private float _minPowerDistance = 0f, _maxPowerDistance = 1f;
    private LineRenderer _lineRenderer;
    float _magnitude = 0f;
    Vector2 _size;
    bool _shooting = false;
    private AudioSource _queAudio;
    void Awake()
    {
        _queAudio = GetComponent<AudioSource>();
       _lineRenderer = GetComponent<LineRenderer>(); 
       _size = GetComponent<SpriteRenderer>().sprite.bounds.size/2;
    }
    void Start()
    {
        EventHandler.Instance.AimEvents.Shoot += Shoot;
        EventHandler.Instance.TurnEvents.TurnChanged += (player) => gameObject.SetActive(true);
    }

    private void Shoot(Vector2 aim, float power)
    {
        StartCoroutine(ShootAnimation(aim, power));
    }

    void Update()
    {
        if (_shooting)
        {
            return;
        }

        _magnitude = Mathf.Lerp(_minPowerDistance, _maxPowerDistance, _aim.Power / _aim.MaxPower);
        transform.position = (Vector2)_aim.transform.position + (_aim.CurrentAim * _size.magnitude * _magnitude);
        _lineRenderer.SetPositions(new Vector3[2] {_aim.transform.position, (Vector2)_aim.transform.position - _aim.CurrentAim * _size.magnitude * _magnitude});
    }

    IEnumerator ShootAnimation(Vector2 aim, float power)
    {
        float animtime = 0f;
        float duration = .1f;
        Vector2 aimPoint = _aim.CurrentAim;
        _shooting = true;

        Vector2 initialPos = (Vector2)transform.position;
        Vector2 endPos = (Vector2)_aim.transform.position + aimPoint * _size.magnitude * _minPowerDistance;

        while (animtime < duration)
        {
            animtime += Time.deltaTime;
            float t = Mathf.Clamp01(animtime/duration);
            transform.position = Vector2.Lerp(initialPos, endPos, t);
            yield return null;
        }
        
        EventHandler.Instance.AimEvents.QueStrike?.Invoke(aim, (Vector2)_aim.transform.position + aimPoint * _minPowerDistance, power);
        _queAudio.Play();

        yield return new WaitForSeconds(1f);
        _shooting = false;
        gameObject.SetActive(false);
    }

}