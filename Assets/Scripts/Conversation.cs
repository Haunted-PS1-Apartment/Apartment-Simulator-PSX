using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "conversation", menuName = "Apartment/Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField] public List<ConversationNode> nodes = new List<ConversationNode>();
}

public enum SecondaryAnimation
{
    None,
    Talking,
    Wave,
    Idle
}

[System.Serializable]
public class ConversationNode
{
    public string dialog;
    public Texture2D profile;
    public PlayerMovement.PlayerAnim playerAnimation = PlayerMovement.PlayerAnim.None;
}
