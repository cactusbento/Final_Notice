using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Actions/Spinning Shot Action")]
public class SpinningProjectiles : EnemyAction
{
    [Header("Spinning Projectile Action")]
    [SerializeField] public float shotsPerSecond;
    [SerializeField] public float degreeChangePerShot;


    public override IEnumerator Use(Transform parent)
    {
        Debug.Log("0");
        float elapsedTimeDuration = 0;
        float baseDegree = 0;

        Debug.Log("1");
        state = EnemyAction.ActionState.Active;
        while (elapsedTimeDuration < duration) {
            // updating times
            elapsedTimeDuration += Time.deltaTime;
            Debug.Log($"Elapsed Time in Projectiles {elapsedTimeDuration}");

            // shooting radial shot
            Debug.Log("2");
            float degreeIncrement = 360 / projectiles.Count;
            float currDegree = baseDegree;
            Debug.Log("Firing Shots");
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
                
            // increment base degree -> next shot is at slighlty different agnl
            baseDegree += degreeChangePerShot;
            Debug.Log($"Waiting {1f / shotsPerSecond}s");
            yield return new WaitForSeconds(1f / shotsPerSecond);
        }

        // doing cooldown
        if (cooldown > 0)
        {
            state = EnemyAction.ActionState.CoolDown;
            yield return new WaitForSeconds(cooldown);
        }
        
        // readying and exiting
        state = EnemyAction.ActionState.Ready;
        Debug.Log("Exiting Spinning Projectiles");
        yield break;
    }
}
