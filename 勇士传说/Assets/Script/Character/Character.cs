using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour {
    [Header("基本属性")]
    public float MaxHealth;
    public float currentHealth;
    public float invincibleTime;
    private float invincibleTimer;

    public UnityEvent<Transform> OnTakeDamage;

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

        if (currentHealth < attacker.damage) {
            currentHealth = 0;
            Debug.Log("die");
            // CharacterDie
            return;
        }

        OnTakeDamage?.Invoke(attacker.transform);
        invincibleTimer += invincibleTime;
        Debug.Log(attacker.damage);
        currentHealth -= attacker.damage;
    }
}