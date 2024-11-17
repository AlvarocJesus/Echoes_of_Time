using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionManager : MonoBehaviour
{
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica o nome da cena e define a posição inicial do Player
        if (scene.name == "Fase2")
        {
            Player.instance.transform.position = new Vector3(0, 0, 0); // Ajuste essa posição
        }
        else if (scene.name == "Fase3")
        {
            Player.instance.transform.position = new Vector3(5, 0, 0); // Ajuste essa posição
        }
        else if (scene.name == "Fase1")
        {
            Player.instance.transform.position = new Vector3(-5, 0, 0); // Posição inicial da Fase1
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
