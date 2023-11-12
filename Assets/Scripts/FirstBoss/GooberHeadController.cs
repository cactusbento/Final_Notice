using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooberHeadController : EnemyController
{
    private float elapsedTime = 0;

    [Header("Test Values")]
    [SerializeField] public int[] actionsToTest;
    [SerializeField] public float waitBeforeAction = 2;
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
        if (elapsedTime > waitBeforeAction)
        {
            elapsedTime = 0;
            foreach(int actionToTest in actionsToTest)
            {
                if (enemyActions[actionToTest].state == EnemyAction.ActionState.Ready)
                {
                    StartCoroutine(enemyActions[actionToTest].Use(transform));
                }
            }

        }
    }
}
