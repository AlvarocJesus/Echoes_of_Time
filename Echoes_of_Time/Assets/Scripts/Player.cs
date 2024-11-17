using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;

    private Animator playerAnim;
    private Rigidbody2D rbPlayer;
    public float speed;
    private SpriteRenderer srPlayer;
    public float jumpForce;
    public bool inFloor = true;
    private GameController gcPlayer;
    public int contpulos = 0;
    private Boss boss;
    public AudioClip coinSound; // Som a ser tocado
    public AudioClip jumpSound; // Som do pulo
    public AudioClip robot; // Som do dano
    public AudioClip bugabuga;
    public AudioClip kinaa;
    public AudioClip Walking;
    public AudioClip check;
    public AudioClip ark;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gcPlayer = GameController.gc;
        gcPlayer.coins = 0;
        playerAnim = GetComponent<Animator>();
        srPlayer = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
        boss = FindObjectOfType<Boss>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        Jump();
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(moveHorizontal * Time.deltaTime * speed, 0, 0);
        rbPlayer.velocity = new Vector2(moveHorizontal * speed, rbPlayer.velocity.y);

        if (moveHorizontal > 0)
        {
            playerAnim.SetBool("Walking", true);
            srPlayer.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            playerAnim.SetBool("Walking", true);
            srPlayer.flipX = true;
        }
        else
        {
            playerAnim.SetBool("Walking", false);
        }
        

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && inFloor)
        {
            playerAnim.SetBool("Jump", true);
            rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            inFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Graud" && collision.gameObject.tag == "Plataforma")
        {
            transform.parent = collision.transform;
        }
        {
            playerAnim.SetBool("Jump", false);
            inFloor = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coins")
        {
            Destroy(collision.gameObject);
            gcPlayer.coins++;
            GameController.gc.RefreshUI();

            if (coinSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(coinSound);
            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            rbPlayer.velocity = Vector2.zero;
            rbPlayer.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            collision.gameObject.GetComponent<Enemy>().enabled = false;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Destroy(collision.gameObject, 1f);

            gcPlayer.AddScore(10); 
            if (bugabuga != null && audioSource != null)
            {
                audioSource.PlayOneShot(bugabuga);
            }
        }
        if (collision.gameObject.tag == "robot")
        {
            rbPlayer.velocity = Vector2.zero;
            rbPlayer.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            collision.gameObject.GetComponent<Enemy>().enabled = false;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Destroy(collision.gameObject, 1f);

            gcPlayer.AddScore(10); 
            if (robot != null && audioSource != null)
            {
                audioSource.PlayOneShot(robot);
            }
        }
        if (collision.gameObject.tag == "kina")
        {
            rbPlayer.velocity = Vector2.zero;
            rbPlayer.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            collision.gameObject.GetComponent<Enemy>().enabled = false;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Destroy(collision.gameObject, 1f);

            gcPlayer.AddScore(10); 

            if (kinaa != null && audioSource != null)
            {
                audioSource.PlayOneShot(kinaa);
            }
        }
        if (collision.gameObject.tag == "Checkpoint")
        {
            if (check != null && audioSource != null)
            {
                audioSource.PlayOneShot(check);
            }
        }
        // if (collision.gameObject.tag == "The ark")
        // {
        //     if (ark != null && audioSource != null)
        //     {
        //         audioSource.PlayOneShot(ark);
        //     }
        // }


        if (collision.gameObject.tag == "Boss")
        {
            if(contpulos == 3){
                rbPlayer.velocity = Vector2.zero;
                rbPlayer.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<SpriteRenderer>().flipY = true;
                collision.gameObject.GetComponent<Boss>().enabled = false;
                collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                gcPlayer.AddScore(1000); 
                contpulos = 0;
                Destroy(collision.gameObject, 1f);
                Invoke("Telawin", 2.5f);
            }else{
                contpulos++;
                rbPlayer.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                if (jumpSound != null && audioSource != null)
                {
                audioSource.PlayOneShot(jumpSound);
                }
                if(boss.speed < 0){
                    boss.speed -= 0.7f;
                }else{
                    boss.speed += 0.7f;
                }
            }
        }
    }

    void Telawin()
    {
        SceneManager.LoadScene("Win");
    }
}
