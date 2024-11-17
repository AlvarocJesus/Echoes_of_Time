using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public float speed;
    public bool groud = true;
    public bool wall = false;
    public Transform GraudCheck;
    public Transform WallCheck; // Novo ponto de verificação para a parede
    public LayerMask layer;
    public bool face = true;

    public int jumpHits = 0; // Contador de saltos
    private Player play;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        // Verifica se há chão abaixo
        groud = Physics2D.Linecast(GraudCheck.position, transform.position, layer);
        
        // Verifica se há uma parede à frente
        wall = Physics2D.Linecast(WallCheck.position, transform.position + transform.right * 0.1f, layer);

        if (!groud || wall) // Se não houver chão ou houver parede, inverte a direção
        {
            speed *= -1;
        }

        if (speed > 0 && !face)
        {
            Flip();
        }
        else if (speed < 0 && face)
        {
            Flip();
        }

        

    }

    void Flip()
    {
        face = !face;
        Vector3 scala = transform.localScale;
        scala.x *= -1;
        transform.localScale = scala;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Verifica se o jogador está caindo ou pulando na cabeça do Boss
            if (col.contacts[0].point.y > transform.position.y) // Verifica se a colisão foi no topo do Boss
            {
                jumpHits++; // Incrementa o contador de saltos
                if (jumpHits >= 3) // Quando o contador chega a 3, o Boss morre
                {
                    Die(); // Chama a função para o Boss morrer
                }
            }
            else
            {
                col.gameObject.GetComponent<LifeScript>().LoseLife(); // Se o jogador não pulou na cabeça, ele recebe dano
            }
        }
    }

    void Die()
    {
        // Lógica para quando o Boss morrer, como destruir ou outras ações
        Destroy(gameObject); // O Boss é destruído
    }
}