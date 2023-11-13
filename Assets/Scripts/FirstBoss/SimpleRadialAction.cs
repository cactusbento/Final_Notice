using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Actions/Simple Radial Shot")]
public class SimpleRadialAction : EnemyAction
{
    public override IEnumerator Use(Transform parent)
    {
        origin = parent.position;
        float degreeIncrement = 360 / projectiles.Count;
        float currDegree = 0;
        foreach (GameObject projectile in projectiles) {
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
