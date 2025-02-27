using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensx;
    public float sensy;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        // zorgt ervoor dat de muis in het midde zit
        Cursor.lockState = CursorLockMode.Locked;
        // zorgt ervoor dat de muis onzichtbaar is
        Cursor.visible = false;
    }

    private void Update()
    {
        // iets met mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * sensx;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensy;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // rotate cam en orientation of iets idk
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
