using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
    public InputController inputControl;
    public PhysicsCheck physicsCheck;
    public PlayerAnimation playerAnimation;

    [Header("基本参数")]
    public float moveSpeed = 3;

    private float runSpeed;
    private float walkSpeed;

    private CapsuleCollider2D coll;

    public float jumpForce = 100;

    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;

    public PhysicsMaterial2D normalMaterial;
    public PhysicsMaterial2D wallMaterial;

    [Header("Input 参数")]
    public Vector2 dirCommand;

    public bool jumpCommand;
    public bool crouchCommand;
    public bool sliderCommand;
    public bool attackCommand;

    public Vector2 originalOffser;
    public Vector2 originalSize;
    public Vector2 crouchOffset;
    public Vector2 crouchSize;

    public float hurtForce;

    [Header("状态")]
    public bool isCourch;

    public bool isHurt;
    public bool isAttack;
    public bool isDead;


    // other
    public Transform attackArea;
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
        inputControl.playerInputControl.Gameplay.Attack.started += PlayerAttack;
    }

    void Start() {
        inputControl = inputControl.instance;
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        coll = GetComponent<CapsuleCollider2D>();
        originalOffser = coll.offset;
        originalSize = coll.size;
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update() {
        CheckState();
        // sync input
        dirCommand = inputControl.inputDirecion;
        jumpCommand = inputControl.jump;
        crouchCommand = inputControl.crouch;
        sliderCommand = inputControl.slider;
        attackCommand = inputControl.attack;
    }

    private void FixedUpdate() {
        if (!isHurt && !isAttack) {
            Move();
        }
    }

    public void Move() {
        if (dirCommand.x < 0) {
            var scale = attackArea.localScale;
            scale.x = -1;
            attackArea.localScale = scale;
            spriteRenderer.flipX = true;
        } else if (dirCommand.x > 0) {
            var scale = attackArea.localScale;
            scale.x = 1;
            attackArea.localScale = scale;
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

    public void GetHurt(Transform attacker) {
        isHurt = true;
        rigidbody2D.velocity = Vector2.zero;
        var dirX = transform.position.x - attacker.position.x;
        var dirV2 = new Vector2(dirX, 0f);
        Vector2 dir = dirV2.normalized;

        rigidbody2D.AddForce(dir, ForceMode2D.Impulse);
    }

    public void PlayerDie() {
        isDead = true;
        inputControl.playerInputControl.Disable();
    }

    private void CheckState() {
        rigidbody2D.sharedMaterial = physicsCheck.isGround ? normalMaterial : wallMaterial;
        if (isDead)
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void PlayerAttack(InputAction.CallbackContext obj) {
        playerAnimation.PlayAttack();
        isAttack = true;
    }
}