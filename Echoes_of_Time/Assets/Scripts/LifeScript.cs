using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeScript : MonoBehaviour
{
    public bool alive = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoseLife(){
        
        if(alive){
            alive = false;
            gameObject.GetComponent<Animator>().SetTrigger("Dead");
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.GetComponent<Player>().enabled = false;
            gameObject.GetComponent<Animator>().SetBool("Jump", false);
            GameController.gc.SetLives(-1);
            Invoke("LoadScene", 2f);

            if(GameController.gc.lives >= 0)
            {
                Invoke("LoadScene", 2f);
            }
            else
            {
                Invoke("Load", 2f);
                GameController.gc.lives = 3;
            }
        }
    }

    void Load(){
        SceneManager.LoadScene("GameOver");
    }


    void LoadScene(){
        SceneManager.LoadScene("SampleScene");
    }
}
