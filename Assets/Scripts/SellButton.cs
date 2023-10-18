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
    private List<int> foundStatusList = new List<int>();

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

        foundStatusList.Clear();

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
                    foundStatusList.Add(1);
                }
            }

            if (!imageFoundInListB)
            {
                Debug.Log("Image from listA not found in listB.");
                emojiImage.sprite = emojiList[1];
                foundStatusList.Add(0);
            }
        }

        if (foundStatusList.Count == listA.Count && foundStatusList.Contains(0) == false)
        {
            Debug.Log("All items are common.");
            totalMoneyEarned += (moneyReward * listA.Count);
            emojiImage.sprite = emojiList[0];
            Debug.Log("Found/Not Found Status: " + string.Join(", ", foundStatusList));
        }
        else
        {
            Debug.Log("Comparison completed. Check the console for details.");
            Debug.Log("Found/Not Found Status: " + string.Join(", ", foundStatusList));
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

   public void UpdateMoneyText()
    {
        moneyText.text = "$ " + totalMoneyEarned;
    }
}
