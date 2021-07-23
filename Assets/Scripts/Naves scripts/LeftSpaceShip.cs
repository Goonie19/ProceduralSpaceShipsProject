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
    private ParticleSystem _particleSystem;

    public Vector2 leftPosition, rightPosition;
    public float duration;
    private Vector3 flip = new Vector3(0f, 90f, 0f);
    public bool isOnleft = true;
    public Transform posToSpawn;

    private void Start()
    {
        transform.position = leftPosition;
        _particleSystem = GetComponentInChildren<ParticleSystem>();
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
            _particleSystem.Stop();
            LeftToRight().Play(); //Secuencia que lo mueve de posicion
            LeftToRight().OnComplete(() => _particleSystem.Play());
            isOnleft = false;
        }
        else //Aqui al contrario, se mueve del izquierdo al derecho
        {
            _particleSystem.Stop();
            RightToLeft().Play(); //Secuencia que lo mueve de posicion
            RightToLeft().OnComplete(() => _particleSystem.Play());
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
        if (collision.GetComponent<MeteorBehaviour>().data.obstacle)
        {
            ScoreManager.Instance.GameOver();
        }
        else
        {
            ScoreManager.Instance.AddScore();
            collision.gameObject.transform.position = posToSpawn.position;
        }
    }
}
