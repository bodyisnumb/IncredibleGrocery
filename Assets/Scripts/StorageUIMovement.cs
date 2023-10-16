using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StorageUIMovement : MonoBehaviour
{
    public RectTransform uiElement;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private bool isAnimating = false;

    private void Start()
    {
        DOTween.Init();

        uiElement.anchoredPosition = startPosition;

        MoveInsideScreen();
    }

    private void MoveInsideScreen()
    {
        if (!isAnimating)
        {
            isAnimating = true;

            uiElement.DOAnchorPos(targetPosition, 1.0f).OnComplete(() =>
            {
                // Animation complete
            });
        }
    }

    public void ReturnToStartPosition()
    {
        if (isAnimating)
        {
            isAnimating = false;

            uiElement.DOAnchorPos(startPosition, 1.0f).OnComplete(() =>
            {
                // Animation complete
            });
        }
    }
}
