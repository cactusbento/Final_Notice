using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    [SerializeField] float health;
    [SerializeField] List<EnemyAction> actions;
    float nextTimeToFire;

}
