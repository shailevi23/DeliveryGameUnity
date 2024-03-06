using UnityEngine;

public class CollisionHandler : MonoBehaviour, ICollisionHandler
{
    public bool IsDecorationCollision(Collision2D collision)
    {
        return collision.collider.CompareTag("Decoration");
    }

    public bool IsRespawnDriverCollision(Collision2D collision)
    {
        return collision.collider.CompareTag("RespawnDriver");
    }

    public bool IsGrassCollision(Collision2D collision)
    {
        return collision.collider.CompareTag("Grass");
    }
}
