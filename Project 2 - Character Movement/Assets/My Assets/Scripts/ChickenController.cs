using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    private CharacterController controller;
    public GameObject player;
    public Animator animator;

    public float gravity = 10.0f;
    private float timeToStayStill = 1.5f;
    public float collisionMoveSensitivity = 3.0f;
    // Movement Limits
    public float doNothingDis = 6.0f;
    public float runDis = 4f;
    public float walkSpeed = 0.5f;
    public float RunSpeed = 1.5f;
    public float animationSpeed = 1.0f;



    // Animation IDs
    private int _MoveSpeed;
    private int _Moving;
    private int _AnimSpeed;

    // Start is called before the first frame update
    void Start()
    {
        this.controller = this.GetComponent<CharacterController>();
        this.animator = this.GetComponent<Animator>();
        AssignAnimationIDs();
    }

    // Update is called once per frame
    void Update()
    {

        float distance = DistanceToPlayer();
        bool moving = false;

        if (distance < doNothingDis){
            moving = true;
            Vector3 direction = this.transform.position - player.transform.position;
            direction.y = 0f;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            Vector3 facing = this.transform.forward;
            facing.y = -gravity;
            float movespeed = 0.5f;

            if (timeToStayStill < 0.0f) {
                if (distance < runDis) {
                    //chicken runs away
                    controller.Move(facing * RunSpeed * Time.deltaTime);
                    movespeed = 1.0f;
                }
                else {
                    //chicken walks away
                    controller.Move(facing * walkSpeed * Time.deltaTime);
                    
                }
                this.animator.SetFloat(_MoveSpeed, movespeed);
            }
            else {
                timeToStayStill -= Time.deltaTime;
            }
        }
        else
        {
            timeToStayStill = 1.5f;
        }

        this.animator.SetBool(_Moving, moving);
        this.animator.SetFloat(_AnimSpeed, animationSpeed);

    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(this.transform.position, player.transform.position);
    }

    private void AssignAnimationIDs()
    {
        this._MoveSpeed = Animator.StringToHash("MoveSpeed");
        this._Moving = Animator.StringToHash("Moving");
        this._AnimSpeed = Animator.StringToHash("AnimSpeed");

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Chicken") {
            MoveAwayFromChicken(other);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Chicken") {
            MoveAwayFromChicken(other);
        }
    }

    private void MoveAwayFromChicken(Collider other)
    {
        // Have hit another chicken so move away from it
        Vector3 moveDir = this.transform.position - other.transform.position;
        moveDir.y = 0f;
        controller.Move(moveDir * collisionMoveSensitivity* Time.deltaTime);
    }
}
