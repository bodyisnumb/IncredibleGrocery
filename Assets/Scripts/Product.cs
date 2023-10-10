using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    public Toggle toggle;
    
    private Image image;
    private Color normalColor;
    private Color disabledColor;

    private void Start()
    {
        image = GetComponent<Image>();
        normalColor = image.color;
        disabledColor = new Color(normalColor.r, normalColor.g, normalColor.b, 0.5f);
    }

    private void Update()
    {
        if (!toggle.isOn)
        {
            image.color = normalColor;
        }
        else
        {
            image.color = disabledColor;
        }
    }
}
