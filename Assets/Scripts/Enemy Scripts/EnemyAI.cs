using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only controls the AI of enemy, all data on enemy is kept in a EnemyData file
public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Transform gun;
    public Transform gunTip;
    public float delayBetweenActions;
    public GameObject projectile;
    public float inaccuracy;

    float speed;
    int damage;

    private Rigidbody2D rb;

    enum States { Start, Attack, Idle, Walk };
    States currState;
    bool aggresive;

    InRange vision;

    float lastAction;

    float angle;

    // Update is called once per frame
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        lastAction = Time.time;
        currState = States.Start;
        vision = GetComponentInChildren<InRange>();
        aggresive = vision.getVision();
        EnemyData init = GetComponent<EnemyData>();
        speed = init.GetSpeed();
        damage = init.GetDamage();
    }

    private void Update() {
        DetermineAgressive();
        DetermineAction();
        TurnEnemy();
    }

    void DetermineAgressive() {
        if (!aggresive) { //This statement could be made more efficient?
            aggresive = vision.getVision();
            if (aggresive) {
                Debug.Log("Aggresive!");
                currState = States.Start;
                lastAction -= delayBetweenActions;
            }
        }
    }

    void TurnEnemy() {
        if (aggresive) {
            Vector2 vecToPlayer = player.position - transform.position;
            angle = Mathf.Atan2(vecToPlayer.y, vecToPlayer.x) * Mathf.Rad2Deg;
            gun.eulerAngles = new Vector3(0, 0, angle);
        }
        else {
            Vector2 turnVec = rb.velocity;
            if (turnVec.magnitude == 0) {
                return;
            }
            angle = Mathf.Atan2(turnVec.y, turnVec.x) * Mathf.Rad2Deg;
            gun.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    /*AI description: When aggresive, should randomly alternate between attacking, walking around and idle. After each attack, it MUST 
     * go to idle. From Idle and Walk, it can go to whichever of the other 2. Will not attack immediatly upon being aggresive
     * Non aggresive will just walk around 
     * In addition, enemies may proceed with an action immediatly after attacking BUT NOT IMMEDIATLY AFTER WALKING OR IDLE*/
    void DetermineAction() {

        if (!(Time.time - lastAction > delayBetweenActions)) {
            return;
        }

        if (aggresive) {
            switch (currState) {
                case States.Start:
                    currState = (States)Random.Range(2,4);
                    break;

                case States.Attack:
                    //Debug.Log("Attack");
                    Attack(); 
                    currState = States.Idle;
                    break;

                case States.Idle:
                    //Debug.Log("Idle");
                    Idle();
                    currState = (States)Random.Range(1, 4);
                    lastAction = Time.time;
                    break;

                case States.Walk:
                    //Debug.Log("Walk");
                    Move();
                    currState = (States)Random.Range(1, 4);
                    lastAction = Time.time;
                    break;

                default:
                    Debug.Log("Error, Default state reached in EnemyController, Aggresive state");
                    currState = States.Start;
                    break;
            }
        }//Aggresive AI ends here-----------------------------------------------------------------------
        else {
            switch (currState) {
                case States.Start:
                    currState = (States)Random.Range(2, 4);
                    break;

                case States.Attack:
                    Debug.Log("Error, Attack state reached in EnemyController, Non-Aggresive state");
                    currState = States.Start;
                    break;

                case States.Idle:
                    //Debug.Log("Idle");
                    Idle();
                    currState = (States)Random.Range(2, 4);
                    lastAction = Time.time;
                    break;

                case States.Walk:
                    //Debug.Log("Walk");
                    Move();
                    currState = (States)Random.Range(2, 4);
                    lastAction = Time.time;
                    break;

                default:
                    Debug.Log("Error, Default state reached in EnemyController, Non-Aggresive state");
                    currState = States.Start;
                    break;
            }//End of Non-Aggresiv---------------------------------------------
        }
    }//State Machine Ends Here ---------------------------------------------------------------------

    void Move() {
        Vector2 movement = Random.insideUnitCircle;
        rb.velocity = movement * speed;
    }

    void Idle() {
        rb.velocity = Vector2.zero;
    }

    void Attack() {
        float angleShift = Random.Range(-1 * inaccuracy, inaccuracy);
        float projAngle = angle + angleShift;
        Quaternion rotation = Quaternion.Euler(0, 0, projAngle);
        EnemyProj newProj = Instantiate(projectile, gunTip.position, rotation).GetComponent<EnemyProj>();
        float projAngleRad = projAngle * Mathf.Deg2Rad;
        Vector3 projVec = new Vector3(Mathf.Cos(projAngleRad), Mathf.Sin(projAngleRad), 0);
        newProj.Construct(projVec);
    }
}
