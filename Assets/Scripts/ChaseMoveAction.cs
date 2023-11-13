using System.Collections;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "Enemy Actions/Movement/Chase")]
public class ChaseMoveAction : EnemyAction
{
    [SerializeField] public float timeBeforeRetargeting = 1f;

    public override IEnumerator Use(Transform parent)
    {
        state = EnemyAction.ActionState.Active;
        // setting up AIPath
        AIPath aiPath = parent.GetComponent<AIPath>();
        AIDestinationSetter destinationSetter = parent.GetComponent<AIDestinationSetter>();
        if(aiPath == null)
        {
            Debug.LogError("ChaseMoveAction: EXITING! No AIPath component on parent!");
            yield break;
        }
        if (destinationSetter == null)
        {
            Debug.LogError("ChaseMoveAction: EXITING! No AIDestinationSetter component on parent!");
            yield break;
        }

        aiPath.maxSpeed = speed;
        aiPath.canMove = true;

        // acquiring a target
        Transform target = GetTarget(parent, out bool foundTarget);
        destinationSetter.target = target;
        if (foundTarget)
        {
            
            Animator animator = parent.GetComponent<Animator>();

            float durationTimer = 0;
            float targetingTimer = 0;
            float lastTime = Time.time;
            while (durationTimer < duration)
            {
                
                durationTimer += Time.time - lastTime;
                targetingTimer += Time.time - lastTime;
                lastTime = Time.time;

                // running chase
                if (targetingTimer > timeBeforeRetargeting)
                {
                    targetingTimer = 0;
                    target = GetTarget(parent, out foundTarget);
                    destinationSetter.target = target;
                }


                // running animation
                if (animator != null && target != null)
                {
                    animator.SetBool("Moving", true);
                    Vector3 direction = target.position - parent.position;
                    animator.SetFloat("MoveX", direction.x);
                    animator.SetFloat("MoveY", direction.y);
                }
                else
                {
                    Debug.LogError("ChaseMoveAction.Use: No animator on parent parameter");
                }
                yield return new WaitForEndOfFrame();
            }

            if (durationTimer > duration)
            {
                aiPath.canMove = false;
                Debug.Log("ChaseseMoveAction.Use: canMove = false");
            }

            // ending animation
            if (animator != null)
            {
                animator.SetBool("Moving", false);
            }

            // doing cooldown
            if (cooldown > 0)
            {
                state = EnemyAction.ActionState.CoolDown;
                yield return new WaitForSeconds(cooldown);
            }

            // readying and exiting
            state = EnemyAction.ActionState.Ready;
            yield break;
        }
        else
        {
            state = EnemyAction.ActionState.Ready;
            Debug.LogWarning("ChaseMoveAction: No players to target!");
            yield break;
        }
    }

}
