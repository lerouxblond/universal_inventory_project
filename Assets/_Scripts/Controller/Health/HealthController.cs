using UnityEngine;
using Health.Model;
using Health.UI;
using System;

namespace Health
{
    public class HealthController : MonoBehaviour
    {
            [SerializeField] private HealthDataConstructorSO healthData;
            [SerializeField] private HealthUI healthUIPrefab;
            [SerializeField] private RectTransform canvas;
            private HealthUI healthUI;

            void Awake()
            {
                initUIInstance();   
            }

            private void initUIInstance()
            {
                healthUI = Instantiate(healthUIPrefab, canvas);
                Debug.Log("Healthbar instantiate");
                healthData.OnHealthChanged += updateHealthUI;
                if(!healthData.isAlreadySetup)
                    initHealthData();
                else
                    healthDataUI();
            }

            private void healthDataUI()
            {
                healthUI.UpdateHealthBar(healthData.health, healthData.maxHealth);
                Debug.Log("Health, " + healthData.health + "Mana, " + healthData.mana +"Stamina, "+ healthData.stamina);
            }

        void OnDisable()
            {
                healthData.OnHealthChanged -= updateHealthUI;
            }

            private void initHealthData()
            {

                healthData.health = healthData.maxHealth;
                healthData.stamina = healthData.maxStamina;
                healthData.mana = healthData.maxMana;
                healthData.isAlreadySetup = true;
                healthUI.UpdateHealthBar(healthData.health, healthData.maxHealth);
                Debug.Log("Health, " + healthData.health + "Mana, " + healthData.mana +"Stamina, "+ healthData.stamina);

            }

            private void updateHealthUI()
            {
                healthUI.UpdateHealthBar(healthData.health, healthData.maxHealth);
            }

        public void addHealth(int value)
            {
                healthData.addHealth(value);
                Debug.Log("Health added: " + value + " | current health: " + healthData.health);
            }

            public void subHealth(int value)
            {
                healthData.subHealth(value);
                Debug.Log("Health removed: " + value + " | current health: " + healthData.health);
            }
    }

}