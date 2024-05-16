using UnityEngine;

public class Attack : MonoBehaviour {
    public int damage;
    public float attacRange;
    public float attackRate;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.GetComponent<Character>() == null) {
            Debug.Log($"{this.gameObject.name} 2 {other.gameObject.name} trigger null");
            return;
        }

        other.GetComponent<Character>().TakeDamage(this);
    }
}