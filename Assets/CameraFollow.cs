using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject follow;
    public Vector2 minCamPos, maxCamPos;
    public float smoothTime; //para efecto de suavizado de movimiento de camara (tiempo de retardo)

    private Vector2 velocity; //para gestionar la velocidad de la camara para smoothTime

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Optimizamos al usar FixedUpdate() y no Update()
    void FixedUpdate() //Se ejecuta el mismo numero de veces que el frameRate del juego, en cambio Update() se ejecuta tantas veces como la cpu sea capaz de procesar
    {
        float posX = Mathf.SmoothDamp(transform.position.x, follow.transform.position.x,ref velocity.x,smoothTime); //para que la camara siga al prota pero con un delay
        //float posY = follow.transform.position.y;
        float posY = Mathf.SmoothDamp(transform.position.y, follow.transform.position.y, ref velocity.y, smoothTime);

        transform.position = new Vector3(
            Mathf.Clamp(posX,minCamPos.x,maxCamPos.x),
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
            transform.position.z);
    }
}
