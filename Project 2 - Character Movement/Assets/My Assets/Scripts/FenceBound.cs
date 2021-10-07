using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBound : MonoBehaviour
{
    public int totalChickens;
    private int chickensCollected = 0;
    private bool allChickensFound = false;
    public FarmerController farmer;
    public StarAppear star;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Chicken") {
            chickensCollected++;
            
            if (chickensCollected == totalChickens && !allChickensFound) {
                // all the chickens have been returned
                ChickenGameComplete();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Chicken") {
            chickensCollected--;
        }
    }

    private void ChickenGameComplete()
    {
        this.allChickensFound = true;
        farmer.SendMessage("ChickensFound");
        star.SendMessage("Appear");
    }

}
