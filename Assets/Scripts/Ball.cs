using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fxPrefab; // ссылка на эффект
    private ParticleSystem _explodeFX; // экземпл€р эффекта
   
    private float _speed;

    public BallData BallData { get; private set; } 

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private BallGenerator _ballGenerator;
    private BallsSpeedManager _speedManager;

    private bool _isInitialized;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _explodeFX = Instantiate(_fxPrefab, transform.position, Quaternion.identity);
        _rigidbody = GetComponent<Rigidbody2D>();
        _speedManager = BallsSpeedManager.Instance;
    }
    private void Start()
    {
        _ballGenerator = BallGenerator.Instance;        
    }

    private void FixedUpdate()
    {
        if(_isInitialized == false)
        {
            return;
        }

        _rigidbody.velocity = Vector2.down * _speed;
    }

    private void OnMouseDown() // симул€ци€ мыши отключаетс€ на устройствах, поддерживаемых touch. ѕрописано в скрипте UserTouchInput.
    {
        AddPlayerPoints();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            AddPlayerDamage();
        }
    }

    public void Init(BallData ballData)
    {
        SetColor(ballData.color);
        _speed = ballData.startSpeed + _speedManager.CurrentSpeedIncreaseValue;
       
        BallData = ballData;
        _isInitialized = true;
    }
    private void SetColor(Color color)
    {
        _spriteRenderer.color = color;
        _explodeFX.startColor = color;
    }

    public void AddPlayerPoints()
    {
        _ballGenerator.RemoveBallFromList(this);
        Explode();
        Player.Instance.AddPoints(BallData.reward);
    }

    private void AddPlayerDamage()
    {
        _ballGenerator.RemoveBallFromList(this);
        Explode();
        Player.Instance.GetDamage(BallData.damage);
    }

    public void Explode()
    {
        _explodeFX.transform.position = transform.position;
        _explodeFX.Play();
        _ballGenerator.CreateNewBall();

        Destroy(gameObject);
    }
}
