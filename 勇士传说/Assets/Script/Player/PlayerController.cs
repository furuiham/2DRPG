using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
    public InputController inputControl;
    public PhysicsCheck physicsCheck;

    public float moveSpeed = 3;
    public float jumpForce = 100;

    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;

    [Header("Input 参数")]
    // input command
    public Vector2 dirCommand;
    public bool jumpCommand;
    public bool crouchCommand;
    public bool sliderCommand;

    void Start() {
        // ???
        inputControl = inputControl.instance;
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    // Update is called once per frame
    void Update() {
        // sync input
        dirCommand = inputControl.inputDirecion;
        jumpCommand = inputControl.jump;
        crouchCommand = inputControl.crouch;
        sliderCommand = inputControl.slider;
    }

    private void FixedUpdate() {
        Move();
    }

    public void Move() {
        if (dirCommand.x < 0) {
            spriteRenderer.flipX = true;
        } else if (dirCommand.x > 0) {
            spriteRenderer.flipX = false;
        }

        rigidbody2D.velocity = new Vector2(dirCommand.x * moveSpeed * Time.deltaTime, rigidbody2D.velocity.y);

        if (jumpCommand && physicsCheck.isGround) {
            rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}