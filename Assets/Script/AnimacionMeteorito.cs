using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimacionMeteorito : MonoBehaviour
{
    public Sprite[] fotogramas;
    public float cuadrosPorSegundo = 10f;

    private SpriteRenderer imagen;
    private float tiempo;
    private int indice;

    void Awake()
    {
        imagen = GetComponent<SpriteRenderer>();

        if (fotogramas != null && fotogramas.Length > 0)
        {
            imagen.sprite = fotogramas[0];
        }
    }

    void Update()
    {
        if (fotogramas == null ||
            fotogramas.Length == 0 ||
            cuadrosPorSegundo <= 0f)
        {
            return;
        }

        tiempo += Time.deltaTime;

        float duracionCuadro = 1f / cuadrosPorSegundo;

        while (tiempo >= duracionCuadro)
        {
            tiempo -= duracionCuadro;

            indice = (indice + 1) % fotogramas.Length;

            imagen.sprite = fotogramas[indice];
        }
    }
}