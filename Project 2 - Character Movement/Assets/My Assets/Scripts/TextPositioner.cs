using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPositioner : MonoBehaviour
{
    public Transform cam;
    private void Update(){

        this.transform.LookAt(2 * transform.position - cam.position);
    }
}
