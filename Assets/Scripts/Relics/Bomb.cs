using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Relic
{
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] float fuseLength;
    [SerializeField] float spawnDistance;
    Transform bomb;
    float timeToExplode;
    RaycastHit2D[] hits;
    PlayerController player;
    Vector3 bombPosition;

    void Start()
    {
        bomb = transform.GetChild(0);
        transform.DetachChildren();

        Transform temp = transform.parent;
        do
        {
            player = temp.GetComponent<PlayerController>();
            temp = temp.parent;
        } while (player == null);
    }

    // Update is called once per frame
    void Update()
    {
        if (bomb.gameObject.activeSelf)
        {
            //bomb.position = bombPosition;
            //bomb.rotation = Quaternion.identity;

            if (Time.time >= timeToExplode)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        Debug.Log("Boom!!!");
        hits = Physics2D.CircleCastAll(transform.position, range, Vector2.up, 0f);
        foreach(RaycastHit2D hit in hits)
        {
            if(player.isGhost)
            {
                if(hit.transform.tag == "Player")
                {
                    hit.transform.GetComponent<PlayerController>().TakeDamage(damage);
                }
            }
            else
            {
                if(hit.transform.tag == "Enemy" || hit.transform.tag == "Boss")
                {
                    hit.transform.GetComponent<EnemyController>().TakeDamage(damage);
                }
            }
        }
        //particle effects
        bomb.gameObject.SetActive(false);
    }

    public override void Use()
    {
        if (!bomb.gameObject.activeSelf)
        {
            bomb.gameObject.SetActive(true);
            timeToExplode = Time.time + fuseLength;
            bomb.position = player.transform.position + (player.GetAimDirection() * spawnDistance);
        }
        else
        {
            Debug.Log("Bomb already active.");
        }
    }
}
