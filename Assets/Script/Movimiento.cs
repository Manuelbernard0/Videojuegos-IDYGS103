using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movimiento : MonoBehaviour
{
    public float fuerza = 6f;
    public Rigidbody2D rigidbody2;
    public SpriteRenderer propulsor;
    private Vector2 direccion;

    void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direccion = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) direccion.y += 1f;
        if (Input.GetKey(KeyCode.S)) direccion.y -= 1f;
        if (Input.GetKey(KeyCode.A)) direccion.x -= 1f;
        if (Input.GetKey(KeyCode.D)) direccion.x += 1f;

        if (propulsor == null) return;
        if (direccion.y > 0f)
            propulsor.color = new Color(1f, 0.5f, 0f);
        else if (direccion.y < 0f)
            propulsor.color = Color.red;
        else if (direccion.x != 0f)
            propulsor.color = Color.yellow;
    }

    void FixedUpdate()
    {
        rigidbody2.linearVelocity = direccion.normalized * fuerza;
    }
}
