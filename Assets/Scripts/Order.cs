using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Order : MonoBehaviour
{
    public List<Sprite> imageList;
    public List<Image> uiImageElements;
    public List<Sprite> selectedImages = new List<Sprite>();

    public int maxOrderProductCount = 3;

    void Start()
    {
        int previousOrderProductCount = PlayerPrefs.GetInt("OrderProductCount", 3);
        
        int newOrderProductCount = Random.Range(1, maxOrderProductCount + 1);
        
        PlayerPrefs.SetInt("OrderProductCount", newOrderProductCount);
        
        SelectRandomImages(newOrderProductCount);
    }

    void SelectRandomImages(int numberOfImages)
    {
        if (imageList.Count == 0)
        {
            Debug.LogWarning("Image list is empty.");
            return;
        }

        if (numberOfImages > imageList.Count)
        {
            Debug.LogWarning("The number of requested images exceeds the size of the list.");
            numberOfImages = imageList.Count;
        }

        while (selectedImages.Count < numberOfImages)
        {
            int randomIndex = Random.Range(0, imageList.Count);
            Sprite selectedImage = imageList[randomIndex];

            if (!selectedImages.Contains(selectedImage))
            {
                selectedImages.Add(selectedImage);
            }
        }

        for (int i = 0; i < selectedImages.Count && i < uiImageElements.Count; i++)
        {
            uiImageElements[i].sprite = selectedImages[i];
        }
    }
}