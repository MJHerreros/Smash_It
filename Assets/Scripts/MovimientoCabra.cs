using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovimientoCabra : MonoBehaviour
{
    public AudioClip Sound;
    public AudioClip Sound2;
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce = 10f;
    public event EventHandler MuerteJugador;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private int Health = 5;
    private bool ignoreDoubleJump;


    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
            ignoreDoubleJump = false;
        }
        /*else
        {
            Grounded = false;
        }*/

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump(); 
            Grounded = false;

        }
        
        /*if (Input.GetKeyDown(KeyCode.W) && !Grounded)
        {
            Jump();

        }
        
        /*if(Input.GetKeyUp(KeyCode.W) && !Grounded && totalJumps !=2)
        {
            Grounded = true;
            jumpCounter = 2;
            ignoreDoubleJump = false;
        }*/

        if (Horizontal < 0.0f)
            transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        else if (Horizontal > 0.0f)
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        Animator.SetBool("correr", Horizontal != 0.0f);

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Jump()
    {
        Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 0.3f)
            direction = Vector2.right;
        else
            direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.3f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(Horizontal * Speed * Time.deltaTime, Rigidbody2D.velocity.y);
        Rigidbody2D.velocity = movement;
    }

    public void Hit()
    {
        Health--;
        if (Health == 0)
        {
            Destroy(gameObject);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound2);
            MuerteJugador?.Invoke(this, EventArgs.Empty);
        }
    }
}