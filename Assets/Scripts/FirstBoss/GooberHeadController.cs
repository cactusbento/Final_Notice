using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooberHeadController : EnemyController
{
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 3)
        {
            elapsedTime = 0;
            enemyActions[0].Use(transform);
        }
    }
}
