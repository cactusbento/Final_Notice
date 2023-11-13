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

    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > waitBeforeAction)
        {
            elapsedTime = 0;
            foreach (int actionToTest in actionsToTest)
            {
                if (enemyActions[actionToTest].state == EnemyAction.ActionState.Ready)
                {
                    StartCoroutine(enemyActions[actionToTest].Use(transform));
                }
            }
        }

        // running idle animation
        bool hasAnimator = transform.TryGetComponent<Animator>(out Animator animator);
        if (hasAnimator && !animator.GetBool("Moving"))
        {
            Transform target = EnemyAction.GetTarget(transform, out bool foundTarget);
            if (foundTarget)
            {
                Vector3 direction = target.position - transform.position;
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
            }
        }
    }
}
