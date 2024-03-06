using UnityEngine;

public class TriggerHandler : MonoBehaviour, ITriggerHandler
{
    public bool IsPackageTriggered(Collider2D collider)
    {
        return collider.CompareTag("Package");
    }

    public bool IsCustomerTriggered(Collider2D collider)
    {
        return collider.CompareTag("Customer");
    }

    public bool IsBoostTriggered(Collider2D collider)
    {
        return collider.CompareTag("Boost");
    }

    public bool IsGrassTriggered(Collider2D collider)
    {
        return collider.CompareTag("Grass");
    }

    public bool IsRoadTriggered(Collider2D collider)
    {
        return collider.CompareTag("Road");
    }
}
