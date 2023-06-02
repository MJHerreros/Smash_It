using UnityEngine;

public class JumpThroughPlatform : MonoBehaviour
{
    public LayerMask platformLayer;

    private Collider2D playerCollider;
    private bool canJumpThroughPlatform = true;

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Detectar si el jugador intenta saltar mientras está en una plataforma
        if (canJumpThroughPlatform && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Implementa la lógica de salto aquí
        // Desactivar temporariamente la colisión con la capa de la plataforma
        Physics2D.IgnoreLayerCollision(playerCollider.gameObject.layer, platformLayer, true);
        canJumpThroughPlatform = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detectar si el jugador ha colisionado con una plataforma
        if (platformLayer == (platformLayer | (1 << collision.gameObject.layer)))
        {
            // Volver a activar la colisión con la capa de la plataforma
            Physics2D.IgnoreLayerCollision(playerCollider.gameObject.layer, platformLayer, false);
            canJumpThroughPlatform = true;
        }
    }
}