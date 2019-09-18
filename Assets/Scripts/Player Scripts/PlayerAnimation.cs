using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer spr;
    Vector2 mouseVec;
    bool moving;

    void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update() {
        Vector2 playerPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVec = (mousePos - playerPos).normalized;

        moving = (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"));
        
        anim.SetBool("isMoving", moving);
        if (mouseVec.x < 0) {
            spr.flipX = true;
        }
        else {
            spr.flipX = false;
        }
    }
}
