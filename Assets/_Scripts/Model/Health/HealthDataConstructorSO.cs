using System;
using UnityEngine;

namespace Health.Model
{
    
    [CreateAssetMenu(fileName = "HealthDataConstructor", menuName = "Scriptable Objects/HealthDataConstructor")]
    public class HealthDataConstructorSO : ScriptableObject
    {
        [field: Header("Health")]
        [field: SerializeField]
        public int maxHealth { get; set; }
        [field: SerializeField]
        public int health { get; set; }
        [field: SerializeField]
        public float healthRegenSpeed { get; set; }
        [field: SerializeField]
        public bool isAlreadySetup = false;

        public event Action OnHealthChanged;

        public void addHealth(int amount)
        {
            health = Mathf.Clamp(health + amount, 0, maxHealth);
            OnHealthChanged?.Invoke();
        }
        public void subHealth(int amount)
        {
            health = Mathf.Clamp(health - amount, 0, maxHealth);
            OnHealthChanged?.Invoke();
        }


        [field: Header("Stamina")]
        public int maxStamina { get; set; }
        [field: SerializeField]
        public int stamina { get; set; }
        [field: SerializeField]
        public float staminaRegenSpeed { get; set; }
        [field: Header("Mana")]
        [field: SerializeField]
        public int maxMana { get; set; }
        [field: SerializeField]
        public int mana { get; set; }
        [field: SerializeField]
        public float manaRegenSpeed { get; set; }
    }

}