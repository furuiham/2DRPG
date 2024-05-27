using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    protected Rigidbody2D rb;
    protected Animator anim;

    [Header("基本参数")]
    public float normalSpeed;

    public PhysicsCheck physicsCheck;

    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;

    [Header("计时器")]
    public float waitTime;
    public bool wait;
    public float waitTimeCounter;
    public void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
    }

    private void Update() {
        faceDir = new Vector3(-transform.localScale.x, 0f, 0f);

        if ((physicsCheck.touchLeftWall && faceDir.x < 0f) || (physicsCheck.touchRightWall && faceDir.x > 0f)) {
            wait = true;
            anim.SetBool("walk", false);
        }
        TimeCounter();
    }

    public void FixedUpdate() {
        Move();
    }

    public virtual void Move() {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    public void TimeCounter() {
        if (wait) {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0) {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
    }
}