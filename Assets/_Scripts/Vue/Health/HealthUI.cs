using UnityEngine;
using UnityEngine.UI;

namespace Health.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;

        public void UpdateHealthBar(int health, int maxHealth)
        {
            healthSlider.value = (float)health/maxHealth;
            Debug.Log("HealthBar Updated" + health + maxHealth);
        }
    }

}