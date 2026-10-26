using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Luna : MonoBehaviour
{
    [SerializeField] private Rigidbody2D nave;

    private Rigidbody2D luna;

    private Vector2 posicionInicialLuna;
    private float rotacionInicialLuna;

    private Vector2 posicionInicialNave;
    private float rotacionInicialNave;

    private bool reinicioPendiente;

    private void Awake()
    {
        luna = GetComponent<Rigidbody2D>();

        posicionInicialLuna = luna.position;
        rotacionInicialLuna = luna.rotation;

        if (nave != null)
        {
            posicionInicialNave = nave.position;
            rotacionInicialNave = nave.rotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            reinicioPendiente = true;
        }
    }

    private void FixedUpdate()
    {
        if (!reinicioPendiente)
        {
            return;
        }

        reinicioPendiente = false;

        if (nave != null)
        {
            Restablecer(nave, posicionInicialNave, rotacionInicialNave);
        }

        Restablecer(luna, posicionInicialLuna, rotacionInicialLuna);

        Debug.Log("Perdiste. Nuevo intento.");
    }

    private void Restablecer(
        Rigidbody2D objeto,
        Vector2 posicion,
        float rotacion)
    {
        objeto.linearVelocity = Vector2.zero;
        objeto.angularVelocity = 0f;

        objeto.position = posicion;
        objeto.rotation = rotacion;
    }
}
