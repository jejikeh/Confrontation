using System;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components
{
    [Serializable]
    public class HealthComponent : IComponent
    {
        [Header("Health Component")]
        [SerializeField] 
        public float MaxHealth;
        public float Health { get; private set; }

        public HealthComponent(HealthComponent healthComponent)
        {
            MaxHealth = healthComponent.MaxHealth;
            Health = MaxHealth;
        }

        public HealthComponent(float maxHealth)
        {
            Health = maxHealth;
            MaxHealth = maxHealth;
        }

        public void Heal(float healAmount)
        {
            if (healAmount > 0)
                Health += healAmount;

            Health = Mathf.Clamp(Health, 0, MaxHealth);
        }

        public void Damage(float damage)
        {
            if (damage > 0)
                Health -= damage;

            Health = Mathf.Clamp(Health, 0, MaxHealth);
        }

        public void Kill()
        {
            Health = 0;
        }
    }
}