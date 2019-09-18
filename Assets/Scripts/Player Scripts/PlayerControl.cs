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
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        playerPos = transform.position;
    }

    void MovePlayer() {
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        if (movement.magnitude > Vector2.one.magnitude) {
            movement.Normalize();
        }

        //TODO: this is a super hacky way to test for the dash, make it better
        if (rb.velocity.magnitude < Vector2.one.magnitude * 10f) {//the 10f is 2/3 of the dash speed of x15f
            rb.velocity = movement * speed;
        }
    }
}