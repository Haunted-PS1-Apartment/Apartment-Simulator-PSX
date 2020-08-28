using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerArea : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera vCamera = null;

    private void Awake()
    {
        vCamera.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        vCamera.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        vCamera.gameObject.SetActive(false);
    }
}
