using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenCust : MonoBehaviour {
    int roomWidth, roomHeight;

    Vector2 roomSize = new Vector2(30, 30);
    float tileSize = 1;//These two can be made public later, for now keep it as is

    public enum SpaceType { floor, wall };
    SpaceType[,] roomTiles;

    List<walker> walkerList;
    public float chanceChangeDir, chanceSpawn, chanceDestoy;//Default: 0.5,0.05,0.05
    /*To use these vars:
     * chanceWalkerChange: Lower values will give more tunnels
     * chanceWalkerSpawn: Higher values will give more bigger spaces
     * chanceWalkerDestoy: Shorter tunnels, less branchy
     */
    public int maxWalkers; //Default:10
    public float maxPercent; // Default: 0.2
    public GameObject wallObj, floorObj;

    struct walker {
        public Vector2 pos;
        public Vector2 dir;
    }

    void Start() {
        Instantiate();
        drawFloor();
        createStage();
    }

    void Instantiate() {
        roomWidth = Mathf.RoundToInt(roomSize.x / tileSize);
        roomHeight = Mathf.RoundToInt(roomSize.y / tileSize);

        roomTiles = new SpaceType[roomWidth, roomHeight];
        for(int i=0; i < roomHeight; i++) {
            for(int j=0; j < roomWidth; j++) {
                roomTiles[j, i] = SpaceType.wall;
            }
        }

        int centerX, centerY;
        centerX = Mathf.RoundToInt(roomWidth / 2);
        centerY = Mathf.RoundToInt(roomHeight / 2);

        walkerList = new List<walker>();

        walker initWalker = new walker();
        initWalker.dir = randDirec();
        initWalker.pos = new Vector2(centerX, centerY);

        walkerList.Add(initWalker);
    }

    void drawFloor() {
        int floors = 0;
        int totalTiles = roomWidth * roomHeight;
        int iterations = 0;
        while ((float)floors/(float)totalTiles < maxPercent && iterations < 100000) {

            int currWalker = 0;
            for (int i=0; i<walkerList.Count; i++) {
                walker thisWalker = walkerList[currWalker];
                Debug.Log("Curr walker pos");
                Debug.Log((int)thisWalker.pos.x);
                Debug.Log((int)thisWalker.pos.y);
                if (roomTiles[(int)thisWalker.pos.x, (int)thisWalker.pos.y] == SpaceType.wall) {
                    roomTiles[(int)thisWalker.pos.x, (int)thisWalker.pos.y] = SpaceType.floor;
                    floors++;
                }

                if (walkerList.Count > 1 && Random.value < chanceDestoy) {
                    walkerList.RemoveAt(currWalker);
                    continue;
                }

                if (walkerList.Count < maxWalkers && Random.value < chanceSpawn) {
                    walker newWalker = new walker();
                    newWalker.dir = randDirec();
                    newWalker.pos = new Vector2((int)thisWalker.pos.x, (int)thisWalker.pos.y);

                    walkerList.Add(newWalker);
                }

                if (Random.value < chanceChangeDir) {
                    thisWalker.dir = randDirec();
                }

                thisWalker.pos += thisWalker.dir;
                if (thisWalker.pos.x >= roomWidth-1 || thisWalker.pos.y >= roomHeight-1 || thisWalker.pos.x <= 0 || thisWalker.pos.y <= 0) {
                    thisWalker.pos -= thisWalker.dir;
                }
                walkerList[currWalker] = thisWalker;
                currWalker++;
            }
        }
    }

    void createStage() {
        for(int i=0; i<roomHeight; i++) {
            for(int j=0; j<roomWidth; j++) {
                switch (roomTiles[i,j]) {
                    case SpaceType.floor:
                        createTile(i, j, floorObj);
                        break;

                    case SpaceType.wall:
                        createTile(i, j, wallObj);
                        break;

                    default:
                        Debug.Log("default case hit in createStage() LevelGenCust");
                        break;
                }
            }
        }
    }

    void createTile(int x, int y, GameObject tileType) {
        Debug.Log("create");
        int centerX, centerY;
        centerX = Mathf.RoundToInt(roomWidth / 2);
        centerY = Mathf.RoundToInt(roomHeight / 2);

        Vector2 centerOffset = new Vector2(centerX, centerY);
        Vector2 spawnPos = new Vector2(x, y);
        Vector2 spawnWithOffset = tileSize * (spawnPos - centerOffset);

        Instantiate(tileType, spawnWithOffset, Quaternion.identity);
    }

    Vector2 randDirec() {
        int randInt = Mathf.FloorToInt(Random.value * 4);
        Vector2 returnDirec;
        switch (randInt) {
            case 0:
                returnDirec = Vector2.up;
                break;

            case 1:
                returnDirec = Vector2.down;
                break;

            case 2:
                returnDirec = Vector2.left;
                break;

            case 3:
                returnDirec = Vector2.right;
                break;

            default:
                returnDirec = Vector2.zero;
                Debug.Log("Default case hit in randDirec() LevelGenCust");
                break;
        }
        return returnDirec;
    }
}