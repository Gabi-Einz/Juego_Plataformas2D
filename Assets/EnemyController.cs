using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 1f;
    public float speed = 1f;

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.AddForce(Vector2.right * speed); //speed me indica el sentido de movimiento(izq o der)

        //CONTROLAR VELOCIDAD MAXIMA
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if(rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f) //si la velocidad del enemigo es cercana a 0 -> cambio su sentido de movimiento
        {
            speed = -speed;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

        }

        //Cambiar sprite cuando el personaje se mueve hacia la IZQ o DER
        if (speed < 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (speed > -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D col) //para detectar cuando un enemigo colisiona con el protagonista
    {
        if(col.gameObject.tag == "Player")
        {
            float yOffset = 0.4f;

            if(transform.position.y + yOffset < col.transform.position.y)
            {
                //Debug.Log("PLAYER DETECTED!");
                col.SendMessage("EnemyJump");
                Destroy(gameObject);
            }
            else
            {
                col.SendMessage("ReboteXDanoEnemigo",transform.position.x);
            }
            
        }
    }
}
