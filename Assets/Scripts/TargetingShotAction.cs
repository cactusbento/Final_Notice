using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Actions/Targeting Shot")]
public class TargetingShotAction : EnemyAction
{
    [Header("Targeting Shot")]
    [SerializeField] public bool hasTargetingRange = false;
    [SerializeField] public float targetingRange;
    public override IEnumerator Use(Transform parent)
    {
        // acquiring a target
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject target = players[0];
        float minDistance = -1;
        if (players.Length > 0 )
        {
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

            // quit if no player within range
            if (hasTargetingRange && minDistance > targetingRange)
            {
                yield break;
            }

            // shooting at the target
            // calculate the primary direction
            Vector3 primaryDirection = target.transform.position - parent.transform.position; 
            // adjust that by accuracy
            if (accuracy != 1)
            {
                float degreeRange = (1 - accuracy) * 360 / 2;
                float degreeMod = Random.Range(-degreeRange, degreeRange);
                primaryDirection = RotateVector(primaryDirection, degreeMod);
            }
            // for each projectile calculate its actual direction based on the spread and the primary target
            float currDegree = -1 * spread / 2;
            foreach (GameObject projectile in projectiles)
            {
                // Check if real projectile
                ProjectileController p = projectile.GetComponent<ProjectileController>();
                if (p)
                {
                    if (projectiles.Count == 1)
                    {
                        p.direction = primaryDirection;
                        SpawnProjectile(projectile, parent);
                    }
                    else
                    {
                        p.direction = RotateVector(primaryDirection, currDegree);
                        SpawnProjectile(projectile, parent);
                        currDegree += spread / (projectiles.Count - 1);
                    }
                }
                else
                {
                    Debug.LogError($"Projectile on {parent.name} is not a real projectile");
                }
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
            Debug.LogError("TargetingShotAction: No players to target!");
            yield break;
        }
    }
}
