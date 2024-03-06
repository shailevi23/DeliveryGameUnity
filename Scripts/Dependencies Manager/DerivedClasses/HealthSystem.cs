using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IHealthSystem
{
    public event Action<float> OnHealthChanged;

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        health = Mathf.Clamp(health, 0f, maxHealth);
        OnHealthChanged?.Invoke(health);
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0f, maxHealth);
        OnHealthChanged?.Invoke(health);
    }

    public void HealToMaxHealth()
    {
        health = maxHealth;
        OnHealthChanged?.Invoke(health);
    }
}

