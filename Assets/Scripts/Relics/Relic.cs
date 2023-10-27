using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Relic : MonoBehaviour
{
    [SerializeField] int charges = 1;
    [SerializeField] int currentCharges;
    [SerializeField] float cooldown = 1f;
    
    public abstract void Use();
}
