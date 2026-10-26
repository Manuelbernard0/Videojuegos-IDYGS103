using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstaculo : MonoBehaviour
{
    public float fuerzaCaida = 20f;

    private Rigidbody2D cuerpo;
    private ControlJuego juego;
    private bool resuelto;

    void Awake()
    {
        cuerpo = GetComponent<Rigidbody2D>();
    }

    public void Inicializar(ControlJuego controlador)
    {
        juego = controlador;
    }

    void FixedUpdate()
    {
        if (juego == null || juego.terminado || resuelto)
            return;

        // Aplica fuerza hacia abajo para acelerar el meteorito.
        cuerpo.AddForce(
            Vector2.down * fuerzaCaida,
            ForceMode2D.Force
        );

        // Limita la velocidad de caída según la dificultad.
        float limite = juego.VelocidadCaida;

        if (cuerpo.linearVelocity.y < -limite)
        {
            cuerpo.linearVelocity = new Vector2(
                cuerpo.linearVelocity.x,
                -limite
            );
        }
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (juego == null || juego.terminado || resuelto)
            return;

        // Comprueba si golpeó al jugador.
        MovimientoJugador jugador =
            colision.gameObject.GetComponent<MovimientoJugador>();

        if (jugador != null)
        {
            resuelto = true;

            SpriteRenderer imagenJugador =
                jugador.GetComponent<SpriteRenderer>();

            if (imagenJugador != null)
            {
                imagenJugador.color = Color.red;
            }

            juego.Terminar();
            return;
        }

        // Si llegó al suelo, suma un punto y desaparece.
        if (colision.gameObject.name == "Suelo")
        {
            resuelto = true;
            juego.SumarPunto();
            Destroy(gameObject);
        }
    }
}