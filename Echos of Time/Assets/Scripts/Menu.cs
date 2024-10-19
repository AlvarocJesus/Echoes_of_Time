using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject opcoes;
    [SerializeField] private GameObject menu;

    private int oi = 0;
    private int oi2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        FecharOpcoes();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Jogar()
    {
        SceneManager.LoadScene("fase1");
    }


    public void Opcoes()
    {
        menu.SetActive(false);
        opcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        menu.SetActive(true);
        opcoes.SetActive(false);
    }

    public void Sair()
    {
        Debug.Log("Saindo do jogo");
        Application.Quit();
    }
}
