using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform Player; // Referência ao Transform do jogador
    public float minX; // Limite mínimo do eixo X
    public float maxX; // Limite máximo do eixo X
    public float timeLerp; // Tempo de interpolação

    private void Awake()
    {
        // Tente encontrar o jogador na cena
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player").transform;
        }
    }

    void FixedUpdate()
    {
        // Verifique se o jogador está atribuído antes de tentar acessar sua posição
        if (Player != null)
        {
            Vector3 newPosition = Player.position + new Vector3(0, 0, -10);
            newPosition.y = 0.1f;
            newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);
            transform.position = newPosition;

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
        }
        else
        {
            Debug.LogWarning("Player não encontrado! Verifique se o objeto do jogador tem a tag 'Player'.");
        }
    }
}
