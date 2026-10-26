using UnityEngine;

public class ControlJuego : MonoBehaviour
{
    public Obstaculo obstaculoPrefab;


    public BoxCollider2D paredIzquierda;
    public BoxCollider2D paredDerecha;

    public float alturaAparicion = 8f;
    public float margenLateral = 0.3f;

    public float velocidadInicial = 3f;
    public float aumentoPorPunto = 0.2f;
    public float velocidadMaxima = 12f;
    public float intervalo = 1f;

    public int puntos;
    public bool terminado;

    private float temporizador;

    public float VelocidadCaida
    {
        get
        {
            return Mathf.Min(
                velocidadInicial + puntos * aumentoPorPunto,
                velocidadMaxima
            );
        }
    }

    void Awake()
    {
        Time.timeScale = 1f;
        puntos = 0;
        terminado = false;
    }

    void Update()
    {
        if (terminado || obstaculoPrefab == null)
            return;

        temporizador += Time.deltaTime;

        if (temporizador >= intervalo)
        {
            temporizador = 0f;

            
            float limiteIzquierdo =
                paredIzquierda.bounds.max.x + margenLateral;

            float limiteDerecho =
                paredDerecha.bounds.min.x - margenLateral;

            
            Vector3 posicion = new Vector3(
                Random.Range(limiteIzquierdo, limiteDerecho),
                alturaAparicion,
                0f
            );

            Obstaculo nuevo = Instantiate(
                obstaculoPrefab,
                posicion,
                Quaternion.identity
            );

            nuevo.Inicializar(this);
        }
    }

    public void SumarPunto()
    {
        if (!terminado)
            puntos++;
    }

    public void Terminar()
    {
        if (terminado)
            return;

        terminado = true;
        Time.timeScale = 0f;
    }

    void OnGUI()
    {
        GUIStyle estiloPuntos = new GUIStyle(GUI.skin.label);

        estiloPuntos.fontSize = 18;
        estiloPuntos.normal.textColor = Color.white;
        estiloPuntos.fontStyle = FontStyle.Bold;
        estiloPuntos.alignment = TextAnchor.UpperLeft;

        
        GUI.Label(
            new Rect(20, 20, 400, 100),
            $"Puntos: {puntos}\nVelocidad: {VelocidadCaida:F1}",
            estiloPuntos
        );


        if (terminado)
        {
            GUI.Label(
                new Rect(
                    Screen.width / 2f - 150,
                    Screen.height / 2f - 50,
                    300,
                    100
                ),
                $"GAME OVER\nPuntos: {puntos}\n" +
                "El juego ha terminado."
            );
        }
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }
}