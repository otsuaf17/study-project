using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EfeitoParalax : MonoBehaviour
{
    [SerializeField] private Image fundo;
    [SerializeField] private float velocidade;
    [SerializeField] private Transform Player;

    private void Update()
    {
        MoveFundo();
    }
    private void MoveFundo()
    {
        transform.position = new Vector3(transform.position.x - velocidade * Time.deltaTime * Input.GetAxisRaw("Horizontal"), 0, 0);

        if (transform.localPosition.x >= fundo.preferredWidth)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - fundo.preferredWidth * 2, 0, 0);
        }
        else if (transform.localPosition.x <= -fundo.preferredWidth)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + fundo.preferredWidth * 2, 0, 0);
        }
    }
}
