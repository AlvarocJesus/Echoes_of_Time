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
        // Destroi o objeto do jogador antes de trocar a cena
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
            Debug.Log("Jogador destruído antes de trocar de cena.");
        }

        // Carrega a nova cena
        SceneManager.LoadScene(nomeCena);
    }
}
