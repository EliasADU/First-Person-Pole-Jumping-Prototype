using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform Player;
    public float MoveSpeed = 4f;
    public float MaxDist;
    public float MinDist;
    private bool alerted;

    public float visionRange;
    private float minDistanceNavPoint = 2f;
    public float visionConeAngle;

    private LayerMask layerMask;
    private Vector3 randomNavPoint;
    private Animator animator;

    




    private float damageTick; //Damage(x) = T* (r - x^3) / r
    private float T;
    private float damage;
    private float radius;

    private float timer;
    private float timeBetweenAttacks = 1.15f;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Walls", "Enemy");

    }

    void Start()
    {
        //initalize player to transform b/c easier to manipulate than controller.
        Player = References.player.transform;
        animator = GetComponentInChildren<Animator>();
        damage = 10.1f;
        radius = 0.5f;
        T = 0.00015f;
        alerted = false;
        GoToRandomNavPoint();
        timer = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }

    private void GoToRandomNavPoint()
    {
        if(References.navPoints.Count > 0)
        {
            int randomRange = Random.Range(0, References.navPoints.Count);
            randomNavPoint = References.navPoints[randomRange].transform.position;
        }
    }
    public void ChasePlayer()
    {
        //Doesn't go down to player's Y Axis Level
        if (Player != null)
        {
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

                    if (Vector3.Distance(transform.position, Player.position) <= MaxDist) // Check if player is still within chasing distance
                    {
                        float x = Vector3.Distance(transform.position, Player.position);
                        damageTick =  (T * ((radius - Mathf.Pow(x, 3)) / radius) + damage);
                        //Debug.Log(damageTick);

                        timer += Time.deltaTime;

                        if (timer >= timeBetweenAttacks)
                        {
                            References.playerHealthSystem.TakeDamage(damageTick);
                            if(damageTick > 0)
                                animator.SetBool("Attack", true);
                            else
                                animator.SetBool("Attack", false);
                            timer = 0.3f;
                        }

                        if (Vector3.Distance(transform.position, Player.position) >= MinDist) // Check if player is still within attacking range long range attack enemy
                        {
                            transform.position += moveTowardsPlayer; // Move towards player
                        }
                        /*else
                        {

                            // if attack animation wants to be added based off distance

                           *//* 
                            animator.SetBool("Attack", true);
                            timer += Time.deltaTime;

                            if (timer >= timeBetweenAttacks)
                            {
                                //References.playerHealthSystem.TakeDamage(damage);
                                References.playerHealthSystem.TakeDamage(damageTick);
                                timer = 0.25f;
                            }*//*

                        }*/

                    }

                    else
                    {
                        alerted = false;
                    }
                }

            }
            else
            {
                animator.SetBool("Attack", false);
                timer = 0.3f;

                if (References.navPoints.Count > 0)
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


        
    }


}
