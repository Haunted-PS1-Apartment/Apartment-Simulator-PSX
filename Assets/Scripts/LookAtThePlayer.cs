using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtThePlayer : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] float lookWeight = .5f;
    [SerializeField] float bodyLook = .5f;
    [SerializeField] float targetHeight = 2;

    Transform target = null;

    void OnAnimatorIK(int layer)
    {
        if(target != null)
        {
            animator.SetLookAtWeight(lookWeight, bodyLook);
            animator.SetLookAtPosition(target.position + Vector3.up * targetHeight);
        }
        else
        {
            animator.SetLookAtWeight(0);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        animator.SetTrigger("Wave");
    }

    void OnTriggerStay(Collider collider)
    {
        target = collider.transform;
    }

    void OnTriggerExit(Collider collider)
    {
        target = null;
    }
}
