using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation : MonoBehaviour
{
    Animator anim;
    [SerializeField] MoveEnemy Me;

    [SerializeField] private int health = 2;


    private void Start()
    {
        anim = GetComponent<Animator>();
        Me = GetComponent<MoveEnemy>();
    }

    private void Update()
    {
        Walk();
        if (health <= 0) Death();
        Attack();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            health--;
        }
    }
    private void Death()
    {
            gameObject.GetComponent<MoveEnemy>().enabled = false;
            anim.SetBool("Death", true);
            Destroy(gameObject, 1f);
    }
    private void Walk()
    {
        if (Me.distance < 4)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }
    private void Attack()
    {
        if(Me.distance <= 2.5)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }
}
