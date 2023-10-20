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
    [SerializeField] float speed;

    private void Start()
    {
        
    }

    private void Update()
    {
       
    }

    public void Fire()
    {

    }
}
