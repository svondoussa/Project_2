using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform parentTransform;
    private Transform thisTransform;
    public float mouseSensitivity = 100.0f;
    public float minAngle = -10.0f;
    public float maxAngle = 50.0f;


    // Start is called before the first frame update
    void Start()
    {
        parentTransform = transform.parent;
        thisTransform = transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -5.0f, 5.0f);
        if (Input.GetKey(KeyCode.W) || true) {
            //Debug.Log(parentTransform.forward.z);
            //float targetAngle = Mathf.Atan2(thisTransform.forward.x, thisTransform.forward.z) * Mathf.Rad2Deg;
            //parentTransform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            //Debug.Log(targetAngle); 
            parentTransform.Rotate(Vector3.up, mouseX);
            //parentTransform.SetPositionAndRotation(parentTransform.position,);

        }
        else 
        {
            thisTransform.RotateAround(parentTransform.position, parentTransform.up, mouseX);
        }
        //Debug.Log(mouseY);
        //Debug.Log(Vector3.SignedAngle(parentTransform.forward, thisTransform.forward, parentTransform.right));
        float angle = Vector3.SignedAngle(parentTransform.forward, thisTransform.forward, parentTransform.right);

        if((angle < maxAngle && mouseY < 0) || (angle > minAngle && mouseY > 0)) {
            // Within the bounds
            thisTransform.RotateAround(parentTransform.position, -parentTransform.right, mouseY);
        }

    }
}
