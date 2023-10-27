using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAction : ScriptableObject
{
    [Header("Attack")]
    [SerializeField] public Vector3 origin;
    [SerializeField, Range(0f, 360)] public float spreadSize = 360;
    [SerializeField, Range(0f, 1f)] public float accuracy;
    // must be gameobjects with the projectile component
    [SerializeField] public List<GameObject> projectiles;
    

    [Header("Movement")]
    [SerializeField] public float moveDuration;
    [SerializeField] public float maxSpeed;
    [SerializeField] public Vector3 moveLocation;

    [Header("General")]
    [SerializeField] public float ActionDuration;

    // desc: Activates/Uses the action
    public abstract void Use(Transform parent);

    public void SpawnProjectile(GameObject projectile, Transform t)
    {
        if (projectile.GetComponent<ProjectileController>())
        {
            Instantiate(projectile, t.position, t.rotation, t);
        }
        else
            Debug.LogError($"Attemping to spawn Projectile {projectile.name} which is not a projectile");
    }

    public void DespawnProjectile(GameObject projectile)
    {
        if (projectile.GetComponent<ProjectileController>())
        {
            Destroy(projectile);
        }
        else
            Debug.LogError($"Attemping to despawn Projectile {projectile.name} which is not a projectile");
    }

    public Vector3 RotateVector(Vector3 direction, float degreesToRotate)
    {
        // Convert degrees to radians (Unity uses radians for trigonometric functions).
        float radiansToRotate = degreesToRotate * Mathf.Deg2Rad;

        // Calculate the new direction using trigonometry.
        float newX = direction.x * Mathf.Cos(radiansToRotate) - direction.y * Mathf.Sin(radiansToRotate);
        float newY = direction.x * Mathf.Sin(radiansToRotate) + direction.y * Mathf.Cos(radiansToRotate);

        // Create a new Vector3 with the rotated values.
        return new Vector3(newX, newY, direction.z);
    }
}
