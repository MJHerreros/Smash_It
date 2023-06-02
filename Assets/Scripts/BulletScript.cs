using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioClip Sound;
    public float Speed;

    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
{
    Direction = direction.normalized;  // Normaliza la dirección para asegurar que tenga magnitud 1
    transform.localScale = new Vector3(direction.x > 0 ? 0.4f : -0.4f, 0.4f, 0.4f);
}

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      PayasoScript Payaso = collision.collider.GetComponent<PayasoScript>();
         if (Payaso != null)
        {
            Payaso.Hit();
        }
    }
}
