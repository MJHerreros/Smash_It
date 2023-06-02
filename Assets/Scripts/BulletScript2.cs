using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript2 : MonoBehaviour
{
    public MovimientoCabra scriptCabra;

    public AudioClip Sound;
    public float Speed;
    public float DestroyDelay = 3f;

    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;

    private void Start()
    {
        scriptCabra = FindObjectOfType<MovimientoCabra>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(DestroyDelay);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction.normalized;
        transform.localScale = new Vector3(direction.x > 0 ? 0.4f : -0.4f, 0.4f, 0.4f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            scriptCabra.Hit();
            Destroy(gameObject);
        } 
    }
}
