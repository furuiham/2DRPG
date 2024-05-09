using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour {
    [Header("基本属性")]
    public float MaxHealth;
    public float currentHealth;
    public float invincibleTime;
    private float invincibleTimer;

    private void Start() {
        currentHealth = MaxHealth;
    }

    private void Update() {
        if (invincibleTimer > 0) {
            invincibleTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(Attack attacker) {
        if (invincibleTimer > 0) {
            return;
        }

        invincibleTimer += invincibleTime;
        Debug.Log(attacker.damage);
        currentHealth -= attacker.damage;
    }
}