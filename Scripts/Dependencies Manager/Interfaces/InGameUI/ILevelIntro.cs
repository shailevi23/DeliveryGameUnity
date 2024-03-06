using TMPro;
using UnityEngine;

public interface ILevelIntro
{
    object LevelIntroText { get; }
    void SetActive();
    void StartCountdown();
    void StopCountdown();
}
