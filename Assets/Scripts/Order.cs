using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Order : MonoBehaviour
{
    public List<Sprite> imageList;
    public List<Image> uiImageElements;
    private List<Sprite> selectedImages = new List<Sprite>();

    public int orderProductCount;

    void Start()
    {
        orderProductCount = Random.Range(1, 4);
        SelectRandomImages(orderProductCount);
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
