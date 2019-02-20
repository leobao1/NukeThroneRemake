using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenCust : MonoBehaviour
{
    public GameObject wall;
    public GameObject floor;

    int width, height;
    float amountOfGrids;
    Vector2 roomSize = new Vector2(30, 30);

    enum spaceType {empty, floor, wall };

    struct walker {
        public Vector2 pos;
        public Vector2 dir;
    }



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
