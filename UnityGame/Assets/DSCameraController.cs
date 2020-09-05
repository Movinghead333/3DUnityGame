using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSCameraController : MonoBehaviour
{
    public Transform player;
    private float height = 2.5f;

    private float turnSpeed = 200;

    private float yawAngle = 0f;
    private float pitchAngle = 0f;

    float distance = 2f;

    Vector2 mouseDelta;

    public static Vector3 lookDir = Vector3.forward;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        // new rotation
        Vector3 pivot = player.position + new Vector3(0f, height, 0f);

        //Debug.Log("Mouse x:" + Input.GetAxis("Mouse X") + " y: " + Input.GetAxis("Mouse Y"));

        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        yawAngle = (yawAngle + mouseDelta.x * Time.deltaTime * turnSpeed) % 360f;
        pitchAngle = pitchAngle - mouseDelta.y * Time.deltaTime * turnSpeed;
        pitchAngle = Mathf.Clamp(pitchAngle, -45f, 45f);

        // update directions
        lookDir = Vector3.forward;
        lookDir = Quaternion.AngleAxis(pitchAngle, Vector3.right) * Vector3.forward;
        lookDir = Quaternion.AngleAxis(yawAngle, Vector3.up) * lookDir;

        transform.position = pivot +- lookDir.normalized * distance;
        transform.LookAt(pivot, Vector3.up);
    }
}
