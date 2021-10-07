using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int starsCollected;

    void Update(){
        scoreText.GetComponent<Text>().text = "Stars Collected: " + starsCollected;
    }

}
