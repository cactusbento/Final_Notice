using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyAction : ScriptableObject
{
    [Header("Attack")]
    [SerializeField] Vector3 origin;
    [SerializeField] bool originIsRelative = true;
    [SerializeField] int numBullets = 1;
    [SerializeField, Range(0f, 360)] float spreadSize = 360;
    [SerializeField, Range(0f, 1f)] float accuracy;
    // must be gameobjects with the projectile component
    [SerializeField] GameObject projectile;
    

    [Header("Movement")]
    [SerializeField] float moveDuration;
    [SerializeField] float maxSpeed;
    [SerializeField] Vector3 moveLocation;

    [Header("Sprite")]
    [SerializeField] SpriteRenderer spriteRender;

    public void Use()
    {
        // Performing the attack

    }
}
