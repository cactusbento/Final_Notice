using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Actions/Attacks/Repeated Targeted Shot")]
public class RepeatedTargetedShot : EnemyAction
{
    [Header("Repeated Targeted Shot")]
    [SerializeField] public float shotsPerSecond = 1f;
    public override IEnumerator Use(Transform user)
    {
        state = ActionState.Active;

        // running update equivalent
        float elapsedTime = 0;
        float shotTimer = 0;
        while (elapsedTime < duration)
        {

            elapsedTime += Time.deltaTime;
            shotTimer += Time.deltaTime;

            if (shotTimer > (1f / shotsPerSecond))
            {
                shotTimer = 0;

                Transform target = GetTarget(user.transform, out bool foundTarget);
                if (foundTarget)
                { 
                    // shooting at the target
                    // calculate the primary direction
                    Vector3 primaryDirection = target.position - user.position;
                    primaryDirection = Vector3.Normalize(primaryDirection);
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
                                SpawnProjectile(projectile, user);
                            }
                            else
                            {
                                p.direction = RotateVector(primaryDirection, currDegree);
                                SpawnProjectile(projectile, user);
                                currDegree += spread / (projectiles.Count - 1);
                            }
                        }
                        else
                        {
                            Debug.LogError($"RepeatedTargetedShotAction.Use: Projectile on {user.name} is not a real projectile");
                        }
                    }
                }
                else
                {
                    Debug.LogError("RepeatedTargetedShotAction.Use: No players to target!");
                }

            }

            yield return new WaitForEndOfFrame();
        }


        // doing cooldown
        if (cooldown > 0)
        {
            state = ActionState.CoolDown;
            yield return new WaitForSeconds(cooldown);
        }

        // readying and exiting
        state = ActionState.Ready;
        yield break;
    }
}
