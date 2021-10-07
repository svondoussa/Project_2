using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMate : MonoBehaviour
{
    public GameObject matingPiece;
    public StarAppear star;

    private void OnTriggerStay(Collider other) {
        if (matingPiece.tag == other.tag) {
            star.SendMessage("Appear");
            Destroy(this);
        }
    }

}
