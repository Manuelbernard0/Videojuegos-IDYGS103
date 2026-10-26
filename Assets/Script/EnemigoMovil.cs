using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemigoMovil : MonoBehaviour
{
    public Vector2 direccion = Vector2.right;
    public float velocidad = 2.5f;
    public AtrapaMonedas juego;
    private Rigidbody2D cuerpo;

    void Awake()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        direccion = direccion.normalized;
    }

    void FixedUpdate()
    {
        if (juego != null && juego.Terminado)
            cuerpo.linearVelocity = Vector2.zero;
        else
            cuerpo.linearVelocity = direccion * velocidad;
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.contactCount > 0)
            direccion = Vector2.Reflect(direccion, colision.GetContact(0).normal).normalized;
    }
}
