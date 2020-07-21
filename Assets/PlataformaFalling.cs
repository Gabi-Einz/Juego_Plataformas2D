using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaFalling : MonoBehaviour
{
    public float fallDelay = 1f; //tiempo hasta que caiga la plataforma
    public float respawnDelay = 5f; //tiempo de reaparicion de la plataforma

    private Rigidbody2D rb2d;
    private PolygonCollider2D pc2d;
    private Vector3 start; //variable para guardar la posicion inicial de la plataforma

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<PolygonCollider2D>();
        start = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            Invoke("Fall", fallDelay); //uso Invoke para llamar a la funcion Fall, y que se ejecute luego de un fallDelay
            Invoke("Respawn", fallDelay + respawnDelay);       
        }
    }

    void Fall()
    {
        rb2d.isKinematic = false;
        pc2d.isTrigger = true; //es una forma de desactivar temporalmente la colision
    }

    void Respawn()
    {
        transform.position = start;
        rb2d.isKinematic = true;
        rb2d.velocity = Vector3.zero;
        pc2d.isTrigger = false;
    }
}
