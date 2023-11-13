using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Actions/Attacks/Repeated Radial Shot")]
public class RepeatedRadialShotAction : EnemyAction
{
    [Header("Repeated Radial Shot")]
    [SerializeField] public float shotsPerSecond = 1f;
    public override IEnumerator Use(Transform parent)
    {
        state = ActionState.Active;

        float elapsedTime = 0;
        float elapsedShotsPerSecondTimer = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            elapsedShotsPerSecondTimer += Time.deltaTime;

            if (elapsedShotsPerSecondTimer > 1 / shotsPerSecond)
            {

                elapsedShotsPerSecondTimer = 0;

                float degreeIncrement = 360 / projectiles.Count;
                float currDegree = 0;
                foreach (GameObject projectile in projectiles)
                {
                    // Check if real projectile
                    ProjectileController p = projectile.GetComponent<ProjectileController>();
                    if (p)
                    {
                        // Setting the new direction of the projectile
                        p.direction = RotateVector(Vector3.up, currDegree);
                        currDegree += degreeIncrement;
                        SpawnProjectile(projectile, parent);

                    }
                    else
                    {
                        Debug.LogError($"Projectile on {parent.name} is not a real projectile");
                    }
                }
            }
            yield return new WaitForFixedUpdate();
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
}
