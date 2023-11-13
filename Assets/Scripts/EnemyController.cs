using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [Header("Enemy Controller")]
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] public float currentHealth = 100f;
    [SerializeField] public bool pushable = false;
    [SerializeField] public List<EnemyAction> enemyActions;
    [SerializeField] public Animator animator;
    [SerializeField] public bool isBoss = false;
    [SerializeField] private HealthBar healthBar;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("EnemyController.Die: Called.");

        if (isBoss)
        {
            PlayerManager manager = GameObject.Find("PlayerInputManager").GetComponent<PlayerManager>();
            manager.BossDied();
        }
        gameObject.SetActive(false);
    }

    Vector3 GetTarget(float maxRange, bool isPredictive)
    {
        if (!isPredictive)
        {

        }
        else
        {

        }

        return Vector3.zero;
    }

    public void SetUp()
    {
        if (isBoss)
        {
            healthBar = GameObject.Find("BossHealthBar").GetComponent<HealthBar>();
            healthBar.SetMaxHealth(maxHealth);
        }
    }
}
