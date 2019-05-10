using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour {
    public Transform player;
    public Transform gun;
    public Transform gunTip;
    public GameObject projectile;
    public float inaccuracy;
    public float attackSpeed;

    Vector3 playerPos;
    Vector3 mousePos;
    Vector3 mouseVec; //points from player to mouse
    bool gunPress;
    bool canAttack;
    float angle;
    float lastShot;

    void Start() {
        lastShot = -1;
    }

    void Update() {
        Inputs();
        RotateGun();
        Shoot();
    }

    void Inputs() {
        playerPos = player.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        mouseVec = (mousePos - playerPos).normalized;
        gunPress = Input.GetMouseButton(0);
    }

    void RotateGun() {
        angle = Mathf.Atan2(mouseVec.y, mouseVec.x) * Mathf.Rad2Deg;
        gun.eulerAngles = new Vector3(0, 0, angle);
    }

    void Shoot() {
        canAttack = (Time.time - lastShot) > attackSpeed;
        if (gunPress && canAttack) {
            float angleShift = Random.Range(-1*inaccuracy, inaccuracy);
            float projAngle = angle + angleShift;
            Quaternion rotation = Quaternion.Euler(0, 0, projAngle);
            Proj newProj = Instantiate(projectile, gunTip.position, rotation).GetComponent<Proj>();
            float projAngleRad = projAngle * Mathf.Deg2Rad;
            Vector3 projVec = new Vector3(Mathf.Cos(projAngleRad), Mathf.Sin(projAngleRad), 0);
            newProj.Construct(projVec);
            lastShot = Time.time;
        }
    }
}