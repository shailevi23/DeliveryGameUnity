using System.Collections;
using UnityEngine;

public class DeliveryAnimation : MonoBehaviour, IDeliveryAnimation
{
    float flickerDuration = 3f;
    Color flickerColor1 = Color.yellow;
    Color flickerColor2 = Color.green;

    public IEnumerator PackageDelivered(SpriteRenderer otherSpriteRenderer)
    {
        if (otherSpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on 'other' GameObject!");
            yield break;
        }

        Color originalColor = otherSpriteRenderer.color;

        float flickerTimer = 0f;

        while (flickerTimer < flickerDuration)
        {
            otherSpriteRenderer.color = flickerColor1;
            yield return new WaitForSeconds(0.1f);
            otherSpriteRenderer.color = flickerColor2;
            yield return new WaitForSeconds(0.1f);

            flickerTimer += 0.2f;
        }

        otherSpriteRenderer.color = originalColor;
    }
}
