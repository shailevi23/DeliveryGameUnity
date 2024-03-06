using UnityEngine;

public interface ICollisionHandler
{
    bool IsDecorationCollision(Collision2D collision);
    bool IsRespawnDriverCollision(Collision2D collision);
}
