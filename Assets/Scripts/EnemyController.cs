using System;
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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("EnemyController.Die: Called.");
        gameObject.SetActive(false);

    }

}
