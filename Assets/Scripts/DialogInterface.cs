using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogInterface : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI dialogText;
    [SerializeField] Animator animator;
    [SerializeField] RawImage profileImage;

    bool shown = false;
    Texture2D queuedTexture = null;

    public void ShowDialog(ConversationNode node)
    {
        dialogText.text = node.dialog;
        if (shown)
        {
            if (profileImage.texture != node.profile)
            {
                queuedTexture = node.profile;
                animator.SetTrigger("Swap");
            }
        } 
        else
        {
            profileImage.texture = node.profile;
            animator.SetTrigger("Pop-in");
            shown = true;
        }
    }

    public void CloseDialog()
    {
        animator.SetTrigger("Pop-out");
        shown = false;
    }

    public void SwapImage()
    {
        profileImage.texture = queuedTexture;
    }
}
