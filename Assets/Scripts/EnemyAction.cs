using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAction : ScriptableObject
{

    [Header("General")]
    [SerializeField] public string actionName = string.Empty;
    [SerializeField] public float duration = 0;
    [SerializeField] public float cooldown = 0;

    [Header("Attack")]
    [SerializeField] public Vector3 origin = Vector3.zero;
    [SerializeField, Range(0f, 360)] public float spread = 360;
    [SerializeField, Range(0f, 1f)] public float accuracy = 1;
    // must be gameobjects with the projectile component
    [SerializeField] public List<GameObject> projectiles;
    

    [Header("Movement")]
    [SerializeField] public float speed = 0;

    public enum ActionState 
    {
        Active,
        Ready,
        CoolDown
    }
    public ActionState state = ActionState.Ready;

    // desc: Activates/Uses the action
    public abstract IEnumerator Use(Transform parent);

    public static void SpawnProjectile(GameObject projectile, Transform t)
    {
        if (projectile.TryGetComponent<ProjectileController>(out ProjectileController p))
        {
            GameObject newProjectile = Instantiate(projectile, t.position, t.rotation);
        }
        else
            Debug.LogError($"Attemping to spawn Projectile {projectile.name} which is not a projectile");

    }

    public static void DespawnProjectile(GameObject projectile)
    {
        if (projectile.GetComponent<ProjectileController>())
        {
            Destroy(projectile);
        }
        else
            Debug.LogError($"Attemping to despawn Projectile {projectile.name} which is not a projectile");
    }

    public static Vector3 RotateVector(Vector3 direction, float degreesToRotate)
    {
        // Convert degrees to radians (Unity uses radians for trigonometric functions).
        float radiansToRotate = degreesToRotate * Mathf.Deg2Rad;

        // Calculate the new direction using trigonometry.
        float newX = direction.x * Mathf.Cos(radiansToRotate) - direction.y * Mathf.Sin(radiansToRotate);
        float newY = direction.x * Mathf.Sin(radiansToRotate) + direction.y * Mathf.Cos(radiansToRotate);

        // Create a new Vector3 with the rotated values.
        return new Vector3(newX, newY, direction.z);
    }

    public static Transform GetTarget(Transform parent, out bool foundTarget, float maxRange = -1, float predictOut = 0)
    {
        // acquiring target based on current position
        if (predictOut == 0)
        {
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
                        minDistance = Vector3.Distance(player.transform.position, parent.position);
                    }
                    else
                    {
                        float testDistance = Vector3.Distance(player.transform.position, parent.position);
                        if (testDistance < minDistance)
                        {
                            minDistance = testDistance;
                            target = player;
                        }
                    }
                }

                // checking if minDistance in range
                if (maxRange != -1 && minDistance > maxRange)
                {
                    Debug.Log("EnemyController.GetTarget: Failed to target, nobody within range.");
                    foundTarget = false;
                    return null;
                }
                foundTarget = true;
                return target.transform;
            }
            else
            {
                Debug.LogWarning("EnemyController.GetTarget: No players to target!");
                foundTarget = false;
                return null;
            }
        }
        else
        {
            throw new NotImplementedException();
        }
    }

}
