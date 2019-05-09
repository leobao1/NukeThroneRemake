using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    void Update() {
        /*Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        transform.RotateAround(transform.parent.gameObject.transform.position, Vector3.forward, 20*Time.deltaTime);*/

        transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
    }
}
