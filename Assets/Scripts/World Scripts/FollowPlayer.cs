using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform followTarget;
    public float speed;//speed doesnt do anything rn
    public float maxDist;

    float cameraZ;
    Vector3 targetPos, mousePos, refVel;


    private Vector3 prev;

    void Start()
    {
        cameraZ = gameObject.transform.position.z;
    }

    void Update() {
        GetMouse(); 
        GetNewPos();
        MoveCamera();
    }

    void GetMouse() {
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos *= 2;
        mousePos -= Vector3.one;
    }

    void GetNewPos() {
        targetPos = mousePos * maxDist + followTarget.position;
        targetPos.z = cameraZ;
    }

    void MoveCamera() {
        //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref refVel, speed);
        //the above line screws stuff up, makes it really jittery
        transform.position = targetPos;
    }
}
