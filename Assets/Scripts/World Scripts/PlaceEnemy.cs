using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceEnemy : MonoBehaviour
{
    LevelGenCust.SpaceType[,] grid;

    void Start()
    {
        grid = GetComponent<LevelGenCust>().GetGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
