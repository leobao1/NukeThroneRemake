using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject followTarget;
    public float speed;
    public Vector3 offset;

    private Vector3 prev;

    void Start()
    {
        offset = transform.position - followTarget.transform.position;
        prev = followTarget.transform.position;
    }

    void LateUpdate() {
        Vector3 cursorOffset = new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,
             Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0f);
        cursorOffset += (followTarget.transform.position - prev);
        /*Camera cam = gameObject.GetComponent<Camera>();
        cursorOffset.x = ((Screen.width / 2) - (Input.mousePosition.x));
        cursorOffset.y = ((Screen.height / 2) - (Input.mousePosition.y));
        Vector3 finalOffset = cam.ScreenToWorldPoint(cursorOffset);
        Debug.Log(finalOffset);*/
        transform.position += cursorOffset;
        prev = followTarget.transform.position;
    }
}
