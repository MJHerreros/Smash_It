using UnityEngine;

public class PayasoScript : MonoBehaviour
{
    public AudioClip Sound;
    public AudioClip Sound2;
    public GameObject Bullet2Prefab;
    public GameObject Cabra;
    public event System.EventHandler MuerteJugador;

    private float LastShoot;
    private int Health = 4;
    private float speed = 2.5f; // Velocidad de movimiento del payaso
    private float jumpForce = 10f; // Fuerza de salto del payaso
    private bool isGrounded = false; // Indica si el payaso está en el suelo
    private bool hasJumped = false;

    private void Update()
    {
        // Calcula la dirección y distancia entre el payaso y la cabra
        Vector3 direction = Cabra.transform.position - transform.position;
        float distance = Mathf.Abs(Cabra.transform.position.x - transform.position.x);

        // Actualiza la escala del payaso para enfrentarlo a la cabra
    if (direction.x >= 0.35f)
        transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
    else
        transform.localScale = new Vector3(-0.35f, 0.35f, 0.35f);

    // Movimiento horizontal
        if (distance < 5.0f)
    {
        // Mueve el payaso hacia la cabra en el eje x
        transform.position += direction.normalized * speed * Time.deltaTime;

            // Salto
        if (isGrounded && !hasJumped)
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        hasJumped = true;
    }

    }


    // Disparo
    if (distance < 5.0f && Time.time > LastShoot + 0.35f)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
        Shoot();
        LastShoot = Time.time;
    }
}

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f)
            direction = Vector2.right;
        else
            direction = Vector2.left;

        GameObject Bullet2 = Instantiate(Bullet2Prefab, transform.position + direction * 0.3f, Quaternion.identity);
        Bullet2.GetComponent<BulletScript2>().SetDirection(direction);
    }

    public void Hit()
    {
        Health--;
        if (Health == 0)
        {
            Destroy(gameObject);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound2);
            MuerteJugador?.Invoke(this, System.EventArgs.Empty);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}