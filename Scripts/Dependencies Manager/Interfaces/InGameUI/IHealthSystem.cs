using System;

public interface IHealthSystem
{
    event Action<float> OnHealthChanged;
    void TakeDamage(float damageAmount);
    void Heal(float healAmount);
    void HealToMaxHealth();
}