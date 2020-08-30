using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopNode : MonoBehaviour
{
    [SerializeField]
    LaptopLog laptopText = null;
    [SerializeField]
    bool turnToFace = true;
    [SerializeField]
    GameObject indicator = null;
    int index = 0;

    private void Start()
    {
        SetVisible(false);
    }

    public string GetLog => laptopText.Text;

    public bool TurnToFace => turnToFace;

    public void SetVisible(bool visible)
    {
        indicator.SetActive(visible);
    }
}
