using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 1;
    private Vector2 direction; // Direção do movimento do projétil

    void Start()
    {
        // Inicializa a direção se ainda não foi configurada
        if (direction != Vector2.zero)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * speed; // Aplica a direção e a velocidade
            }
        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized; // Normaliza a direção para garantir movimento consistente
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Dano ao jogador
            LifeScript playerLife = col.gameObject.GetComponent<LifeScript>();
            if (playerLife != null)
            {
                playerLife.TakeDamage(damage); // Método que causa o dano
            }
            Destroy(gameObject);  // Destrói o projétil após o impacto
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            // Se colidir com outros inimigos, você pode adicionar outra lógica aqui
            Destroy(gameObject); // Destrói o projétil ao colidir com qualquer outro objeto
        }
    }
}
