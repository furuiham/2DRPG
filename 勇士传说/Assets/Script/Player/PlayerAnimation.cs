using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    public PhysicsCheck physicsCheck;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start() {
        animator = this.GetComponent<Animator>();
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        physicsCheck = this.GetComponent<PhysicsCheck>();
        playerController = this.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        SetAnimation();
    }

    public void SetAnimation() {
        animator.SetFloat("VelocityX", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("VelocityY", Mathf.Abs(rigidbody2D.velocity.y));
        animator.SetBool("IsGround", physicsCheck.isGround);
        animator.SetBool("IsCrouch", playerController.isCourch);
    }
}