using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float fireRate = 1f;
    float nextTimeToFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextTimeToFire)
        {
            nextTimeToFire = nextTimeToFire + 1f / fireRate;
            Object.Instantiate(projectile, transform.position, transform.rotation, transform);
        }
    }
}
