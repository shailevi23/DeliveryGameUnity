using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject driverToFollow;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        transform.position = driverToFollow.transform.position + new Vector3(0, 0, -10);
    }
}
