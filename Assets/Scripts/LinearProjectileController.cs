using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectileController : ProjectileController
{
    public override void FollowPath()
    {
   
        transform.position = transform.position + (direction.normalized * speed);
    }

    // Start is called before the first frame update
    void Start()
    {
        base.SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        base.Run();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("LinearProjectileController.OnTriggerEnter2D: called");
        base.Collide(collider);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("LinearProjectileController.OnCollisionEnter2D: called");
        base.Collide(collision.collider);
    }
}
