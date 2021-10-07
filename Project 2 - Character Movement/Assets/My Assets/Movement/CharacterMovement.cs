using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool inAir;
    private Animator animator;
    private CharacterController controller;
    public float Horizontal = 0.0f;
    public float Vertical = 0.0f;
    public float lastHorizontal = 0.0f;
    private float lastVertical = 0.0f;
    public float gravity = 10.0f;
    public float moveSpeed = 0.0f;
    public float jumpHeight = 0.0f;
    public float yVelocity = 0.0f;
    private Vector3 moveDirection;
    public float movementVsAnimationOffset = 1.0f;

    // variables for picking up/carrying object
    public bool canPickUpObject = false;
    public bool isCarryingObject = false;
    public InteractiveObject objectBeingHeld = null;
    public bool tryPickUp = false;


    //animation IDS
    private int _Horizontal;
    private int _Vertical;
    private int _Jump;
    private int _InAir;
    private int _MovementSpeed;
    private int _Cheering;


    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.controller = this.GetComponent<CharacterController>();
        AssignAnimationIDs();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = controller.isGrounded;
        // if moving forward
        if (Input.GetKey(KeyCode.W)) {
            if (Input.GetKey(KeyCode.LeftShift)) {Vertical = 2.0f;}
            else {Vertical = 1.0f;}
            if (Input.GetKey(KeyCode.A)) {Horizontal = -1.0f;}
            else {
                if (Input.GetKey(KeyCode.D)){Horizontal = 1.0f;}
                else {Horizontal = 0.0f;}
            }
        }
        else {Vertical = 0.0f;}

        Horizontal *= Vertical;

        //Smooth out the variables
        if (Mathf.Abs(Horizontal - lastHorizontal) > 0.0001) {
            lastHorizontal = Mathf.Lerp(lastHorizontal, Horizontal, 0.1f);
        }
        if (Mathf.Abs(Vertical - lastVertical) > 0.0001) {
            lastVertical = Mathf.Lerp(lastVertical, Vertical, 0.1f);
        }


        bool jumping = false;
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (grounded) {
                jumping = true;
            }
        }

        animator.SetBool(_Jump, jumping);

        if (!grounded) {animator.SetBool(_InAir, true); inAir = true;}
        else {animator.SetBool(_InAir, false); inAir = false;}


        // Set animations
        animator.SetFloat(_Horizontal, lastHorizontal);
        animator.SetFloat(_Vertical, lastVertical);

        animator.SetFloat(_MovementSpeed, moveSpeed * movementVsAnimationOffset);

        // Jump logic
        if (grounded && yVelocity < 0.0f) {
            yVelocity = -2.0f;
        }

        if (jumping) {
             yVelocity = jumpHeight;
        }

        yVelocity -= gravity * Time.deltaTime;

        // Do movements
        moveDirection = new Vector3(lastHorizontal, yVelocity, lastVertical);

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        controller.Move(moveDirection * Time.deltaTime);
        PickUpObject();


    }

    private void PickUpObject()
    {
        if (canPickUpObject && Input.GetKeyDown(KeyCode.P) && !isCarryingObject) {
            animator.SetTrigger("PickUp");
            animator.SetBool("IsHoldingObject", true);
            tryPickUp = true;
            StartCoroutine(PickUpWait());
        }

        if (isCarryingObject && Input.GetKeyDown(KeyCode.P)) {
            DropObject();
        }
    }

    private void DropObject()
    {
        animator.SetBool("IsHoldingObject", false);
        animator.SetLayerWeight(1, 0);
        isCarryingObject = false;
        objectBeingHeld.SendMessage("Drop");
        objectBeingHeld = null;

    }

    private IEnumerator PickUpWait()
    {
        yield return new WaitForSeconds(2);
        animator.SetLayerWeight(1, 1);
        isCarryingObject = true;
    }

    private void Cheer()
    {
        StartCoroutine(CheerWithDelay());
    }

    IEnumerator CheerWithDelay()
    {
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger(_Cheering);
        yield return new WaitForSeconds(1.7f);
        animator.SetLayerWeight(1, 0);
    }


    private void AssignAnimationIDs()
    {
        _Horizontal = Animator.StringToHash("Horizontal");
        _Vertical = Animator.StringToHash("Vertical");
        _Jump = Animator.StringToHash("Jump");
        _InAir = Animator.StringToHash("InAir");
        _MovementSpeed = Animator.StringToHash("MovementSpeed");
        _Cheering = Animator.StringToHash("Cheering");
    }
}
