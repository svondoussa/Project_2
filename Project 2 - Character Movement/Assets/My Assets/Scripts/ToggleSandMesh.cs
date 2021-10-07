using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSandMesh : MonoBehaviour
{
    public GameObject glass;
    public GameObject glassHolder;
    public GameObject sand;

    private MeshRenderer glassMesh;
    private MeshRenderer glassHolderMesh;
    private MeshRenderer sandMesh;

    public ParticleSystem particles;

    private void Start()
    {
     this.glassMesh = this.glass.GetComponent<MeshRenderer>();
     this.glassHolderMesh = this.glassHolder.GetComponent<MeshRenderer>();
     this.sandMesh = this.sand.GetComponent<MeshRenderer>(); 
    }

    private void HideMesh()
    {
        this.glassMesh.enabled = false;
        this.glassHolderMesh.enabled = false;
        this.sandMesh.enabled= false;
        this.particles.Stop();
    }

    private void ShowMesh()
    {
        this.glassMesh.enabled = true;
        this.glassHolderMesh.enabled = true;
        this.sandMesh.enabled= true;
        this.particles.Play();
    }
}
