using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeScript : MonoBehaviour
{
    public bool alive = true;
    private Vector3 respawnPosition; // Posição de respawn do jogador
    public int health = 3;  // Vida do jogador

    void Start()
    {
        respawnPosition = transform.position; // Define a posição inicial de respawn
    }

    void Update()
    {
        // Verifica condições de vida, se necessário
    }

    public void TakeDamage(int damage)
    {
        if (alive)
        {
            health -= damage;
            if (health <= 0)
            {
                LoseLife(); // Chama a lógica de morte se a vida chegar a 0 ou menos
            }
        }
    }

    public void LoseLife()
    {
        if (alive)
        {
            alive = false;
            // Chama a animação de morte
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Dead");
            }

            // Para qualquer movimento e interações do jogador
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic; // Desativa física do jogador
            }

            // Desativa colisão e interações com o jogador
            CapsuleCollider2D collider = gameObject.GetComponent<CapsuleCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Desativa o script de controle do jogador
            Player playerScript = gameObject.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.enabled = false;
            }

            // Atualiza o controlador de vidas
            GameController.gc.SetLives(-1);

            if (GameController.gc.lives >= 0)
            {
                StartCoroutine(Respawn()); // Inicia o respawn
            }
            else
            {
                Invoke("LoadGameOver", 2f);
                GameController.gc.lives = 3; // Resetar as vidas
            }
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        transform.position = respawnPosition; // Renasce no último checkpoint salvo
        ResetPlayer();
    }

    void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void ResetPlayer()
    {
        alive = true;

        // Reativa colisão e física
        CapsuleCollider2D collider = gameObject.GetComponent<CapsuleCollider2D>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        // Reativa o controle do jogador
        Player playerScript = gameObject.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o jogador colidiu com um checkpoint
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            respawnPosition = collision.transform.position; // Atualiza a posição de respawn para o checkpoint
        }
    }
}
