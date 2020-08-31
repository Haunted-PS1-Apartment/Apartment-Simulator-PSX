using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class LaptopInterface : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textUI = null;
    [SerializeField] Animator animator = null;
    [SerializeField] TextMeshProUGUI pageIndicator = null;

    PlayerInputActions controls;

    void Awake()
    {
        controls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        Vector2 movement = controls.Player.Move.ReadValue<Vector2>();
        if (movement.y > 0)
        {
            textUI.pageToDisplay = 1;
            pageIndicator.text = "pg 1/2";
        } else if(movement.y < 0)
        {
            textUI.pageToDisplay = 2;
            pageIndicator.text = "pg 2/2";
        }
    }

    public void ShowText(string text)
    {
        animator.SetTrigger("Show");
        textUI.text = text;
        textUI.pageToDisplay = 1;
        pageIndicator.text = "pg 1/2";
    }

    public void CloseText()
    {
        animator.SetTrigger("Hide");
    }
}
