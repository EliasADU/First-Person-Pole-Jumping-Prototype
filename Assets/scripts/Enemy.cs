using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform Player;
    public float MoveSpeed = 8f;
    public float MaxDist = 50f;
    public float MinDist = 15f;
    private bool alerted;

    private float visionRange;
    private float minDistanceNavPoint = 3.5f;
    private float visionConeAngle;
    private float distanceFromWall = 12f;

    private LayerMask layerMask;
    private Vector3 randomNavPoint;
    private Animator animator;
    private new Rigidbody rigidbody;

    private float damageTick; //Damage(x) = T* (r - x^3) / r
    private float T;
    private float damage;
    private float radius;

    private float timer;
    private float timeBetweenAttacks = 1.15f;

    public new Light light;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Walls", "Enemy");
    }

    void Start()
    {
        //initalize player to transform b/c easier to manipulate than controller.
        Player = References.player.transform;
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        damage = 10.1f;
        radius = 0.5f;
        T = 0.00015f;
        alerted = false;
        GoToRandomNavPoint();
        timer = 0.3f;
        visionRange = 25f;
        visionConeAngle = 180f;

    }

    // Update is called once per frame
    void Update()
    {
        EnemyLogic();
    }

    private void GoToRandomNavPoint()
    {
        if(References.navPoints.Count > 0)
        {
            int randomRange = Random.Range(0, References.navPoints.Count);
            randomNavPoint = References.navPoints[randomRange].transform.position;
        }
    }

    private void Alerted(Vector3 vectorToPlayer, Vector3 moveForward)
    {
        //if enemy is looking at a player and there is a wall
        RaycastHit hit;
        transform.LookAt(Player); //look at player

        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceFromWall, layerMask))
        {
            //Fly Upwards but don't move towards player
            transform.position += transform.up * (MoveSpeed /2) * Time.deltaTime;
        }
        else
        {


            if (Vector3.Distance(transform.position, Player.position) <= MaxDist) // Check if player is still within chasing distance
            {
                float x = Vector3.Distance(transform.position, Player.position);
                damageTick = (T * ((radius - Mathf.Pow(x, 3)) / radius) + damage);
                //Debug.Log(damageTick);

                timer += Time.deltaTime;

                if (timer >= timeBetweenAttacks)
                {
                    References.playerHealthSystem.TakeDamage(damageTick);
                    if (damageTick > 0)
                        animator.SetBool("Attack", true);
                    else
                        animator.SetBool("Attack", false);
                    timer = 0.3f;
                }

                if (Vector3.Distance(transform.position, Player.position) >= MinDist) // Check if player is still within attacking range long range attack enemy
                {
                    //transform.position += moveForward; // Old Move towards player
                    //rigidbody.velocity = vectorToPlayer.normalized * MoveSpeed; //new moves towards player

                    Vector3 newPosition = vectorToPlayer.normalized * (MoveSpeed * 3) * Time.deltaTime; //move towards player but stops
                    rigidbody.MovePosition(transform.position + newPosition);

                }

            }

            else
            {
                alerted = false;
                References.numberOfEnemies.Remove(this);

            }
        }
    }

    private void Patrol(Vector3 vectorToPlayer, Vector3 moveForward)
    {


        animator.SetBool("Attack", false);
        timer = 0.3f;
        RaycastHit hit;

        if (References.navPoints.Count > 0)
        {
            if (Vector3.Distance(transform.position, randomNavPoint) >= minDistanceNavPoint) //Is close enought to random nav point
            {
                transform.LookAt(randomNavPoint); // look at random navigation point
                if (Physics.Raycast(transform.position, transform.forward, out hit, distanceFromWall, layerMask)) //object in enemies path
                {
                    transform.position += transform.up * (MoveSpeed/2) * Time.deltaTime;
                }
                else
                {
                    Vector3 vectorToNavPoint = randomNavPoint - transform.position; //get objects position

                    Vector3 newPosition = vectorToNavPoint.normalized * (MoveSpeed * 3) * Time.deltaTime; //new position to go towards
                    rigidbody.MovePosition(transform.position + newPosition); //move towards the newPosition
                }

            }
            else
            {
                GoToRandomNavPoint();
            }
        }
        if (Vector3.Distance(transform.position, Player.position) <= visionRange) // if player is within enemy vision range
        {
            if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle) // And player is within cone of vision
            {
                if (!Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, layerMask)) // make sure it sees player but not through a wall
                {
                    alerted = true;
                    References.numberOfEnemies.Add(this);
                }
            }
        }
    }

    private void EnemyLogic()
    {
        //Doesn't go down to player's Y Axis Level
        if (Player != null)
        {
            Vector3 vectorToPlayer = Player.position - transform.position; //find player's position from enemy
            Vector3 moveForward = transform.forward * MoveSpeed * Time.deltaTime;

            //Guard Alerted
            if (alerted)
            {

                Alerted(vectorToPlayer,moveForward);
                light.color = Color.red;

            }
            else
            {
                Patrol(vectorToPlayer, moveForward);
                light.color = Color.white;
            }
        }


        
    }



}
