using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    protected Rigidbody2D rb;
    protected Animator anim;

    [Header("基本参数")]
    public float normalSpeed;

    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;

    public void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    private void Update() {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
    }

    public void FixedUpdate() {
        Move();
    }

    public virtual void Move() {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
}