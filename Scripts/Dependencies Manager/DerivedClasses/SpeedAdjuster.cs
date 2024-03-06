using System.Collections;
using UnityEngine;

public class SpeedAdjuster : MonoBehaviour, ISpeedAdjuster
{
    private readonly float originalMoveSpeed = 17f;

    private void Start()
    {
        CurrentMoveSpeed = originalMoveSpeed;
        CurrentSteerSpeed = 220f;
    }

    public float CurrentMoveSpeed { get; set; }
    public float CurrentSteerSpeed { get; set; }

    public float GetSteerAmount()
    {
        return -1 * Input.GetAxis("Horizontal") * CurrentSteerSpeed * Time.deltaTime;
    }

    public float GetMoveAmount()
    {
        return Input.GetAxis("Vertical") * CurrentMoveSpeed * Time.deltaTime;
    }

    public float GetOriginalMoveSpeed()
    {
        return originalMoveSpeed;
    }

    public IEnumerator AdjustSpeedOverTime(float duration, float moveSpeed)
    {
        CurrentMoveSpeed = moveSpeed; 
        yield return new WaitForSeconds(duration);

        CurrentMoveSpeed = originalMoveSpeed;
    }
}