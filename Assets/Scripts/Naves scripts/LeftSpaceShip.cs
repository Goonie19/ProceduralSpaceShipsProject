using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeftSpaceShip : MonoBehaviour
{
    public enum MovePosition
    {
        LeftToRight, RightToLeft
    }

    private MovePosition movePos;

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

            if (touch.position.x < 360)
            {
                CheckPosition();
            }
        }
    }

    /// <summary>
    /// Función que se encarga de mover la nave
    /// </summary>
    private void CheckPosition()
    {
        if (isOnleft) //SI esta en el carril izquierdo, se mueve hacia el derecho
        {
            LeftToRight().Play(); //Secuencia que lo mueve de posicion
            isOnleft = false;
        }
        else //Aqui al contrario, se mueve del izquierdo al derecho
        {
            RightToLeft().Play(); //Secuencia que lo mueve de posicion
            isOnleft = true;
        }
    }

    #region ANIMACIONES_DOTWEEN
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
    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Star"))
        {
            ScoreManager.Instance.AddScore();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Meteor"))
        {
            ScoreManager.Instance.GameOver();
            Destroy(collision.gameObject);
        }
    }
}
