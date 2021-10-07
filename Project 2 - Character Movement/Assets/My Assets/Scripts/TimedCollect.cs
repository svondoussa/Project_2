using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedCollect : MonoBehaviour
{
    public GameObject timedCollectText;
    public int gemsCollected = 0;
    public int numGems;
    public float timeAllowed = 20f;
    public float timeRemaining;
    public bool isActive = false;
    private Vector3 initialPos;

    private void Start() {
        this.initialPos = this.transform.position;
    }

    void Update() {
        if (isActive) {
            timedCollectText.GetComponent<Text>().text = "Collect the gems!\n" + timeRemaining.ToString("0,0")
                + "\n" + gemsCollected + "/" + numGems;
            timeRemaining -= Time.deltaTime;
            if(gemsCollected == numGems) {
                StartCoroutine(GemsCollected());
            } else if (timeRemaining <= 0.0f) {
                StartCoroutine(OutOfTime());
            }

        }
    }

    IEnumerator GemsCollected()
    {
        isActive = false;
        timedCollectText.GetComponent<Text>().text = "CONGRATS!!\nYou collected all the gems";
        yield return new WaitForSeconds(3);
        timedCollectText.GetComponent<Text>().text = "";
        Destroy(gameObject);
    }

    IEnumerator OutOfTime()
    {
        isActive = false;
        CollectGem[] Gems = this.GetComponentsInChildren<CollectGem>();
        for (int i=0; i < numGems; i++) {
            Gems[i].HideComponent();
        }
        timedCollectText.GetComponent<Text>().text = "OH NO!! You ran out of time\nHave another go";
        yield return new WaitForSeconds(3);
        timedCollectText.GetComponent<Text>().text = "";
        this.GetComponent<SphereCollider>().enabled = true;
        this.GetComponent<ToggleSandMesh>().SendMessage("ShowMesh");
        this.GetComponent<StarAnimator>().enabled = true;
        gemsCollected = 0;
        isActive = false;
        timeRemaining = timeAllowed;
    }

    public void StartTimer()
    {

        timeRemaining = timeAllowed;
        isActive = true;
        CollectGem[] Gems = this.GetComponentsInChildren<CollectGem>();
        for (int i=0; i < numGems; i++) {
            Gems[i].StartGems();
        }
        this.GetComponent<SphereCollider>().enabled = false;
        this.GetComponent<ToggleSandMesh>().SendMessage("HideMesh");
        this.GetComponent<StarAnimator>().enabled = false;
        this.transform.position = initialPos;

    }

    private void OnTriggerEnter(Collider other) {
        if (!isActive) {
            StartTimer();
        }
    }
}
