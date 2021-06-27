using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour
{

    public MeteorData data;

    public float BaseSpeed;

    private float _speed;

    private void Start()
    {
        _speed = BaseSpeed;
    }

    private void OnEnable()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = data.sprite;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = data.color;
        transform.GetComponent<CircleCollider2D>().radius = data.radiusOfImpact;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        
    }

    public void SetSpeed(float i)
    {
        _speed = i;
    }
}
