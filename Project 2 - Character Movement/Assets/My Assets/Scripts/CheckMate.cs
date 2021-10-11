using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMate : MonoBehaviour
{
    public StarAppear star;
    public Collider rook;
    public FarmerController character;


    private void OnTriggerStay(Collider other) {
        if (other == rook) {
            star.SendMessage("Appear");
            Destroy(this);
            character.SendMessage("ChickensFound");
        }
    }

}
