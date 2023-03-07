using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    //Convocando os scripts de alta classe kkkk
    Animator Anim;
    moviment Move;

    [SerializeField] private float timeAttack;

    [SerializeField]private bool IsAttackPressed, IsAttacking;

    private string currentState;
    const string Idle = "idle";
    const string Run = "run";
    const string Jump = "jump";
    const string Fall = "fall";

    void Start()
    {
        Anim = GetComponent<Animator>();//pega os components la na casa do aralho 
        Move = GetComponent<moviment>();
    }

    void Update()
    {
        //Ele so faz a animação de andar papito -_-
        if (Move.IsGrounded() && !IsAttacking)
        {
            if (Move.MoveX != 0)
            {
                AnimationState(Run);
            }
            else
            {
                AnimationState(Idle);
            }
        }
        //Ele ve se o broder ta vuando 
        if(!Move.IsWalled() && !IsAttacking)
        { 
            if (Move.isJumping && !Move.IsGrounded())
            {
                AnimationState(Jump);
            }
            else if (!Move.isJumping && !Move.IsGrounded())
            {
                AnimationState(Fall);
            }
        }

        //flip
        if (!Move.isWallJumping && !IsAttacking)
        {
            if (Move.MoveX > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (Move.MoveX < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        //Animação De Ataque 0w0
        if (Input.GetKeyDown(KeyCode.J))
        {
            IsAttacking = true;
            Anim.SetBool("bool_attack", true);
            Invoke("IsAttack", timeAttack);
        }
        else
        {
            Anim.SetBool("bool_attack", false);
        }
        
    }
    private void IsAttack()
    {
        IsAttacking = false; 
    }

    private void AnimationState(string AnimState)
    {
        if (currentState == AnimState) return;

        Anim.Play(AnimState);

        currentState = AnimState;
    }
}
