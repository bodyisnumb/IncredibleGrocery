using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashierBubble : MonoBehaviour
{
    public GameObject sourceUIObject;
    public Vector3 newPosition = new Vector3(1350, 700, 0); 
    private GameObject cashierBubbleUI;
    private bool hasCreatedCashierBubbleUI = false;
    private float cashierAlpha = 1.0f;
    private float childAlpha = 0.7f;

    void Update()
    {
        if (sourceUIObject == null)
        {
            Debug.LogError("Cashier: The 'sourceUIObject' reference is not set. Please assign it in the inspector.");
            return;
        }

        if (!hasCreatedCashierBubbleUI)
        {
            cashierBubbleUI = Instantiate(sourceUIObject, newPosition, Quaternion.identity);

            cashierBubbleUI.name = "CashierBubbleUI";

            cashierBubbleUI.transform.SetParent(transform);

            hasCreatedCashierBubbleUI = true;

            UpdateAlphaValues(cashierBubbleUI, cashierAlpha);
        }

        UpdateCashierBubbleUIImages();
    }

    void UpdateCashierBubbleUIImages()
    {
        if (cashierBubbleUI != null && sourceUIObject != null)
        {
            Image[] cashierImages = cashierBubbleUI.GetComponentsInChildren<Image>();
            Image[] sourceImages = sourceUIObject.GetComponentsInChildren<Image>();

            if (cashierImages.Length == sourceImages.Length)
            {
                for (int i = 0; i < cashierImages.Length; i++)
                {
                    cashierImages[i].sprite = sourceImages[i].sprite;
                }
            }
            else
            {
                Debug.LogWarning("The number of images in CashierBubbleUI does not match the sourceUIObject.");
            }
        }
    }

    void UpdateAlphaValues(GameObject obj, float alpha)
    {
        Image[] childImages = obj.GetComponentsInChildren<Image>();

        foreach (Image image in childImages)
        {
            if (image.gameObject == obj)
            {
                Color color = image.color;
                color.a = cashierAlpha;
                image.color = color;
            }
            else
            {
                Color color = image.color;
                color.a = childAlpha;
                image.color = color;
            }
        }
    }
}
