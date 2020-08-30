using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogReader : MonoBehaviour
{
    [SerializeField] DialogInterface dialogUI = null;
    [SerializeField] PlayerMovement movement = null;
    [SerializeField] LaptopInterface laptopInterface = null;

    Conversation currentConversation = null;
    int conversationIndex = 0;
    bool readingALaptop = false;

    PlayerInputActions controls;

    DialogNode currentNode = null;
    LaptopNode laptopNode = null;
    bool laptopActive = false;

    void Awake()
    {
        controls = new PlayerInputActions();
        controls.Player.Interact.performed += Interact;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Interact(InputAction.CallbackContext context)
    {
        if(currentConversation != null)
        {
            conversationIndex++;
            if(conversationIndex >= currentConversation.nodes.Count)
            {
                currentConversation = null;
                dialogUI.CloseDialog();
                movement.EndAnimation();
            } 
            else
            {
                dialogUI.ShowDialog(currentConversation.nodes[conversationIndex]);
                movement.StartAnimation(currentConversation.nodes[conversationIndex].playerAnimation);
            }
        }
        else if (readingALaptop)
        {
            readingALaptop = false;
            laptopInterface.CloseText();
            movement.EndAnimation();
        }
        else if(laptopNode != null && laptopActive)
        {
            laptopInterface.ShowText(laptopNode.GetLog);
            readingALaptop = true;
            movement.StartAnimation(PlayerMovement.PlayerAnim.Think);
        } 
        else if (currentNode != null)
        {
            currentConversation = currentNode.GetConversation();
            conversationIndex = 0;
            dialogUI.ShowDialog(currentConversation.nodes[conversationIndex]);
            movement.StartAnimation(currentConversation.nodes[conversationIndex].playerAnimation,
                currentNode.TurnToFace ? currentNode.transform : null);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        DialogNode dialog = other.GetComponent<DialogNode>();
        if(dialog != null && dialog != currentNode)
        {
            if(currentNode != null)
            {
                currentNode.SetVisible(false);
            }
            if(laptopNode != null)
            {
                laptopNode.SetVisible(false);
                laptopActive = false;
            }
            currentNode = dialog;
            currentNode.SetVisible(true);
        }
        LaptopNode laptop = other.GetComponent<LaptopNode>();
        if (laptop != null && laptop != laptopNode)
        {
            if (currentNode != null)
            {
                currentNode.SetVisible(false);
            }
            laptopActive = true;
            if (laptopNode != null)
            {
                laptopNode.SetVisible(false);
            }
            laptopNode = laptop;
            laptopNode.SetVisible(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DialogNode dialog = other.GetComponent<DialogNode>();
        if (dialog == currentNode && currentNode != null)
        {
            currentNode.SetVisible(false);
            currentNode = null;
        }
        LaptopNode laptop = other.GetComponent<LaptopNode>();
        if (laptop == laptopNode && laptopNode != null)
        {
            laptopNode.SetVisible(false);
            laptopNode = null;
        }
    }
}
