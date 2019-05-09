using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject proj;
    public float speed;

    void Update() {
        if (Input.GetMouseButtonDown(0)) { 
            Vector2 clickPos = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            GameObject clone;
            clone = Instantiate(proj, transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().velocity = new Vector2(clickPos.x - transform.position.x, clickPos.y - transform.position.y);
        }
    }
}
