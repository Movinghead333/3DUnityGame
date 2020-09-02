using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TODO: This class should use a coroutines
 */
public class RaiseDeadHandController : MonoBehaviour
{
    private float delay = 0f;
    private float currentDelay = 0f;

    private Animator animator;
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        delay = Random.Range(0f, 1f);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (done) return;
        if (currentDelay < delay)
        {
            currentDelay += Time.deltaTime;
        }
        else
        {
            animator.SetInteger("animType", Random.Range(0, 4));
            done = true;
        }
    }
}
