using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SellButton : MonoBehaviour
{
    public GameObject gameObjectA;
    public GameObject gameObjectB;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(CompareImageLists);
    }

    void CompareImageLists()
    {
        List<Sprite> listA = gameObjectA.GetComponent<Order>().selectedImages;
        List<Image> listB = gameObjectB.GetComponent<ProductManager>().selectedImages;

        bool matchFound = false;

        foreach (Sprite imageA in listA)
        {
            bool imageFoundInListB = false;

            foreach (Image imageB in listB)
            {
                if (AreImagesEqual(imageA, imageB))
                {
                    Debug.Log("Common image found.");
                    imageFoundInListB = true;
                    // Do something when a common image is found
                }
            }

            if (!imageFoundInListB)
            {
                Debug.Log("Image from listA not found in listB.");
                // Do something when an image in listA is not found in listB
                matchFound = true;
            }
        }

        if (!matchFound)
        {
            Debug.Log("Comparison completed. Check the console for details.");
            // Do something when matches were found
        }
        else
        {
            Debug.Log("No matches found between the image lists.");
        }
    }

    bool AreImagesEqual(Sprite imageA, Image imageB)
    {
        MatchCollection matchesA = Regex.Matches(imageA.name, @"\d+");
        string numbersA = ExtractNumbers(matchesA);

        MatchCollection matchesB = Regex.Matches(imageB.name, @"\d+");
        string numbersB = ExtractNumbers(matchesB);

        int numberA = int.Parse(numbersA);
        int numberB = int.Parse(numbersB);

//       Debug.Log("Numbers: " + numberA + "  " + numberB);
        return numberA == numberB;
    }

    string ExtractNumbers(MatchCollection matches)
    {
        string result = "";
        foreach (Match match in matches)
        {
            result += match.Value;
        }
        return result;
    }
}
