using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public bool groud = true;
    public Transform GraudCheck;
    public LayerMask layer;
    public bool face = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        groud = Physics2D.Linecast(GraudCheck.position, transform.position, layer);
        Debug.Log(groud);

        if(groud == false){
            speed *= -1;
        }

        if(speed > 0 && !face){
            Flip();
        }else if(speed < 0 && face){
            Flip();
        }

    }
    void Flip(){
        face = !face;
        Vector3 scala = transform.localScale;
        scala.x *= -1;
        transform.localScale = scala;
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            col.gameObject.GetComponent<LifeScript>().LoseLife();
        }
    }
}
