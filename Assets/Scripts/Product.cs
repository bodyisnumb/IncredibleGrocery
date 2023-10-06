using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    
    public Toggle toggle;

    private Image image;
    private Color tempColor;

    void Start()
    {
        image = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!toggle.isOn)
        {
            tempColor = image.color;
            tempColor.a = 1;
            image.color = tempColor;
        }
        else
        {
            tempColor = image.color;
            tempColor.a = .5f;
            image.color = tempColor;
        }

    }
}
