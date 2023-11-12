using System.Collections;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "Enemy Actions/Movement/Chase")]
public class ChaseMoveAction : EnemyAction
{
    private AIPath aiPath;

    public override IEnumerator Use(Transform parent)
    {

        // setting up AIPath
        aiPath = parent.GetComponent<AIPath>();
        if(aiPath == null)
        {
            Debug.LogError("ChaseMoveAction: EXITING! No AIPath component on parent!");
            yield break;
        }
        aiPath.maxSpeed = speed;

        // acquiring a target
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {  
            GameObject target = players[0];
            float minDistance = -1;
            // getting the nearest player as a target
            foreach (GameObject player in players)
            {
                if (minDistance == -1)
                {
                    minDistance = Vector3.Distance(player.transform.position, parent.transform.position);
                }
                else
                {
                    float testDistance = Vector3.Distance(player.transform.position, parent.transform.position);
                    if (testDistance < minDistance)
                    {
                        minDistance = testDistance;
                        target = player;
                    }
                }
            }
            aiPath.canMove = true;
            aiPath.destination = target.transform.position;

            state = EnemyAction.ActionState.Active;
            yield return new WaitForSeconds(duration);
            aiPath.canMove = false;


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
            Debug.LogWarning("ChaseMoveAction: No players to target!");
            yield break;
        }
    }

}
