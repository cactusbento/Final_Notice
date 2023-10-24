using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    private Rigidbody2D rgbd;

    /// <summary>
    /// This will add a RigidBody2D if one is not present.
    /// </summary>
    void Start() {
        if(!TryGetComponent<Rigidbody2D>(out rgbd)) {
            this.AddComponent<Rigidbody2D>();
            this.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveFn();

    }

    /// <summary>
    /// This function handles all movement of the
    /// player. 
    /// 
    /// Goto
    /// <code>
    /// Edit > Project Settings > Inputmanager > Axis
    /// </code>
    /// 
    /// `WASD` Movement is + or - 1.0f
    /// </summary>
    void MoveFn() {
        float x_scale = Input.GetAxis("Horizontal");
        float y_scale = Input.GetAxis("Vertical");
        float dt = Time.deltaTime;

        transform.Translate(
            x_scale * dt * speed,
            y_scale * dt * speed,
            0
        );
    }
//    void UpdateAnimationAndMove()
//    {
//        if (Change != Vector3.zero)
//        {
//            MoveCharacter();
//            animator.SetFloat("moveX", Change.x);
//            animator.SetFloat("moveY", Change.y);
//            animator.SetBool("Moving", true);
//        }
//        else
//        {
//            animator.SetBool("Moving", false);
//        }
//    }
    
}
