using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGem : MonoBehaviour
{

    public TimedCollect timedCollect;
    private ParticleSystem particle;

    private void Start() {
        this.particle = GetComponentInChildren<ParticleSystem>();
        HideComponent();

    }
    void Update()
    {
        if (timedCollect.isActive) {
        }
    }

    public void StartGems()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<StarAnimator>().enabled = true;
        this.GetComponent<SphereCollider>().enabled =  true;
        particle.Play();
    }

    private void OnTriggerEnter(Collider other) {
        timedCollect.gemsCollected++;
        HideComponent();
    }

    public void HideComponent()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<StarAnimator>().enabled = false;
        this.GetComponent<SphereCollider>().enabled =  false;
        particle.Stop();
    }
}
