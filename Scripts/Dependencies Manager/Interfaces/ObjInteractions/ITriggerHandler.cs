using UnityEngine;

public interface ITriggerHandler
{
    bool IsPackageTriggered(Collider2D collider);
    bool IsCustomerTriggered(Collider2D collider);
    bool IsBoostTriggered(Collider2D collider);
    bool IsGrassTriggered(Collider2D collider);
    bool IsRoadTriggered(Collider2D collider);
}
