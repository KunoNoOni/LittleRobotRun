using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemy : MonoBehaviour
{
    private float speed;

    void Start()
    {
        speed = 3f;
    }

 
    void Update()
    {
        transform.Translate(this.transform.localScale.x * (speed * Time.deltaTime), 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LevelWall") || collision.gameObject.CompareTag("BlueDoor"))
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0);
    }
}
