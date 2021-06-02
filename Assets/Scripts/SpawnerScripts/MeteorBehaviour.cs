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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
