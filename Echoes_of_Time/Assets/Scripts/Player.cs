using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D rbPlayer;
    public float speed;
    private SpriteRenderer srPlayer;
    public float jumpForce;
    public bool inFloor = true;

    private GameController gcPlayer;

    void Start()
    {
        gcPlayer = GameController.gc;
        gcPlayer.coins = 0;
        playerAnim = GetComponent<Animator>();
        srPlayer = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    void MovePlayer(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Debug.Log(moveHorizontal);
        transform.position += new Vector3(moveHorizontal * Time.deltaTime * speed, 0, 0);
        rbPlayer.velocity = new Vector2(moveHorizontal * speed, rbPlayer.velocity.y);

        if(moveHorizontal > 0){
            playerAnim.SetBool("Walking", true);
            srPlayer.flipX = false;
        }else if(moveHorizontal < 0){
            playerAnim.SetBool("Walking", true);
            srPlayer.flipX = true;
        }else{
            playerAnim.SetBool("Walking", false);
        }
    }

    void Jump(){
        if(Input.GetButtonDown("Jump") && inFloor){
            playerAnim.SetBool("Jump", true);
            rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            inFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Graud"){
            playerAnim.SetBool("Jump", false);
            inFloor = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coins"){
            Destroy(collision.gameObject);
            gcPlayer.coins++;
            GameController.gc.RefreshUI();
        }
        if(collision.gameObject.tag == "Enemy"){
            rbPlayer.velocity = Vector2.zero;
            rbPlayer.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            collision.gameObject.GetComponent<Enemy>().enabled = false;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Destroy(collision.gameObject, 1f);

            gcPlayer.AddScore(10);
        }
    }

}
