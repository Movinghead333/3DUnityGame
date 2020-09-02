using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToPlayer : MonoBehaviour
{
    public Transform tracked;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = tracked.position;
        transform.rotation = tracked.rotation;
        transform.position -= transform.forward * offset;
    }
}
