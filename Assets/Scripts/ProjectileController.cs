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
    [SerializeField] public bool parryable = false;
    [SerializeField] public bool isParried = false;

    protected float lifeTime;
    protected float travelDistance;

    protected GameObject parent;

    // description: moves projectile along path set out for it
    public abstract void FollowPath();

    public void ChangeDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
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
        if ((lifeTime >= maxDuration && hasMaxDuration) || (travelDistance >= maxDistance && hasMaxDistance))
        {
            Despawn();
        }
        lifeTime += Time.deltaTime;

        FollowPath();
    }

    // note: call in all child classes' OnCollisionEnter2D
    protected void Collide(Collider2D collision)
    {
        //Debug.Log("ProjectileController: Colliding!");
        string tag = collision.transform.tag;
        if (tag == "Player")
        {
            PlayerController player = collision.transform.GetComponent<PlayerController>();
            if (player == null)
            {
                Debug.LogError("ProjectileController: collided with non-player, but player-tagged collider!");
                return;
            }
            else
            {
                player.TakeDamage(damage);
                Despawn();
            }

        }
        else if (tag == "Enemy" && isParried)
        {
            EnemyController enemy = collision.transform.GetComponent<EnemyController>();
            if (enemy == null)
            {
                Debug.LogError("ProjectileController: collided with non-enemy, but enemy-tagged collider!");
                return;
            }
            else
            {
                enemy.TakeDamage(damage);
                Despawn();
            }
        }
        else if (tag == "Environment")
        {
            //Debug.Log("ProjectileController: collided with environment!");
            Despawn();
        }
        else
        {
            //Debug.Log("ProjectileController: collided with undealt with tagged game object.");
        }
    }


}
