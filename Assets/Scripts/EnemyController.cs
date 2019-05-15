using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public Transform player;

    private Rigidbody2D rb;

    float moveHorizontal;
    float moveVertical;

    Vector3 playerPos;

    // Update is called once per frame
    void Update()
    {
        
    }
}
