using System.Collections;

public interface ISpeedAdjuster
{
    float CurrentSteerSpeed { get; set; }
    float CurrentMoveSpeed { get; set; }
    float GetOriginalMoveSpeed();
    float GetSteerAmount();
    float GetMoveAmount();
    IEnumerator AdjustSpeedOverTime(float duration, float moveSpeed);
}