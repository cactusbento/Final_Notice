using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] public float health = 100f;
    [SerializeField] public List<EnemyAction> enemyActions;

    Vector3 GetTarget(float maxRange, bool isPredictive)
    {
        if (!isPredictive)
        {

        }
        else
        {

        }

        return Vector3.zero;
    }
}
