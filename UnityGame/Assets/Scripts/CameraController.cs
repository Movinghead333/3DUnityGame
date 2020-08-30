using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    private float height = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDir = player.transform.forward;
        Vector3 offset = new Vector3(0f, height, 0f) - lookDir * 2.5f;

        transform.position = player.position - lookDir + offset;

        Vector3 target = player.position + new Vector3(0f, 2f, 0f);
        transform.LookAt(target);
    }
}
