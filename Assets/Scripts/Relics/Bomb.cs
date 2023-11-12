using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] float fuseLength;
    [SerializeField] bool ghostBomb;
    float timeToExplode;
    RaycastHit2D[] hits;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timeToExplode)
        {
            Explode();
        }
    }

    void Explode()
    {
        Debug.Log("Boom!!!");
        hits = Physics2D.CircleCastAll(transform.position, range, Vector2.up, 0f);
        foreach(RaycastHit2D hit in hits)
        {
            if(ghostBomb)
            {
                if(hit.transform.tag == "Player")
                {
                    hit.GetComponent<PlayerController>().TakeDamage(damage);
                }
            }
            else
            {
                if(hit.transform.tag == "Enemy" || hit.transform.tag == "Boss")
                {
                    // add enemy take damage
                }
            }
        }
    }

    public void SetUp(float r, float d, float f, bool g)
    {
        range = r;
        damage = d;
        fuseLength = f;
        ghostBomb = g;

        timeToExplode = Time.time + fuseLength;
    }
}
