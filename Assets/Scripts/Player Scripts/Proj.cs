using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj : MonoBehaviour
{
    public float speed; //this will be changed to non-public later

    Vector3 direc;
    int damage;

    Rigidbody2D rb;

    public void Construct(Vector3 target, int dmg) {
        rb = GetComponent<Rigidbody2D>();
        damage = dmg;
        rb.velocity = target * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Enemy")) {
            EnemyData dmg = collision.gameObject.GetComponent<EnemyData>();
            dmg.DamageEnemy(damage);
            Destroy(gameObject);
        }
    }

}
