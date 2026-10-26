using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EfectoMeteorito : MonoBehaviour
{
    [Header("Animacion visual")]
    public float rapidez = 12f;

    [Range(0f, 0.2f)]
    public float intensidadPulso = 0.05f;

    public float inclinacion = 4f;
    public float desplazamiento = 0.03f;

    private SpriteRenderer imagen;
    private Vector3 escalaOriginal;
    private Vector3 posicionOriginal;
    private Quaternion rotacionOriginal;
    private float tiempo;

    void Awake()
    {
        imagen = GetComponent<SpriteRenderer>();

        escalaOriginal = transform.localScale;
        posicionOriginal = transform.localPosition;
        rotacionOriginal = transform.localRotation;

        // Evita que todos se animen exactamente al mismo tiempo.
        tiempo = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        // Mantiene la imagen quieta al terminar la partida.
        if (Time.timeScale == 0f)
            return;

        tiempo += Time.deltaTime * rapidez;

        float onda = Mathf.Sin(tiempo);

        // Desplazamiento lateral mediante Transform.
        transform.localPosition =
            posicionOriginal +
            Vector3.right * onda * desplazamiento;

        // Pulso de tamaño.
        transform.localScale =
            escalaOriginal * (1f + onda * intensidadPulso);

        // Pequeña inclinación hacia ambos lados.
        transform.localRotation =
            rotacionOriginal *
            Quaternion.Euler(0f, 0f, onda * inclinacion);

        // Variación de tono.
        float mezcla = (onda + 1f) / 2f;

        imagen.color = Color.Lerp(
            new Color(1f, 0.75f, 0.5f, 1f),
            Color.white,
            mezcla
        );
    }
}