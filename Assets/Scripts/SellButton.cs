using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SellButton : MonoBehaviour
{
    public GameObject gameObjectA;
    public GameObject gameObjectB;
    public List<Sprite> emojiList;
    public Image emojiImage;
    public Text moneyText;
    public int moneyReward = 10;
    private int totalMoneyEarned = 0;

    private void Start()
    {
        totalMoneyEarned = PlayerPrefs.GetInt("TotalMoney", 0);
        UpdateMoneyText();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(CompareImageLists);
        button.interactable = false;
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

                    totalMoneyEarned += moneyReward;
                    PlayerPrefs.SetInt("TotalMoney", totalMoneyEarned);
                }
            }

            if (!imageFoundInListB)
            {
                Debug.Log("Image from listA not found in listB.");
                matchFound = true;
                emojiImage.sprite = emojiList[1];
            }
        }

        if (!matchFound)
        {
            Debug.Log("Comparison completed. Check the console for details.");

            totalMoneyEarned += ((moneyReward * 2 * (listA.Count)) - (moneyReward * listA.Count));
            PlayerPrefs.SetInt("TotalMoney", totalMoneyEarned);
            emojiImage.sprite = emojiList[0];
        }
        else
        {
            Debug.Log("No matches found between the image lists.");
        }

        UpdateMoneyText();
    }

    bool AreImagesEqual(Sprite imageA, Image imageB)
    {
        MatchCollection matchesA = Regex.Matches(imageA.name, @"\d+");
        string numbersA = ExtractNumbers(matchesA);

        MatchCollection matchesB = Regex.Matches(imageB.name, @"\d+");
        string numbersB = ExtractNumbers(matchesB);

        int numberA = int.Parse(numbersA);
        int numberB = int.Parse(numbersB);

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

    void UpdateMoneyText()
    {
        moneyText.text = "$ " + totalMoneyEarned;
    }
}
