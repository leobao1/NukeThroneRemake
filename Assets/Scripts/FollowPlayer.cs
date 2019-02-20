using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject followTarget;

    public Vector3 offset;

    void Start()
    {
        offset = transform.position - followTarget.transform.position;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = followTarget.transform.position + offset;
    }
}
