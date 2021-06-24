using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBehaviour : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("SpaceShip"))
        {
            Debug.Log("Estrella cogida");
            Destroy(gameObject);
        }
    }
}
