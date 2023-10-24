using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] List<EnemyAction> actions;

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
