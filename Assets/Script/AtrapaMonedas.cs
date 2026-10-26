using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AtrapaMonedas : MonoBehaviour
{
    public float velocidad = 5f;
    public int monedas;
    public int objetivo = 5;
    public int vidas = 3;
    public UnityEngine.UI.Text contador;
    public UnityEngine.UI.Text contadorVidas;
    public GameObject mensajeVictoria;
    public GameObject mensajeDerrota;
    private Rigidbody2D cuerpo;
    private Vector2 inicio;
    private Vector2 direccion;
    private float proximoGolpe;

    public bool Terminado => monedas >= objetivo || vidas <= 0;

    void Awake()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        inicio = cuerpo.position;
    }

    void Update()
    {
        direccion = Vector2.zero;
        if (Terminado) return;
        if (Input.GetKey(KeyCode.W)) direccion.y += 1f;
        if (Input.GetKey(KeyCode.S)) direccion.y -= 1f;
        if (Input.GetKey(KeyCode.A)) direccion.x -= 1f;
        if (Input.GetKey(KeyCode.D)) direccion.x += 1f;
    }

    void FixedUpdate()
    {
        cuerpo.linearVelocity = direccion.normalized * velocidad;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (Terminado) return;
        if (otro.name.StartsWith("Moneda") && otro.gameObject.activeSelf)
        {
            otro.gameObject.SetActive(false);
            monedas++;
            contador.text = "Monedas: " + monedas + " / " + objetivo;
            if (monedas >= objetivo)
            {
                direccion = Vector2.zero;
                cuerpo.linearVelocity = Vector2.zero;
                mensajeVictoria.SetActive(true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (Terminado || Time.time < proximoGolpe) return;
        if (colision.gameObject.GetComponent<EnemigoMovil>() != null)
        {
            vidas--;
            contadorVidas.text = "Vidas: " + vidas;
            proximoGolpe = Time.time + 0.75f;
            direccion = Vector2.zero;
            cuerpo.position = inicio;
            cuerpo.linearVelocity = Vector2.zero;

            if (vidas <= 0)
                mensajeDerrota.SetActive(true);
        }
    }
}
