using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    private bool isBeingCarried = false;
    public CharacterMovement player;
    private HoldPosition holdPosition;
    public string objectName;
    public Collider triggerCollider;
    public Collider interactionCollider;
    private bool inVacinity = false;

    private void Start() {
        holdPosition = player.GetComponentInChildren<HoldPosition>();
    }

    private void Update() {
        if (isBeingCarried) {
            this.transform.position = holdPosition.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !isBeingCarried) {
            // can pick up object
            player.canPickUpObject = true;
            inVacinity = true;

        }
    }

    private void OnTriggerStay(Collider other) {
        if (!isBeingCarried && player.canPickUpObject && !player.isCarryingObject && player.tryPickUp && inVacinity) {
            Debug.Log("Picking up");
            player.tryPickUp = false;
            StartCoroutine(PickUpDelay());
        }
    }


    private IEnumerator PickUpDelay()
    {
        yield return new WaitForSeconds(1.3f);
        isBeingCarried = true;
        player.objectBeingHeld = this;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.triggerCollider.enabled = false;
        this.interactionCollider.enabled = false;

    }

    private void Drop()
    {
        isBeingCarried = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.triggerCollider.enabled = true;
        this.interactionCollider.enabled = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            // out of object vicinity
            player.canPickUpObject = false;
            inVacinity = false;
        }
    }
}
