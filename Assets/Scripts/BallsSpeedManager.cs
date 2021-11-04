using System.Collections;
using UnityEngine;

public class BallsSpeedManager : MonoBehaviour
{
    public static BallsSpeedManager Instance { get; private set; }

    [SerializeField] private float intervalToSpeedIncrease = 15f; // ������ ���������� ��������
    [SerializeField] private float speedIncreaseValue = 0.2f; // ��������, �� ������� ������������� �������� ����� ������ �������
    [SerializeField] private float maxSpeedIncreaseValue = 6f; // ������������ �������� ������ � �������������� �������� ����

    public float CurrentSpeedIncreaseValue { get; private set; } = 0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            return;
        }
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(IncreaseSpeed());
    }

    private IEnumerator IncreaseSpeed()
    {
        if (CurrentSpeedIncreaseValue + speedIncreaseValue > maxSpeedIncreaseValue)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(intervalToSpeedIncrease);
            CurrentSpeedIncreaseValue += speedIncreaseValue;
            StartCoroutine(IncreaseSpeed());
        }      
    }

    public void Restart()
    {
        StopAllCoroutines();
        CurrentSpeedIncreaseValue = 0f;
        StartCoroutine(IncreaseSpeed());
    }
}
