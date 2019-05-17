using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProj : MonoBehaviour
{
    public float speed; //this will be changed to non-public later

    Vector3 direc;
    //float damage;

    Rigidbody2D rb;

    public void Construct(Vector3 target) {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = target * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }
}
