using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject followTarget;
    public float speed;

    private Vector3 prev;

    void Start()
    {
        prev = followTarget.transform.position;
    }

    void LateUpdate() {
        /*Vector3 cursorOffset = new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,
             Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0f);
        cursorOffset += (followTarget.transform.position - prev);
        transform.position += cursorOffset;
        prev = followTarget.transform.position;*/

        Vector3 cursorOffset = new Vector3(0f, 0f, 0f);
        cursorOffset += (followTarget.transform.position - prev);
        transform.position += cursorOffset;
        prev = followTarget.transform.position;

        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
    }
}
