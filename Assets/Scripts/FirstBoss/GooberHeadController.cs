using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooberHeadController : EnemyController
{
    private float elapsedTime = 0;
    [SerializeField] public int ActionToTest = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Setting All Actions to ready State
        foreach (EnemyAction action in enemyActions) {
            action.state = EnemyAction.ActionState.Ready;
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 10)
        {
            elapsedTime = 0;
            Debug.Log($"Action State = {enemyActions[ActionToTest].state}");
            if (enemyActions[ActionToTest].state == EnemyAction.ActionState.Ready) {
                Debug.Log("Starting Action!");
                StartCoroutine(enemyActions[ActionToTest].Use(transform));
                Debug.Log("F");
            }
            else
            {
                Debug.Log("Action failed: wasn't ready");
            }
                
        }
    }
}
