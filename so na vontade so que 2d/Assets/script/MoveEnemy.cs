using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    Rigidbody2D rb2D;

    [SerializeField] GameObject player;

    [SerializeField] private bool flip;
    [SerializeField] private float speed;
    
    public float distance { get; set; }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector3 scale = transform.localScale;
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        //codigo pra quando ele avistar o tio
        if (distance < 4)
        {
            if (distance <= 2.5)
                return;    
            if (player.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);//faz ele virar pro lado onde o tio ta
                rb2D.velocity = new Vector2(speed * 1, rb2D.velocity.y);//isso faz ele ir ate o tio

            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);//faz ele virar pro lado onde o tio ta
                rb2D.velocity = new Vector2(speed * -1, rb2D.velocity.y);//isso faz ele ir ate o tio

            }  
        }
        transform.localScale = scale;   
    }
}
