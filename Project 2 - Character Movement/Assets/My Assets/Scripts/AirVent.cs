using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVent : MonoBehaviour
{
    public float movementForce;
    private bool activate = false;
    private Collider otherOne;
    private void OnTriggerStay(Collider other) {
        Debug.Log("Hit");
        otherOne = other;
        activate = true;

    }

    private void LateUpdate() {
        if (activate) {
            otherOne.transform.position += new Vector3(0f, movementForce * Time.deltaTime, 0f);
            activate = false;
        }
    }
}
