using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Determines if this specific enemy has seen the player, walls will obstruct vision
//This could be combined into EnemyController? keep it as is for now
public class InRange : MonoBehaviour
{
    public Transform player;

    bool sawPlayer = false;
    int layerMask;

    private void Start() {
        int layerNum1 = LayerMask.NameToLayer("Wall");

        if (layerNum1 == -1) {
            Debug.Log("invalid layer name");
        }

        layerMask = (1 << layerNum1);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && !sawPlayer) {
            Vector3 curr = gameObject.transform.position;
            Vector3 direc = player.position - curr;
            sawPlayer = !Physics2D.Raycast(curr, direc, direc.magnitude, layerMask);
            //Debug.DrawRay(curr, direc, Color.red, 0.05f, true);
            //Debug.Log(sawPlayer);
        }
    }

    public bool getVision() {
        return sawPlayer;
    }
}
