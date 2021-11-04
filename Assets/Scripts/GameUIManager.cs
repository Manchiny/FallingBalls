using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private Text _currentPointsText;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private GameObject _pauseMenu;

    [SerializeField] private Button _stopGameButton;

    [SerializeField] private Sprite _puaseImage;
    [SerializeField] private Sprite _playImage;

    [SerializeField] private Text _gameStatusText;
    [SerializeField] private Text _recordText;
    [SerializeField] private Text _resultText;

    private Image _buttonStopImage;
    private Animator _pointsTextAnimator;
    private Animator _healthBarAnimator;

    private bool _isGameStopped;

    private void Awake()
    {
        _pauseMenu.SetActive(false);
        _buttonStopImage = _stopGameButton.GetComponent<Image>();
        _buttonStopImage.sprite = _puaseImage;
        _pointsTextAnimator = _currentPointsText.GetComponent<Animator>();
        _healthBarAnimator = _healthBar.GetComponent<Animator>();
    }
    private void Start()
    {
        Player.Instance.OnHealthChanged += ChangeHealth;
        Player.Instance.OnPointsChanged += ChangePoints;

        _healthBar.maxValue = Player.Instance.MaxHealth;
        _healthBar.value = _healthBar.maxValue;
        _currentPointsText.text = "0";

        _stopGameButton.gameObject.SetActive(true);
    }

    private void ChangeHealth(int value)
    {
        _healthBarAnimator.SetTrigger("Damage");
        _healthBar.value = value;
        if(value <=0)
        {
            StopStartGame(true);
        }
    }

    private void ChangePoints(int value)
    {
        _pointsTextAnimator.SetTrigger("Add");
        _currentPointsText.text = $"{value}";
    }

    public void StopStartGame(bool isGameOver)
    {
        if(isGameOver == false)
        {
            if (_isGameStopped)
            {
                PlayGame();
            }
            else
            {
                PauseGame();
                _gameStatusText.text = "Пауза";
            }
        }
        else
        {
            GameOver();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        _buttonStopImage.sprite = _playImage;

        CheckRecordValue(Player.Instance.CurrentPoints);
        _recordText.text = $"Ваш рекорд: {PlayerPrefs.GetInt("Record")} очков";
        _resultText.text = $"Вы набрали: {Player.Instance.CurrentPoints} очков";

        _pauseMenu.SetActive(true);
        _isGameStopped = true;
    }

    private void PlayGame()
    {
        Time.timeScale = 1;
        _buttonStopImage.sprite = _puaseImage;
        _pauseMenu.SetActive(false);
        _isGameStopped = false;
    }

    public void GameOver()
    {
        _gameStatusText.text = "Игра окончена";
        PauseGame();
        _stopGameButton.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        Player.Instance.Restart();
        BallsSpeedManager.Instance.Restart();
        BallGenerator.Instance.Restart();
        _stopGameButton.gameObject.SetActive(true);

        PlayGame();        
    }

    private void CheckRecordValue(int value)
    {
        if(PlayerPrefs.HasKey("Record"))
        {
            if(PlayerPrefs.GetInt("Record") < value)
            {
                PlayerPrefs.SetInt("Record", value);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Record", value);
        }
    }
}
