using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void SetUp()
    {
        currentHealth = maxHealth;
        foreach (EnemyAction action in enemyActions)
        {
            action.state = EnemyAction.ActionState.Ready;
        }
        if (isBoss)
        {
            healthBar = GameObject.Find("BossHealthBar").GetComponent<HealthBar>();
            healthBar.SetMaxHealth(maxHealth);
        }
    }

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
        // Debug.Log("EnemyController.Die: Called.");

        if (isBoss)
        {
            PlayerManager manager = GameObject.Find("PlayerInputManager").GetComponent<PlayerManager>();
            manager.BossDied();
        }
        gameObject.SetActive(false);
    }

    public void TryToStartAction(string enemyActionName, bool logging = false)
    {
        EnemyAction action = enemyActions.FirstOrDefault(x => x.actionName == enemyActionName);
        if (action != null)
        {
            if (action.state == EnemyAction.ActionState.Ready)
            {
                StartCoroutine(action.Use(transform));
            }
            else if (logging)
            {
                Debug.Log($"EnemyController.TryToStartAction: couldn't start '{enemyActionName}', wasn't ready...");
            }
        }
        else
        {
            Debug.LogError($"EnemyController.TryToStartAction: Couldn't find Enemy Action named '{enemyActionName}'!");
        }
    }

    public bool AllActionsReady()
    {
        foreach(EnemyAction action in enemyActions)
        {
            if (action.state != EnemyAction.ActionState.Ready)
            {
                return false;
            }
        }
        return true;
    }

    public void SetUp()
    {

    }
}
