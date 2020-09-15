using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float xRotattion = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //never over rotate and look behiend the player
        xRotattion -= mouseY;
        xRotattion = Mathf.Clamp(xRotattion, -90f, 90f);

        
        transform.localRotation = Quaternion.Euler(xRotattion,0f,0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
