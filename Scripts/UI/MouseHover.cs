using UnityEngine;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour
{
    public Button buttonObj;
    public AudioSource myAudioSource;
    public AudioClip hoverInSound;
    public AudioClip hoverOutSound;
    public AudioClip clickSound;

    public void TurnRed()
    {
        ColorBlock colors = buttonObj.colors;
        colors.normalColor = Color.red;
        colors.highlightedColor = new Color32(255, 100, 100, 255);
        buttonObj.colors = colors;
    }

    public void TurnWhite()
    {
        ColorBlock colors = buttonObj.colors;
        colors.normalColor = Color.white;
        colors.highlightedColor = new Color32(225, 225, 225, 255);
        buttonObj.colors = colors;
    }

    public void HoverInSound()
    {
        TurnRed();
        myAudioSource.PlayOneShot(hoverInSound);
    }

    public void HoverOutSound()
    {
        TurnWhite();
        myAudioSource.PlayOneShot(hoverOutSound);
    }

    public void ClickSound()
    {
        TurnRed();
        myAudioSource.PlayOneShot(clickSound);
    }
}
