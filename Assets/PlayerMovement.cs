using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D MyRigidBoi;
    private Vector3 Change;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        MyRigidBoi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Change = Vector3.zero;
        Change.x = Input.GetAxisRaw("Horizontal");
        Change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
    }
    void UpdateAnimationAndMove()
    {
        if (Change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", Change.x);
            animator.SetFloat("moveY", Change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
    
    void MoveCharacter()
    {
        MyRigidBoi.MovePosition(
            transform.position + Change * speed * Time.fixedDeltaTime
            );
    }
}
