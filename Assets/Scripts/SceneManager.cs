using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public GameObject buyerObject;
    public GameObject orderUIObject;
    public GameObject emojiBubbleUIObject;
    public GameObject storageUIObject;
    public Button storageUIButton;
    private GameObject cashierBubbleUIObject;
    public Text moneyText;
    private bool buttonPressedFlag = false;
    private int totalMoneyEarned = 0;

    private JumpingMovement jumpingMovement;

    void Start()
    {
        totalMoneyEarned = PlayerPrefs.GetInt("TotalMoney", 0);
        moneyText.text = "$ " + totalMoneyEarned;

        storageUIButton.interactable = false;

        jumpingMovement = buyerObject.GetComponent<JumpingMovement>();

        StartCoroutine(EnableObjectsAndAwaitButton());
    }

    private IEnumerator EnableObjectsAndAwaitButton()
    {
        yield return new WaitForSeconds(1f);

        if (buyerObject != null)
        {
            buyerObject.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        if (orderUIObject != null)
        {
            orderUIObject.SetActive(true);
        }

        yield return new WaitForSeconds(5f);

        if (orderUIObject != null)
        {
            orderUIObject.SetActive(false);
        }

        yield return new WaitForSeconds(1f);

        if (storageUIObject != null)
        {
            storageUIObject.SetActive(true);
        }

        Debug.Log("Waiting for button press...");

        yield return new WaitUntil(() => buttonPressedFlag);

        Debug.Log("Button has been pressed.");

        yield return new WaitForSeconds(2f);

        if (emojiBubbleUIObject != null)
        {
            emojiBubbleUIObject.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        if (emojiBubbleUIObject != null)
        {
            emojiBubbleUIObject.SetActive(false);
        }

        jumpingMovement.StartReverseJump();

        yield return new WaitForSeconds(2f);

        Debug.Log("Coroutine completed.");
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void ButtonPressed()
    {
        buttonPressedFlag = true;
    }
}

