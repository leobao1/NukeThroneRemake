using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed;
    public Transform player;
    public Transform gun;
    public Transform gunTip;
    public GameObject projectile;

    private Rigidbody2D rb;

    float moveHorizontal;
    float moveVertical;

    Vector3 playerPos;
    Vector3 mousePos;
    Vector3 mouseVec; //points from player to mouse
    float angle;
    bool gunPress;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        GetInputs();
        MovePlayer();
        RotateGun();
        Shoot();
    }

    void GetInputs() {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        playerPos = player.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        mouseVec = (mousePos - playerPos).normalized;
        gunPress = Input.GetMouseButtonDown(0);
    }

    void MovePlayer() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * speed * Time.deltaTime;
    }

    void RotateGun() {
        angle = Mathf.Atan2(mouseVec.y, mouseVec.x) * Mathf.Rad2Deg;
        gun.eulerAngles = new Vector3(0, 0, angle);
    }

    void Shoot() {
        if (gunPress) {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Proj newProj = Instantiate(projectile, gunTip.position, rotation).GetComponent<Proj>(); ;
            newProj.Construct(mouseVec);
        }
    }
}