using System.Collections;
using UnityEngine;
using DG.Tweening;

public class JumpingMovement : MonoBehaviour
{
    public Transform[] jumpPoints;
    public float jumpHeight = 2.0f;
    public float jumpDuration = 1.0f;
    public float jumpDelay = 1.0f;

    private int currentJumpPoint = 0;
    private Vector3 initialPosition;
    private Vector3 initialScale;
    private bool isReturning = false;
    private bool hasCompletedRoundTrip = false;
    private bool startReverseJump = false;

    void Start()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
        StartJumpSequence();
    }

    void StartJumpSequence()
    {
        MoveToNextJumpPoint();
    }

    void MoveToNextJumpPoint()
    {
        if (!hasCompletedRoundTrip)
        {
            Vector3 targetPosition = jumpPoints[currentJumpPoint].position;

            Vector3 midPoint = (initialPosition + targetPosition) / 2;
            midPoint += Vector3.up * jumpHeight;

            Vector3 targetScale = transform.localScale;
            if (isReturning)
            {
                targetScale.x = Mathf.Abs(targetScale.x);
            }
            else
            {
                targetScale.x = Mathf.Sign(targetScale.x) * Mathf.Abs(targetScale.x);
            }

            transform.DOScale(targetScale, jumpDuration / 2);
            transform.DOMove(midPoint, jumpDuration / 2)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    transform.DOMove(targetPosition, jumpDuration / 2)
                        .SetEase(Ease.InOutQuad)
                        .OnComplete(() =>
                        {
                            if (!isReturning)
                            {
                                if (currentJumpPoint < jumpPoints.Length - 1)
                                {
                                    currentJumpPoint++;
                                    StartCoroutine(NextJumpDelayed());
                                }
                                else if (startReverseJump)
                                {
                                    isReturning = true;
                                    currentJumpPoint--;
                                    StartCoroutine(NextJumpDelayed());
                                }
                            }
                            else
                            {
                                if (currentJumpPoint > 0)
                                {
                                    currentJumpPoint--;
                                    StartCoroutine(NextJumpDelayed());
                                }
                                else
                                {
                                    hasCompletedRoundTrip = true;
                                }
                            }
                        });
                });
        }
    }

    IEnumerator NextJumpDelayed()
    {
        yield return new WaitForSeconds(jumpDelay);
        MoveToNextJumpPoint();
    }

    public void StartReverseJump()
    {
        startReverseJump = true;
    }
}