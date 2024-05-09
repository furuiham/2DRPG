using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public InputController inputControl;
    public PhysicsCheck physicsCheck;

    [Header("基本参数")]
    public float moveSpeed = 3;

    private float runSpeed;

    // private float runSpeed => moveSpeed;
    private float walkSpeed;
    // private float walkSpeed => moveSpeed / 2.5f;

    private CapsuleCollider2D coll;

    public float jumpForce = 100;

    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;

    [Header("Input 参数")]
    // input command
    public Vector2 dirCommand;

    public bool jumpCommand;
    public bool crouchCommand;
    public bool sliderCommand;

    public bool isCourch;

    public Vector2 originalOffser;
    public Vector2 originalSize;

    public Vector2 crouchOffset;
    public Vector2 crouchSize;
    private void Awake() {
        runSpeed = moveSpeed;
        walkSpeed = moveSpeed / 2.5f;
        // 可以用上一帧来处理
        inputControl.playerInputControl.Gameplay.Slider.performed += ctx => {
            if (physicsCheck.isGround) {
                moveSpeed = walkSpeed;
            }
        };

        inputControl.playerInputControl.Gameplay.Slider.canceled += ctx => {
            if (physicsCheck.isGround) {
                moveSpeed = runSpeed;
            }
        };
    }

    void Start() {
        // ???
        inputControl = inputControl.instance;
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        coll = GetComponent<CapsuleCollider2D>();
        originalOffser = coll.offset;
        originalSize = coll.size;
    }

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

        var targetVel = new Vector2(dirCommand.x * moveSpeed * Time.deltaTime, rigidbody2D.velocity.y);
        rigidbody2D.velocity = targetVel;

        if (jumpCommand && physicsCheck.isGround) {
            rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        isCourch = crouchCommand;

        if (isCourch) {
            coll.offset = crouchOffset;
            coll.size = crouchSize;
        } else {
            coll.offset = originalOffser;
            coll.size = originalSize;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        // Debug.Log(other.name);
    }
}