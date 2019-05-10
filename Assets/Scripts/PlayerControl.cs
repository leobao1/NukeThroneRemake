using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed;
    public Transform player;

    private Rigidbody2D rb;

    float moveHorizontal;
    float moveVertical;

    Vector3 playerPos;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Inputs();
        MovePlayer();
    }

    void Inputs() {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        playerPos = player.position;
    }

    void MovePlayer() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * speed * Time.deltaTime;
    }
}