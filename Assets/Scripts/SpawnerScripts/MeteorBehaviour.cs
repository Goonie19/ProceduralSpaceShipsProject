using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour
{

    public MeteorData data;

    public float Speed;

    private void OnEnable()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = data.sprite;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = data.color;
        transform.GetComponent<CircleCollider2D>().radius = data.radiusOfImpact;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }
}
