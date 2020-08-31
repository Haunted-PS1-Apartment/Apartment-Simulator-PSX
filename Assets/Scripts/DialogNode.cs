using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNode : MonoBehaviour
{
    [SerializeField]
    public List<Conversation> conversations = new List<Conversation>();
    [SerializeField]
    bool turnToFace = true;
    [SerializeField]
    GameObject indicator = null;
    int index = 0;

    [SerializeField] Animator NPCAnimator = null;
    [SerializeField] string NPCTrigger = "Talk";

    private void Start()
    {
        SetVisible(false);
    }

    public Conversation GetConversation()
    {
        if (NPCAnimator != null)
        {
            NPCAnimator.SetTrigger(NPCTrigger);
        }

        int lastIndex = index;
        index = Math.Min(index + 1, conversations.Count - 1);
        return conversations[lastIndex];
    }

    public bool TurnToFace => turnToFace;

    public void SetVisible(bool visible)
    {
        indicator.SetActive(visible);
    }
}
