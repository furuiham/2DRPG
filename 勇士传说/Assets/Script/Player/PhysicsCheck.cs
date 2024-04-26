using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PhysicsCheck : MonoBehaviour {
    public bool isGround;
    public float checkRaduis;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    void Start()
    {

    }

    // Update is called once per frame
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
