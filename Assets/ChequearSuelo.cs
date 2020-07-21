using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChequearSuelo : MonoBehaviour
{
    private PlayerController player;

    private Rigidbody2D rb2d; //objeto que se usa para setear la velocidad del prota

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        rb2d = GetComponentInParent<Rigidbody2D>(); //se obtiene componente del objeto padre, que en nuestro caso es el prota.
        
    }

    void OnCollisionEnter2D(Collision2D col) //evento que se ejecuta solo la primera vez que el jugador colisiona con otro objeto
    {
        if (col.gameObject.tag == "Platform")
        {
            rb2d.velocity = new Vector3(0f, 0f, 0f); //La primera vez que el prota toca una plataforma movil -> su velocidad es cero!
            player.transform.parent = col.transform; //ahora el padre de player es la plataforma  movil (lo hacemos para que el player se mueva junto a la plataforma)
            player.grounded = true;
            //Debug.Log("bool: " + player.grounded);
        }
    }

    void OnCollisionStay2D(Collision2D col) //evento que se ejecuta en cada fotograma, para ver si existe o no colision
    {
        if (col.gameObject.tag == "Ground")
        { player.grounded = true;
            //Debug.Log("bool: " + player.grounded);
        }

        if (col.gameObject.tag == "Platform")
        {
            player.transform.parent = col.transform; //ahora el padre de player es la plataforma  movil (lo hacemos para que el player se mueva junto a la plataforma)
            player.grounded = true;
            //Debug.Log("bool: " + player.grounded);
        }

    }

    void OnCollisionExit2D(Collision2D col) // evento que se ejecuta cuando se deja de colisionar
    {
        if (col.gameObject.tag == "Ground")
        { player.grounded = false;
            //Debug.Log("bool: " + player.grounded);
        }
        if (col.gameObject.tag == "Platform")
        {
            player.transform.parent = null; // ahora ya no es hijo de plataforma movil
            player.grounded = false;
            //Debug.Log("bool: " + player.grounded);
        }
    } 

   
}
