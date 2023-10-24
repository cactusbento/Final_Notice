using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] float damage = 1f;
    [SerializeField] Vector3 direction;
    [SerializeField] bool hasMaxDuration = true;
    [SerializeField] bool hasMaxDistance = true;
    [SerializeField] float maxDuration = 1f;
    [SerializeField] float maxDistance = 5f;
    [SerializeField] float speed = 1f;
    float lifeTime;
    float travelDistance;

    private void Start()
    {
        if (direction == Vector3.zero) direction = transform.up;
    }

    private void Update()
    {
        if(lifeTime >= maxDuration || travelDistance >= maxDistance)
        {
            Despawn();
        }
        lifeTime += Time.deltaTime;

        transform.position = transform.position + (direction * speed);
    }

    public void Fire()
    {

    }

    public void ChangeDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
