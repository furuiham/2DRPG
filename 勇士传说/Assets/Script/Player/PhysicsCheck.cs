using System;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour {
    private CapsuleCollider2D collider2D;
    public bool manual;
    public bool isGround;
    public float checkRaduis;
    public LayerMask groundLayer;

    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    public bool touchLeftWall;
    public bool touchRightWall;

    private void Awake() {
        collider2D = GetComponent<CapsuleCollider2D>();
        if (!manual) {
            rightOffset = new Vector2((collider2D.bounds.size.x + collider2D.offset.x), collider2D.bounds.size.y / 2);
        }
    }

    void Update() {
        Check();
    }

    public void Check() {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}