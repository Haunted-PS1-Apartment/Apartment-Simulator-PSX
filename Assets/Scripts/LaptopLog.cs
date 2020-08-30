using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "laptop", menuName = "Apartment/LaptopLog")]
public class LaptopLog : ScriptableObject
{
    [SerializeField, TextArea(20, 100)] string text = "Hello World";
    public string Text => text;
}
