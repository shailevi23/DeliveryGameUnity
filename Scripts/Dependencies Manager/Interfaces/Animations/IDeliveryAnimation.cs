using System.Collections;
using UnityEngine;

public interface IDeliveryAnimation
{
    IEnumerator PackageDelivered(SpriteRenderer otherSpriteRenderer);
}
