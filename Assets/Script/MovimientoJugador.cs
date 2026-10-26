using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 6f;

    // Actívalo si la imagen original mira hacia la derecha.
    public bool spriteMiraDerecha = true;

    private Rigidbody2D cuerpo;
    private SpriteRenderer imagen;
    private float direccion;

    void Awake()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        imagen = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        direccion = 0f;

        // Evita cambiar la orientación cuando termina el juego.
        if (Time.timeScale == 0f)
            return;

        if (Input.GetKey(KeyCode.A))
            direccion -= 1f;

        if (Input.GetKey(KeyCode.D))
            direccion += 1f;

        // Conserva la última orientación cuando dejas de moverte.
        if (direccion < 0f)
            imagen.flipX = spriteMiraDerecha;
        else if (direccion > 0f)
            imagen.flipX = !spriteMiraDerecha;
    }

    void FixedUpdate()
    {
        // Movimiento horizontal; sin velocidad vertical.
        cuerpo.linearVelocity = new Vector2(
            direccion * velocidad,
            0f
        );
    }
}