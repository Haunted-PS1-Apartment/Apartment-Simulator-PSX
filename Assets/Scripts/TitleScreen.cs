using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    PlayerInputActions controls;

    int index = 0;

    [SerializeField] AnimationClip[] anims = { };
    [SerializeField] Animator animator = null;
    [SerializeField] string nextScene = "";

    void Awake()
    {
        controls = new PlayerInputActions();
        controls.Player.Interact.performed += Increment;
    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Increment(InputAction.CallbackContext context)
    {
        if(index < anims.Length)
        {
            animator.Play(anims[index].name);
            if (index == anims.Length - 1)
            {
                StartCoroutine(Transition());
            }
        }
        index++;
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextScene);
    }
}
