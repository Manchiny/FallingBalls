using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public static BallGenerator Instance { get; private set; }

    [SerializeField] GameObject _ballPrefab;
    [SerializeField] private int _startBallsCount = 9;
    [SerializeField] private BallData[] _ballDatabase;

    private HashSet<Ball> _balls;

    private float _maxXPosition; // крайняя позиция по оси Х для спавна шаров
    private float _startYPosition;

    private bool _isRestarting;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            _balls = new HashSet<Ball>();
            GetScreenBounds();

            return;
        }
        Destroy(gameObject);
    }

    void Start()
    {
        Restart();
    }

    public void CreateNewBall()
    {
        if (_isRestarting)
            return;

        float posX = Random.Range(-_maxXPosition, _maxXPosition);
        float posY = Random.Range(_startYPosition, _startYPosition + 1f);

        int ballID = Random.Range(0, _ballDatabase.Length);
        BallData ballData = _ballDatabase[ballID];

        Ball ball = Instantiate(_ballPrefab, new Vector2(posX, posY), Quaternion.identity).GetComponent<Ball>();

        _balls.Add(ball);
        ball.Init(ballData);

    }

    public void RemoveBallFromList(Ball ball)
    {
        _balls.Remove(ball);
    }

    public void Restart()
    {
        _isRestarting = true;

        foreach (var ball in _balls)
        {
            ball?.Explode();
        }
        _balls.Clear();

        _isRestarting = false;

        StartCoroutine(InitialCreatingBalls());
    }

    private IEnumerator InitialCreatingBalls()
    {
        for (int i = 0; i < _startBallsCount; i++)
        {
            CreateNewBall();
            yield return new WaitForSeconds(0.25f);
        }

        yield break;
    }

    /// <summary>
    /// Вычисляет допустимые значения по оси Х и актуальное значение по оси Y для спауна шариков, <br/>
    /// основываясь на границах экрана.
    /// </summary>
    private void GetScreenBounds()
    {
        Camera camera = Camera.main;

        float width = camera.pixelWidth;
        float height = camera.pixelHeight;
        Vector2 topRight = camera.ScreenToWorldPoint(new Vector2(width, height));

        _maxXPosition = topRight.x - 0.7f / 2; // учитываю радиус шаров
        _startYPosition = topRight.y + 0.7f / 2;
    }
}
