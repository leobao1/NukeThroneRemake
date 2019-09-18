using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucian : MonoBehaviour
{

    public float dashCooldown;
    float lastDash;
    public Transform gun;
    public Transform gunTip;
    public GameObject projectile;
    public float inaccuracy;
    public float attackDelay;

    Rigidbody2D rb;
    Vector3 playerPos;
    Vector3 mousePos;
    Vector3 mouseVec; //points from player to mouse
    bool gunPress;
    bool canAttack;
    bool doubleShoot;
    float angle;
    float lastShot;
    int damage;

    void Start() {
        PlayerData init = GetComponent<PlayerData>();
        damage = init.GetDamage();
        lastShot = -1;

        rb = GetComponent<Rigidbody2D>();
        lastDash = -1 * dashCooldown;
    }

    void Update() {
        Inputs();
        RotateGun();
        Shoot();

        //clean up this part later, super ugly but it works
        if (Input.GetKeyDown(KeyCode.LeftShift) && (lastDash + dashCooldown <= Time.time)) {
            doubleShoot = true;
            Dash();
        }
    }

    void Inputs() {
        playerPos = transform.position;
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
        canAttack = (Time.time - lastShot) > attackDelay;
        if (gunPress && canAttack) {
            float angleShift = Random.Range(-1 * inaccuracy, inaccuracy);
            float projAngle = angle + angleShift;
            Quaternion rotation = Quaternion.Euler(0, 0, projAngle);
            Proj newProj = Instantiate(projectile, gunTip.position, rotation).GetComponent<Proj>();
            float projAngleRad = projAngle * Mathf.Deg2Rad;
            Vector3 projVec = new Vector3(Mathf.Cos(projAngleRad), Mathf.Sin(projAngleRad), 0);
            newProj.Construct(projVec, damage);
            if (doubleShoot) {
                StartCoroutine(SecondShot());
                doubleShoot = false;
            }
            lastShot = Time.time;
        }
    }


//---------------------------------------------------------------------------------------------------------------------------------

    void Dash() {
        Vector2 direc = rb.velocity;
        lastDash = Time.time;
        if (direc.magnitude == 0) {
            StartCoroutine(DashWait());
        }
        else {
            StartCoroutine(DashWait());
        }
    }

    void Laser() {

    }

    IEnumerator DashWait() {
        Vector2 orig = rb.velocity;
        if (orig.magnitude < 0.1f) {
            rb.velocity = mouseVec.normalized * 15f;
        } else {
            rb.velocity = orig.normalized * 15f;
        }
        yield return new WaitForSeconds(0.1f);
        rb.velocity = orig;
    }

    IEnumerator SecondShot() {
        yield return new WaitForSeconds(attackDelay/5);
        float angleShift = Random.Range(-1 * inaccuracy, inaccuracy);
        float projAngle = angle + angleShift;
        Quaternion rotation = Quaternion.Euler(0, 0, projAngle);
        Proj newProj = Instantiate(projectile, gunTip.position, rotation).GetComponent<Proj>();
        float projAngleRad = projAngle * Mathf.Deg2Rad;
        Vector3 projVec = new Vector3(Mathf.Cos(projAngleRad), Mathf.Sin(projAngleRad), 0);
        newProj.Construct(projVec, damage);
    }
}
