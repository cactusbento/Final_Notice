using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : Relic
{
    [SerializeField] float range;
    [SerializeField] float disconnectRange;
    [SerializeField] float maxConnectTime;
    [SerializeField] float pullForce;
    [SerializeField] float fireSpeed;
    [SerializeField] float retractSpeed;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Rigidbody2D targetRb;
    [SerializeField] Transform hook;
    [SerializeField] Vector3 connectPoint;
    Vector3 fireDirection;
    public bool active = false;
    public bool retracting = false;

    // Start is called before the first frame update
    void Start()
    {
        if (playerRb == null) playerRb = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //reel in
        if (retracting)
        {
            hook.position = hook.position + (Vector3.Normalize(transform.position - hook.transform.position) * retractSpeed * Time.deltaTime);

            if (Vector3.Distance(hook.position, transform.position) < 0.1f)
            {
                hook.gameObject.SetActive(false);
                retracting = false;
            }
        }
        else if (active)
        {
            if(targetRb)
            {
                connectPoint = targetRb.ClosestPoint(hook.position);
                hook.position = connectPoint;

                //pull player/enemy/object to player
                if (targetRb.tag == "Player" || targetRb.tag == "Enemy" || targetRb.tag == "Object")
                {
                    targetRb.AddForce(Vector3.Normalize(transform.position - connectPoint) * pullForce);
                }
                //pull player to boss
                else if (targetRb.tag == "Boss" || targetRb.tag == "Environment")
                {
                    playerRb.AddForce(Vector3.Normalize(connectPoint - transform.position) * pullForce);
                }
            }
            //fire grapple hook
            else if (fireDirection != Vector3.zero)
            {
                hook.position = hook.position + fireDirection.normalized * fireSpeed * Time.deltaTime;
            }
        }

        if (Vector3.Distance(hook.position, transform.position) > range || (fireDirection == Vector3.zero && Vector3.Distance(hook.position, transform.position) < disconnectRange))
        {
            Disconnect();
        }
    }

    public void Connect(Rigidbody2D rb)
    {
        if (targetRb == null && active && !retracting)
        {
            targetRb = rb;
            fireDirection = Vector3.zero;
        }
    }

    public void Disconnect()
    {
        targetRb = null;
        retracting = true;
        active = false;
    }

    void Fire()
    {
        hook.gameObject.SetActive(true);
        fireDirection = transform.up;
        active = true;
    }

    public override void Use()
    {
        if(active) Disconnect();
        else Fire();
    }

    void OnDrawGizmos()
    {
        //fire direction
        Gizmos.color = Color.green;
        if (fireDirection == Vector3.zero) Gizmos.DrawRay(transform.position, transform.up * range);
        else Gizmos.DrawRay(transform.position, fireDirection * range);
    }
}
