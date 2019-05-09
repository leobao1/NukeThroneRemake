using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed;
    public Transform player;
    public Transform gun;

    private Rigidbody2D rb;

    float moveHorizontal;
    float moveVertical;

    Vector3 playerPos;
    Vector3 mousePos;
    Vector3 mouseVec; //points from player to mouse

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        GetInputs();
        MovePlayer();
        RotateGun();
    }

    void GetInputs() {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        playerPos = player.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        mouseVec = (mousePos - playerPos).normalized;
    }

    void MovePlayer() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * speed * Time.deltaTime;
    }

    void RotateGun() {
        float angle = -1 * Mathf.Atan2(mouseVec.y, mouseVec.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        //gun.eulerAngles = new Vector3(0, 0, angle);
        Debug.Log(angle);
    }
}