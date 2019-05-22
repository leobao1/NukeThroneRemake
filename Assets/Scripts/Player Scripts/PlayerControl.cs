using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    float speed;

    private Rigidbody2D rb;

    float moveHorizontal;
    float moveVertical;

    Vector3 playerPos;

    void Start() {
        PlayerData init = GetComponent<PlayerData>();
        speed = init.GetSpeed();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Inputs();
        MovePlayer();
    }

    void Inputs() {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        playerPos = transform.position;
    }

    void MovePlayer() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if (!(rb.velocity.magnitude > Vector2.one.magnitude * speed * Time.deltaTime)) {
            rb.velocity = movement * speed * Time.deltaTime;
        }
    }
}