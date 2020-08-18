using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNode : MonoBehaviour
{
    [SerializeField]
    public List<Conversation> conversations = new List<Conversation>();
    [SerializeField]
    GameObject indicator = null;
    int index = 0;

    private void Start()
    {
        SetVisible(false);
    }

    public Conversation GetConversation()
    {
        int lastIndex = index;
        index = Math.Min(index + 1, conversations.Count - 1);
        return conversations[lastIndex];
    }

    public void SetVisible(bool visible)
    {
        indicator.SetActive(visible);
    }
}
