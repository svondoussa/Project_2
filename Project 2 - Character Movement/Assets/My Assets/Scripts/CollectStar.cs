using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStar : MonoBehaviour
{
    GameObject player;
    public GameObject Confetti;
    public GameObject shineAppear;
    void OnTriggerEnter(Collider other)
    {
        ScoringSystem.starsCollected ++;
        other.SendMessage("Cheer");
        GameObject conf = Instantiate(Confetti, this.transform.position, this.transform.rotation);
        GameObject shine = Instantiate(shineAppear, this.transform.position, this.transform.rotation);

        Destroy(gameObject);
    }
}
