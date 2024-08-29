using TMPro;
using UnityEngine;


namespace ConnectionLost
{
    internal sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI attackTmp;
        [SerializeField] private TextMeshProUGUI healthTmp;

        [Space]
        [SerializeField] private HealthComponent health;
        [SerializeField] private AttackComponent attack;


        private void OnEnable()
        {
            health.OnDamageReceived += UpdateHealth;
            attackTmp.text = $"<sprite=1>{attack.AttackDamage}";
        }

        private void OnDisable()
        {
            health.OnDamageReceived -= UpdateHealth;
        }

        private void UpdateHealth()
        {
            healthTmp.text = $"<sprite=0>{health.CurrentHealth}";
        }
    }
}