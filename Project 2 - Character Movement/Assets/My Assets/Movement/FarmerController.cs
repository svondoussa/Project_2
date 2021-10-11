using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FarmerController : MonoBehaviour
{

    private Animator animator;
    public TextMeshPro speech;
    private string currentSpeechText;
    public string beforeFoundSpeech = "I wish my  chickens\nwould come back...";
    public string afterFoundSpeech = "Yay my chickens\nare back!!";

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        currentSpeechText = beforeFoundSpeech;
        speech.text = "";
    }

    private void ChickensFound()
    {
        animator.SetBool("IsHappy", true);
        currentSpeechText = afterFoundSpeech;
        speech.text = currentSpeechText;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            speech.text = currentSpeechText;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            speech.text = "";
        }
    }
}
