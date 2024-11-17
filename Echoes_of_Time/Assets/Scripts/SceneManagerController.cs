using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController instance;

    private void Awake()
    {
        // Singleton para garantir que apenas uma instância do gerenciador de cena exista
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Não destrói ao trocar de cena
        }
        else
        {
            Destroy(gameObject); // Destrói a nova instância
        }
    }

    public void TrocarCena(string nomeCena)
    {
        // Encontra o jogador ao trocar de cena
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            // Reinicia o jogador antes de trocar a cena
            player.GetComponent<LifeScript>().ResetPlayer(); // Chama o método para reiniciar o jogador
        }
        else
        {
            Debug.LogWarning("Jogador não encontrado ao tentar trocar de cena.");
        }

        // Carrega a nova cena
        SceneManager.LoadScene(nomeCena);
    }
}
