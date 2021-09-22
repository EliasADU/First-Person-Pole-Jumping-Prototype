using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    float speed;

    [SerializeField]
    float gravitationalSpeed;

    [SerializeField]
    float gravity;

    [SerializeField]
    float minDownwardVelocity;

    [SerializeField]
    float miniImpulseWeight;

    [SerializeField]
    float miniImpulseStrength;


    //cooldown period on the jumps while in the air: denoting all changes made feel free to delete
    [SerializeField]
    float cooldownPeriod;

    List<Impulse> currentImpulses;

    Vector3 lockedwasdMove;
    bool locked;


    float timeSinceBoost;

    private void Awake()
    {
        References.player = gameObject;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentImpulses = new List<Impulse>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ClearFinishedImpulses();

        //cooldown period on the jumps while in the air: denoting all changes made feel free to delete
        timeSinceBoost += Time.deltaTime;
    }

    public void AddImpulse(Impulse i, bool isSurfaceBounce = true)
    {
        currentImpulses.Add(i);
        if (isSurfaceBounce)
        {
            locked = false;
        }
    }

    //Aggregates all movement on the player, including
    //impulses and applies to gameobject using the
    //character controller
    private void Move()
    {
        ////VERTICAL MOVEMENT////

        if (characterController.isGrounded || gravitationalSpeed >= 0)
        {
            gravitationalSpeed = minDownwardVelocity;
        }
        else
        {
            gravitationalSpeed -= gravity * Time.deltaTime;
        }

        Vector3 gravityMove = new Vector3(0, gravitationalSpeed, 0);

        ////WASD MOVEMENT////
        
        float horizontalMove = Input.GetAxis("Horizontal");
        float forwardsMove = Input.GetAxis("Vertical");

        Vector3 wasdDirection = transform.forward * forwardsMove + transform.right * horizontalMove;
        Vector3 wasdMove = wasdDirection * speed;


        //cooldown period on the jumps while in the air: denoting all changes made feel free to delete
        if (Input.GetKeyDown(KeyCode.Space) && !characterController.isGrounded && timeSinceBoost >= cooldownPeriod)
        {
            AddImpulse(getMiniImpulse());
            lockedwasdMove = wasdMove;
            ResetGravity();
            locked = true;

            //cooldown period on the jumps while in the air: denoting all changes made feel free to delete
            timeSinceBoost = 0;
        }
        if (characterController.isGrounded)
        {
            locked = false;
        }
        if (locked)
        {
            wasdMove = new Vector3(lockedwasdMove.x + wasdMove.x / 2, lockedwasdMove.y + wasdMove.y / 2, lockedwasdMove.z + wasdMove.z / 2); 
        }

        ////TOTAL FROM IMPULSES////
        
        Vector3 impulseMove = GetNetImpulse();

        ////AGGREGATING ALL OTHERS////
        
        characterController.Move((Time.deltaTime * wasdMove) + (gravityMove * Time.deltaTime) + (impulseMove * Time.deltaTime));
    }

    //Mini Impulse is an impulse that imitates a "lunge",
    //takes current player WASD raw input plus some
    //vertical movement
    Impulse getMiniImpulse()
    {
        Vector3 miniImpulseDirection = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal"));

        miniImpulseDirection.y = miniImpulseWeight;
        return new Impulse(miniImpulseDirection, miniImpulseStrength);
    }

    public void ResetGravity()
    {
        gravitationalSpeed = 0;
    }

    void ClearFinishedImpulses()
    {
        currentImpulses.RemoveAll(s => s.IsZero());
    }

    Vector3 GetNetImpulse()
    {
        Vector3 netImpulse = Vector3.zero;
        foreach (Impulse i in currentImpulses)
        {
            i.Tick();
            netImpulse += i.GetCurrentVelocity();
        }

        return netImpulse;
    }
}
