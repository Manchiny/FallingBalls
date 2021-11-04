using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int MaxHealth { get; private set; } = 100;
    private int _currentHealth;

    public int CurrentPoints { get; private set; } = 0;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnPointsChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        _currentHealth = MaxHealth;
        CurrentPoints = 0;

        OnPointsChanged?.Invoke(CurrentPoints);
        OnHealthChanged?.Invoke(_currentHealth);
    }

    public void AddPoints(int points)
    {
        CurrentPoints += points;
        OnPointsChanged?.Invoke(CurrentPoints);
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth);
    }
}
