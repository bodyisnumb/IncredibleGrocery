using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public int maxSelected;
    public Button sellButton;
    private float activeAlpha = 1.0f;
    private float inactiveAlpha = 0.7f;
    private List<Toggle> allToggles = new List<Toggle>();
    public List<Image> selectedImages = new List<Image>();

    private void Start()
    {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener((value) => ToggleStateChanged(toggle, value));
            allToggles.Add(toggle);
        }

        UpdateButtonState();
    }

    private void Update()
    {
        maxSelected = PlayerPrefs.GetInt("OrderProductCount", 3);
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

                Transform parentTransform = toggle.transform.parent;
                Image image = parentTransform.GetComponentInChildren<Image>();
                if (image != null)
                {
                    if (!selectedImages.Contains(image))
                    {
                        selectedImages.Add(image);
                    }
                }
            }
        }
        else
        {
            foreach (Toggle t in allToggles)
            {
                t.interactable = true;
            }

            Transform parentTransform = toggle.transform.parent;
            Image image = parentTransform.GetComponentInChildren<Image>();
            if (image != null)
            {
                selectedImages.Remove(image);
            }
        }

        UpdateButtonState();
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

    private void UpdateButtonState()
    {
        int selectedCount = CountSelectedToggles();
        if (selectedCount < maxSelected)
        {
            sellButton.interactable = false;
            ChangeButtonAlpha(sellButton, inactiveAlpha);
        }
        else
        {
            sellButton.interactable = true;
            ChangeButtonAlpha(sellButton, activeAlpha);
        }
    }

    private void ChangeButtonAlpha(Button button, float alpha)
    {
        Color buttonColor = button.image.color;
        buttonColor.a = alpha;
        button.image.color = buttonColor;
    } 
}