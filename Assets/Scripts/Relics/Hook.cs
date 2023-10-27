using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] Hookshot hookshot;

    public void Disconnect()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        hookshot.Connect(col.transform.GetComponent<Rigidbody2D>());
    }
}
