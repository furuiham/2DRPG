using System;
using System.Collections;
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
    public Transform attacker;
    public bool isHurt;

    [Header("计时器")]
    public float waitTime;

    public bool wait;
    public float waitTimeCounter;
    public float hurtForce;
    public bool isDead;

    public void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
    }

    public void Update() {
        faceDir = new Vector3(-transform.localScale.x, 0f, 0f);

        if ((physicsCheck.touchLeftWall && faceDir.x < 0f) || (physicsCheck.touchRightWall && faceDir.x > 0f)) {
            wait = true;
            anim.SetBool("walk", false);
        }

        TimeCounter();
    }

    public void FixedUpdate() {
        if (isHurt) {
            return;
        }

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

    public void OnTakeDamage(Transform attackTrans) {
        attacker = attackTrans;
        // 转身
        if (attackTrans.position.x - transform.position.x > 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (attackTrans.position.x - transform.position.x < 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // 受伤被击退
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = (Vector2)transform.position - (Vector2)attacker.position;
        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir) {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void OnDie() {
        anim.SetBool("dead", true);
        gameObject.layer = 2;
        isDead = true;
    }

    public void DestoryAfterAnimation() {
        Destroy(this.gameObject);
    }
}