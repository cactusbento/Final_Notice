using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectileController : ProjectileController
{
    public override void FollowPath()
    {
        transform.position = transform.position + (direction * speed);
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
}
