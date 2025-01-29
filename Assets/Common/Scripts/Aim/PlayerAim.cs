using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private Camera _mainCamera;
    [field:SerializeField] public float PowerMultiplier { get; private set; } = 100f;
    [field:SerializeField] public float MaxPower = 5f;
    public Vector2 CurrentAim { get; private set; } = Vector2.zero;
    public float Power { get; private set; } = 1f;
    public Vector2 AimInput = Vector2.zero;
    private LineRenderer _lineRenderer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        AimInput = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        CurrentAim = (AimInput - (Vector2)transform.position).normalized;
        
        if (Input.mouseScrollDelta != Vector2.zero)
        {        
            Power += Input.mouseScrollDelta.y * .2f;
            Power = Mathf.Clamp(Power, 1f,MaxPower);
            EventHandler.Instance.AimEvents.PowerChange?.Invoke(Power, MaxPower);
        }
    
        if (InputHandler.Instance.GetBindByName("Shoot").GetInputDown())
        {
            EventHandler.Instance.AimEvents.Shoot?.Invoke(CurrentAim , Power * PowerMultiplier);
        }
    }
}