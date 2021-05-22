using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeftSpaceShip : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPosition;

    public Vector2 leftPosition, rightPosition;
    public float duration;
    private Vector3 flip = new Vector3(0f, 90f, 0f);
    public bool isOnleft = true;

    private void Start()
    {
        transform.position = leftPosition;
    }

    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            touch = Input.touches[0];
            Debug.Log("Left ship: " + touch.position);

            if (touch.position.x < 360)
            {
                CheckPosition();
            }
        }

    }

    private void CheckPosition()
    {
        if (isOnleft)
        {
            LeftToRight().Play();
            //transform.position = rightPosition;
            isOnleft = false;
        }
        else
        {
            RightToLeft().Play();
            //transform.position = leftPosition;
            isOnleft = true;
        }
    }

    private Sequence LeftToRight()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveX(rightPosition.x, duration)
            .SetEase(Ease.Linear));
        seq.Join(Flip());

        return seq;
    }

    private Sequence RightToLeft()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveX(leftPosition.x, duration)
            .SetEase(Ease.Linear));
        seq.Join(Flip());

        return seq;
    }

    private Sequence Flip()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DORotate(flip, duration / 2)
            .SetRelative()
            .SetEase(Ease.Linear));
        seq.Append(transform.DORotate(-flip, duration / 2)
            .SetRelative()
            .SetEase(Ease.Linear));

        return seq;
    }
}
