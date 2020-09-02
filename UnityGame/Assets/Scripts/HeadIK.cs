using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadIK : MonoBehaviour
{
    public Transform head;
    public Transform start;
    public Transform body;

    public Vector3 lastRotationAngles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 dir = transform.position - start.position;

        float angleDif = Vector3.Angle(body.forward, dir);
        Debug.Log("Angle: " + angleDif);
        Quaternion lookDir = Quaternion.LookRotation(-dir);

        if (angleDif < 90)
        {
            head.rotation = lookDir;
            head.RotateAround(head.position, head.right, -90f);
            lastRotationAngles = head.localEulerAngles;
        }
        else
        {
            Quaternion left = Quaternion.LookRotation(-body.right);
            Quaternion right = Quaternion.LookRotation(body.right);

            float angle1 = Quaternion.Angle(lookDir, left);
            float angle2 = Quaternion.Angle(lookDir, right);

            Quaternion result = angle1 < angle2 ? left : right;
            head.rotation = result;
            head.RotateAround(head.position, head.right, -90f);
        }
    }
}
