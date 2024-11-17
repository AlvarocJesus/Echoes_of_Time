using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float amplitude = 2f; // Amplitude do movimento no eixo Y
    public float speed = 2f;     // Velocidade do movimento

    private float startY; // Posição inicial no eixo Y

    void Start()
    {
        startY = transform.position.y; // Armazena a posição inicial
    }

    void Update()
    {
        // Calcula o novo valor de Y usando o movimento PingPong
        float newY = startY + Mathf.PingPong(Time.time * speed, amplitude) - amplitude / 2;
        
        // Atualiza a posição da plataforma
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
