using UnityEngine;

public class PhysicsCheck : MonoBehaviour {
    public bool isGround;
    public float checkRaduis;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;

    void Update() {
        Check();
    }

    public void Check() {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
        // OnDrawGizmos
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}
