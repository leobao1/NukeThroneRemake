using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProj : MonoBehaviour
{
    public float speed; //this will be changed to non-public later

    Vector3 direc;
    int damage;

    Rigidbody2D rb;

    public void Construct(Vector3 target, int dmg) {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = target * speed * Time.deltaTime;
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Player")) {
            PlayerData hitTarget = collision.gameObject.GetComponent<PlayerData>();
            hitTarget.DamagePlayer(damage);
            Destroy(gameObject);
        }
    }
}
