using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{  
    [Header("Movement")]
    [SerializeField] float acceleration = 1f;
    [SerializeField] float deccelerationScalar = 1f;
    [SerializeField] float topSpeed = 5f;

    Vector2 movementInput;
    Rigidbody2D rb;
    Vector2 moveForce;

    [Header("Aiming")]
    [SerializeField] float aimInputThreshold;
    [SerializeField] Transform reticleContainer;
    [SerializeField] Transform reticle;

    Vector2 aimInput;

    [Header("Parry")]
    [SerializeField] float parryStrength = 5f;
    [SerializeField] float parryRange = 1f;
    [SerializeField] float parryAngle = 30f;
    [SerializeField] float parryRate = 1f;
    [SerializeField] CircleCollider2D parryCollider;

    bool parryInput;
    float nextTimeToParry;
    RaycastHit2D hit;
    List<Collider2D> parryColliderOverlaps;
    Collider2D parryTarget;
    Rigidbody2D parryTargetRb;
    float minDistance;
    float distance;

    [Header("Ability")]
    [SerializeField] float abilityRate = 4f;

    bool abilityInput;
    float nextTimeToAbility;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();

        parryCollider.radius = parryRange;
        parryColliderOverlaps = new List<Collider2D>();

        reticle.localPosition = new Vector3(0f, parryRange, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        if (rb.velocity.magnitude > topSpeed)
        {
            moveForce = rb.mass * deccelerationScalar * (-rb.velocity);
        }
        else
        {
            moveForce = rb.mass * acceleration * movementInput;  
        }
        rb.AddForce(moveForce);

        //aiming
        if (aimInput.magnitude > aimInputThreshold)
        {
            reticleContainer.up = aimInput;
        }

        //parry
        if(parryInput && Time.time >= nextTimeToParry)
        {
            nextTimeToParry = Time.time + 1f / parryRate;
            Parry();
        }

        //ability
        if(abilityInput && Time.time >= nextTimeToAbility)
        {
            nextTimeToAbility = Time.time + 1f / abilityRate;
            Ability();
        }
    }

    void Parry()
    {
        Debug.Log("Parry.");

        parryColliderOverlaps.Clear();
        parryCollider.OverlapCollider(new ContactFilter2D().NoFilter(), parryColliderOverlaps);

        if (parryColliderOverlaps.Count > 0)
        {
            minDistance = -1f;
            foreach (Collider2D col in parryColliderOverlaps)
            {
                //check valid object
                if (IsParryableObject(col))
                {
                    //check valid angle
                    if (IsInParryCone(col))
                    {
                        //check distance
                        distance = Vector3.Distance(col.transform.position, transform.position);
                        if (distance < minDistance || minDistance == -1f)
                        {
                            minDistance = distance;
                        }
                    }
                }
            }

            if (minDistance != -1f)
            {
                parryTarget = parryColliderOverlaps.Find(col => Vector3.Distance(col.transform.position, transform.position) == minDistance);
                Debug.Log("parryTarget: " + parryTarget.transform.name);

                if(parryTarget.gameObject.TryGetComponent<Rigidbody2D>(out parryTargetRb))
                {
                    parryTargetRb.AddForce(reticleContainer.up * parryStrength);
                }
            }
        }
    }

    bool IsParryableObject(Collider2D col)
    {
        return col.gameObject.GetInstanceID() != gameObject.GetInstanceID();
    }

    bool IsInParryCone(Collider2D col)
    {
        if (Vector3.Angle(col.transform.position - transform.position, reticleContainer.up) <= parryAngle)
        {
            return true;
        }

        hit = Physics2D.Raycast(transform.position, Quaternion.Euler(0f, 0f, -parryAngle) * reticle.up, parryRange);
        if (hit && hit.collider.gameObject.GetInstanceID() == col.gameObject.GetInstanceID())
        {
            return true;
        }

        hit = Physics2D.Raycast(transform.position, Quaternion.Euler(0f, 0f, parryAngle) * reticle.up, parryRange);
        if (hit && hit.collider.gameObject.GetInstanceID() == col.gameObject.GetInstanceID())
        {
            return true;
        }

        return false;
    }

    void Ability()
    {
        Debug.Log("Ability used.");
    }

    void OnDrawGizmos()
    {
        //parry range
        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
        Gizmos.DrawSphere(transform.position, parryRange);

        //parry cone
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0f, 0f, -parryAngle) * reticleContainer.up * parryRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0f, 0f, parryAngle) * reticleContainer.up * parryRange);
    }

    
    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnAim(InputAction.CallbackContext ctx) => aimInput = ctx.ReadValue<Vector2>();

    public void OnParry(InputAction.CallbackContext ctx) => parryInput = ctx.ReadValueAsButton();

    public void OnAbility(InputAction.CallbackContext ctx) => abilityInput = ctx.ReadValueAsButton(); 
}
