using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "conversation", menuName = "Apartment/Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField] public List<ConversationNode> nodes = new List<ConversationNode>();
}

[System.Serializable]
public class ConversationNode
{
    public string dialog;
    public Texture2D profile;
}
