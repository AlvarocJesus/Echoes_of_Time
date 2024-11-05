using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController gc;
    public Text txtCoins;
    public int coins;
    public Text txtLives;
    public int lives = 3;
    public int score;  // Campo para armazenar o score
    public Text tScore;

    // Novo campo para armazenar o estado do amuleto
    public bool amuletoColetado = false;

    void Awake()
    {
        if(gc == null)
        {
            gc = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(gc != this)
        {
            Destroy(gameObject);
        }
        RefreshUI();
    }

    public void SetLives(int life)
    {
        lives += life;
        if(lives >= 0)
            RefreshUI();
    }

    public void AddScore(int points)
    {
        score += points;
        RefreshUI(); 
    }

    public void RefreshUI()
    {
        txtCoins.text = coins.ToString();
        txtLives.text = lives.ToString();
        tScore.text = score.ToString();
    }
}

