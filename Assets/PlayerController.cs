using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 6.5f;

    public AudioClip jumpClip;
    public AudioClip dieClip;
    

	private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spr;
    private bool jump;
    private bool doubleJump;
    private bool puedoMoverme = true;
    private AudioSource audioPlayer;
    

    // Start is called before the first frame update
    void Start()
    {
		rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        audioPlayer = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed",Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("tocando_suelo", grounded);
        //Debug.Log("bool: " + grounded);

        //SALTO DE PRECAUCION, (sirve para tener un salto guardado por si me caigo
        if (grounded)
        {
            doubleJump = true;
        }

        //PARA SALTOS (si estoy en el piso
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (grounded)
            {
                jump = true;
                doubleJump = true;
            }else if(doubleJump)
            {
                jump = true;
                doubleJump = false;
            }
            
        }
    }

    void FixedUpdate() {

        //PARA FRENADO DEL PJ EN EL SUELO
        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x*= 0.75f;
        if (grounded) {
            rb2d.velocity = fixedVelocity;
        }

        float h = Input.GetAxis("Horizontal"); //Save en h 1 si presiono DER o -1 si presiono IZQ
        if(!puedoMoverme) { h = 0; }
        
        rb2d.AddForce(Vector2.right * speed * h);

        //CONTROLAR VELOCIDAD MAXIMA
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        //Cambiar sprite cuando el personaje se mueve hacia la IZQ o DER
        if (h > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        //CONFIG PARA SALTOS

        if(jump == true)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //Vector2.up indica el sentido del vector
            jump = false;
            //SONIDO AGRAGADO 
            audioPlayer.clip = jumpClip;
            audioPlayer.Play();
        }

        //Debug.Log(rb2d.velocity.x);
    }
    //PARA RESETEAR EL PLAYER CUANDO MORIMOS
    void OnBecameInvisible() {
        transform.position = new Vector3(-6, 0, 0);
    }

    //Para rebotar cuando pisamos al enemigo
    public void EnemyJump()
    {
        jump = true;
    }

    public void ReboteXDanoEnemigo(float enemyPosX)
    {
        jump = true;

        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);

        puedoMoverme = false;
        Invoke("ActivarMovimiento", 0.7f);
        spr.color = Color.red;
    }

     void ActivarMovimiento()
    {
        puedoMoverme = true;
        spr.color = Color.white;
    }
}
