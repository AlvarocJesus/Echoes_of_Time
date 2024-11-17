using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine;

public class AmuletoTheArk : MonoBehaviour
{
    private bool amuletoColetado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisão detectada com: " + other.name);

        if (other.CompareTag("Player"))
        {
            amuletoColetado = true;  // Define como true quando o jogador coleta
            Debug.Log("Amuleto coletado pelo jogador! Novo estado: " + amuletoColetado);
            
            // Chame a função para não destruir o objeto
            DontDestroyOnLoad(gameObject);
            
            // Desativa o sprite do amuleto (se necessário)
            GetComponent<SpriteRenderer>().enabled = false; 
        }
    }

    private void Update()
    {
        //Debug.Log("Update chamado! Amuleto coletado: " + amuletoColetado); // Log para verificar estado

        if (amuletoColetado == true)  // Verifica se o amuleto foi coletado
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Tecla P pressionada!");  // Confirma que a tecla P foi pressionada
                TrocarFase();
            }
        }
    }

    private void TrocarFase()
{
    string cenaAtual = SceneManager.GetActiveScene().name;
    Debug.Log("Cena atual: " + cenaAtual);

    if (cenaAtual == "SampleScene")
    {
        Debug.Log("Trocando para Fase2...");
        SceneManagerController.instance.TrocarCena("Fase2");
    }
    else if (cenaAtual == "Fase2")
    {
        Debug.Log("Trocando para Fase3...");
        SceneManagerController.instance.TrocarCena("Fase3");
    }
    else if (cenaAtual == "Fase3")
    {
        Debug.Log("Trocando para SampleScene...");
        SceneManagerController.instance.TrocarCena("SampleScene");
    }
    else
    {
        Debug.Log("Nome da cena não reconhecido!");
    }
}

}
