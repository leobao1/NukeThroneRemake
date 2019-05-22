using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenCust : MonoBehaviour
{
    int roomWidth, roomHeight;

    Vector2 roomSize = new Vector2(30, 30);
    float worldUnitsInOneGridCell = 1;//These two can be made public later, for now keep it as is

    public enum SpaceType {empty, floor, wall };
    SpaceType[,] grid;

    List<walker> walkers;
    public float chanceWalkerChangeDir, chanceWalkerSpawn, chanceWalkerDestoy;//Default: 0.5,0.05,0.05
    /*To use these vars:
     * chanceWalkerChange: Lower values will give more tunnels
     * chanceWalkerSpawn: Higher values will give more bigger spaces
     * 
     */
    public int maxWalkers; //Default:10
    public float percentToFill; // Default: 0.2
    public GameObject wallObj, floorObj;

    struct walker {
        public Vector2 pos;
        public Vector2 dir;
    }

    void Start() {
        Setup();
        CreateFloors();
        CreateWalls();
        RemoveSingleWalls();
        SpawnLevel();
    }
    void Setup() {
        //find grid size
        roomHeight = Mathf.RoundToInt(roomSize.x / worldUnitsInOneGridCell);
        roomWidth = Mathf.RoundToInt(roomSize.y / worldUnitsInOneGridCell);
        //create grid
        grid = new SpaceType[roomWidth, roomHeight];
        //set grid's default state
        for (int x = 0; x < roomWidth - 1; x++) {
            for (int y = 0; y < roomHeight - 1; y++) {
                //make every cell "empty"
                grid[x, y] = SpaceType.empty;
            }
        }
        //set first walker
        //init list
        walkers = new List<walker>();
        //create a walker 
        walker newWalker = new walker();
        newWalker.dir = RandomDirection();
        //find center of grid
        Vector2 spawnPos = new Vector2(Mathf.RoundToInt(roomWidth / 2.0f),
                                        Mathf.RoundToInt(roomHeight / 2.0f));
        newWalker.pos = spawnPos;
        //add walker to list
        walkers.Add(newWalker);
    }
    void CreateFloors() {
        int iterations = 0;//loop will not run forever
        do {
            //create floor at position of every walker
            foreach (walker myWalker in walkers) {
                grid[(int)myWalker.pos.x, (int)myWalker.pos.y] = SpaceType.floor;
            }
            //chance: destroy walker
            int numberChecks = walkers.Count; //might modify count while in this loop
            for (int i = 0; i < numberChecks; i++) {
                //only if its not the only one, and at a low chance
                if (Random.value < chanceWalkerDestoy && walkers.Count > 1) {
                    walkers.RemoveAt(i);
                    break; //only destroy one per iteration
                }
            }
            //chance: walker pick new direction
            for (int i = 0; i < walkers.Count; i++) {
                if (Random.value < chanceWalkerChangeDir) {
                    walker thisWalker = walkers[i];
                    thisWalker.dir = RandomDirection();
                    walkers[i] = thisWalker;
                }
            }
            //chance: spawn new walker
            numberChecks = walkers.Count; //might modify count while in this loop
            for (int i = 0; i < numberChecks; i++) {
                //only if # of walkers < max, and at a low chance
                if (Random.value < chanceWalkerSpawn && walkers.Count < maxWalkers) {
                    //create a walker 
                    walker newWalker = new walker();
                    newWalker.dir = RandomDirection();
                    newWalker.pos = walkers[i].pos;
                    walkers.Add(newWalker);
                }
            }
            //move walkers
            for (int i = 0; i < walkers.Count; i++) {
                walker thisWalker = walkers[i];
                thisWalker.pos += thisWalker.dir;
                walkers[i] = thisWalker;
            }
            //avoid boarder of grid
            for (int i = 0; i < walkers.Count; i++) {
                walker thisWalker = walkers[i];
                //clamp x,y to leave a 1 space boarder: leave room for walls
                thisWalker.pos.x = Mathf.Clamp(thisWalker.pos.x, 1, roomWidth - 2);
                thisWalker.pos.y = Mathf.Clamp(thisWalker.pos.y, 1, roomHeight - 2);
                walkers[i] = thisWalker;
            }
            //check to exit loop
            if ((float)NumberOfFloors() / (float)grid.Length > percentToFill) {
                break;
            }
            iterations++;
        } while (iterations < 100000);
    }
    void CreateWalls() {
        //loop though every grid space
        for (int x = 0; x < roomWidth - 1; x++) {
            for (int y = 0; y < roomHeight - 1; y++) {
                //if theres a floor, check the spaces around it
                if (grid[x, y] == SpaceType.floor) {
                    //if any surrounding spaces are empty, place a wall
                    if (grid[x, y + 1] == SpaceType.empty) {
                        grid[x, y + 1] = SpaceType.wall;
                    }
                    if (grid[x, y - 1] == SpaceType.empty) {
                        grid[x, y - 1] = SpaceType.wall;
                    }
                    if (grid[x + 1, y] == SpaceType.empty) {
                        grid[x + 1, y] = SpaceType.wall;
                    }
                    if (grid[x - 1, y] == SpaceType.empty) {
                        grid[x - 1, y] = SpaceType.wall;
                    }
                }
            }
        }
    }
    void RemoveSingleWalls() {
        //loop though every grid space
        for (int x = 0; x < roomWidth - 1; x++) {
            for (int y = 0; y < roomHeight - 1; y++) {
                //if theres a wall, check the spaces around it
                if (grid[x, y] == SpaceType.wall) {
                    //assume all space around wall are floors
                    bool allFloors = true;
                    //check each side to see if they are all floors
                    for (int checkX = -1; checkX <= 1; checkX++) {
                        for (int checkY = -1; checkY <= 1; checkY++) {
                            if (x + checkX < 0 || x + checkX > roomWidth - 1 ||
                                y + checkY < 0 || y + checkY > roomHeight - 1) {
                                //skip checks that are out of range
                                continue;
                            }
                            if ((checkX != 0 && checkY != 0) || (checkX == 0 && checkY == 0)) {
                                //skip corners and center
                                continue;
                            }
                            if (grid[x + checkX, y + checkY] != SpaceType.floor) {
                                allFloors = false;
                            }
                        }
                    }
                    if (allFloors) {
                        grid[x, y] = SpaceType.floor;
                    }
                }
            }
        }
    }
    void SpawnLevel() {
        for (int x = 0; x < roomWidth; x++) {
            for (int y = 0; y < roomHeight; y++) {
                switch (grid[x, y]) {
                    case SpaceType.empty:
                        break;
                    case SpaceType.floor:
                        Spawn(x, y, floorObj);
                        break;
                    case SpaceType.wall:
                        Spawn(x, y, wallObj);
                        break;
                }
            }
        }
    }
    Vector2 RandomDirection() {
        //pick random int between 0 and 3
        int choice = Mathf.FloorToInt(Random.value * 3.99f);
        //use that int to chose a direction
        switch (choice) {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            default:
                return Vector2.right;
        }
    }
    int NumberOfFloors() {
        int count = 0;
        foreach (SpaceType space in grid) {
            if (space == SpaceType.floor) {
                count++;
            }
        }
        return count;
    }
    void Spawn(float x, float y, GameObject toSpawn) {
        //find the position to spawn
        Vector2 offset = roomSize / 2.0f;
        Vector2 spawnPos = new Vector2(x, y) * worldUnitsInOneGridCell - offset;
        //spawn object
        Instantiate(toSpawn, spawnPos, Quaternion.identity);
    }

    public SpaceType[,] GetGrid() {
        return grid;
    }
}
