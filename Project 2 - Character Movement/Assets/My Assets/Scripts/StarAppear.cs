using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAppear : MonoBehaviour
{

    // Components
    private MeshRenderer meshRenderer;
    private StarAnimator starAnimator;
    private SphereCollider sphereCollider;
    private ParticleSystem shine;
    public GameObject appearShine;

    private void Start() {
        this.meshRenderer = this.GetComponent<MeshRenderer>();
        this.starAnimator = this.GetComponent<StarAnimator>();
        this.sphereCollider = this.GetComponent<SphereCollider>();
        this.shine = this.GetComponentInChildren<ParticleSystem>();

        this.meshRenderer.enabled = false;
        this.starAnimator.enabled = false;
        this.sphereCollider.enabled = false;
        this.shine.Stop();
    }

    private void Appear()
    {
        this.meshRenderer.enabled = true;
        this.starAnimator.enabled = true;
        this.sphereCollider.enabled = true;
        this.shine.Play();
        GameObject shineAppear = Instantiate(appearShine, this.transform.position, this.transform.rotation);
    }
}
