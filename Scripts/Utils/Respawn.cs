using UnityEngine;

public class Respawn : MonoBehaviour
{
    private ICollisionHandler _collisionHandler;

    private void Start()
    {
        _collisionHandler = DependencyManager.Instance.CollisionHandler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_collisionHandler.IsRespawnDriverCollision(collision))
        {
            transform.position = new Vector3(0, 2, 0);
        }
    }
}
