using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public int maxSelected = 3;
    private List<Toggle> allToggles = new List<Toggle>();

    private void Start()
    {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener((value) => ToggleStateChanged(toggle, value));
            allToggles.Add(toggle);
        }
    }

    private void ToggleStateChanged(Toggle toggle, bool value)
    {
        if (value)
        {
            if (CountSelectedToggles() > maxSelected)
            {
                toggle.isOn = false;
            }
            else
            {
                toggle.interactable = true;
            }
        }
        else
        {
            foreach (Toggle t in allToggles)
            {
                t.interactable = true;
            }
        }
    }

    private int CountSelectedToggles()
    {
        int count = 0;
        foreach (Toggle toggle in allToggles)
        {
            if (toggle.isOn)
            {
                count++;
            }
        }
        return count;
    }
}
