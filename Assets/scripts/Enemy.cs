using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 4f;
    public float MaxDist;
    public float MinDist;
    public float damage;
    Transform Player;
    public float visionRange;
    private Vector3 randomNavPoint;
    private float minDistanceNavPoint = 2f;
    private bool alerted;
    public float visionConeAngle;
    private LayerMask layerMask;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Walls", "Enemy");
    }

    void Start()
    {
        //initalize player to transform b/c easier to manipulate than controller.
        Player = References.player.transform;
        alerted = false;
        GoToRandomNavPoint();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }

    private void GoToRandomNavPoint()
    {
        int randomRange = Random.Range(0, References.navPoints.Count);
        randomNavPoint = References.navPoints[randomRange].transform.position;
    }
    public void ChasePlayer()
    {
        //Doesn't go down to player's Y Axis Level
        if (Player != null)
        {
            //Debug.Log(Vector3.Distance(transform.position, Player.position));
            Vector3 vectorToPlayer = Player.position - transform.position; //find player's position from enemy
            Vector3 moveTowardsPlayer = transform.forward * MoveSpeed * Time.deltaTime;

            //Guard Alerted
            if (alerted)
            {
                //if enemy is looking at a player and there is a wall
                if (Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, layerMask))
                {
                    //Fly Upwards but don't move towards player
                    transform.position += transform.up * MoveSpeed * Time.deltaTime;
                }
                else
                {

                    transform.LookAt(Player); //look at player
                    //Debug.Log(Vector3.Distance(transform.position, Player.position)); //Debug

                    if (Vector3.Distance(transform.position, Player.position) <= MaxDist) // Check if player is still within chasing distance
                    {
                        if (Vector3.Distance(transform.position, Player.position) >= MinDist) // Check if player is still within attacking range long range attack enemy
                        {
                            transform.position += moveTowardsPlayer; // Move towards player
                        }
                    }
                }

            }
            else
            {
                if (Vector3.Distance(transform.position, randomNavPoint) >= minDistanceNavPoint) // Check if player is still within attacking range long range attack enemy
                {
                    transform.LookAt(randomNavPoint); // look at random navigation point
                    transform.position += moveTowardsPlayer; //move random navigation point
                }
                else
                {
                    GoToRandomNavPoint();
                }
                if (Vector3.Distance(transform.position, Player.position) <= visionRange) // if player is within enemy vision range
                {
                    if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle) // And player is within cone of vision
                    {
                        if (!Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, layerMask)) // make sure it sees player but not through a wall
                        {
                            alerted = true;
                        }
                    }
                }
            }
        }


        //Mesh Agent Code
        /*if (Player != null)
        {
            if (navMeshAgent.Warp(Player.position))
            {
                navMeshAgent.destination = Player.position;
            }
        }
*/
        /*
                Vector3 playerPosition = References.thePlayer.transform.position;
                Vector3 vectorToPlayer = playerPosition - transform.position;
                ourRigidBody.velocity = vectorToPlayer.normalized * speed;
                Vector3 playerPositionAtOurHeight = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
                transform.LookAt(playerPositionAtOurHeight);*/

        /*if (Player != null) //Follows Player Well But Gets Stuck Behind Walls
        {
            Vector3 vectorToPlayer = Player.position - transform.position;
            if (alerted)
            {
                transform.LookAt(Player);
                Debug.Log(Vector3.Distance(transform.position, Player.position));

                if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
                {

                    transform.position += transform.forward * MoveSpeed * Time.deltaTime;

                }
            }
            else
            {
                if (Vector3.Distance(transform.position, Player.position) <= visionRange)
                {
                    if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle)
                    {
                        if (!Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, layerMask))
                        {
                            alerted = true;
                        }
                    }
                }
            }
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject characterGameObject = collision.gameObject;

        
        if(characterGameObject.GetComponent<CharacterController>() != null)
        {
            HealthSystem playerHealthSystem = characterGameObject.GetComponent<HealthSystem>();
            playerHealthSystem.TakeDamage(damage);
        }
        
    }

}
