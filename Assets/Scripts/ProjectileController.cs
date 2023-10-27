using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public abstract class ProjectileController : MonoBehaviour
{
    [SerializeField] public float damage = 1f;
    [SerializeField] public Vector3 direction;
    [SerializeField] public bool hasMaxDuration = true;
    [SerializeField] public bool hasMaxDistance = true;
    [SerializeField] public float maxDuration = 1f;
    [SerializeField] public float maxDistance = 5f;
    [SerializeField] public float speed = 1f;
    protected float lifeTime;
    protected float travelDistance;

    protected GameObject parent;

    // description: moves projectile along path set out for it
    public abstract void FollowPath();

    public void ChangeDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

    // note: call in all child classes' Start()
    protected void SetUp()
    {
        if (direction == Vector3.zero) direction = transform.up;
        if (!parent) parent = gameObject;
    }

    // note: call in all child classes Update()
    protected void Run()
    {
        if (lifeTime >= maxDuration || travelDistance >= maxDistance)
        {
            Despawn();
        }
        lifeTime += Time.deltaTime;

        FollowPath();
    }
}
