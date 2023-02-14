using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reynamovements : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    private Rigidbody2D Rigidbody2D; //referencia, direccion al rigidbody 2D
    private float Horizontal;
    private bool Grounded; //true o false, (true) estamos en el suelo

    void Start()// lo va a ejecutar, inicia acá
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update() // la lógica del juego empieza de aquí a abajo
    {
        // lo primero es poner el teclado del jugador

        Horizontal = Input.GetAxisRaw("Horizontal");  //1.5f, 5,6f valores reales (float)

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }

        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump(); //si apretamos la W saltaremos
        }
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);

    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y); // modificaremos la x que es la de izq-der
    }
}
