using System.Collections;
using UnityEngine;

public class DriverController : MonoBehaviour
{
    private ISpeedAdjuster _speedAdjuster;
    private ICollisionHandler _collisionHandler;
    private ITriggerHandler _triggerHandler;
    private IHealthSystem _healthSystem;

    bool isTakingDamage = false;

    private void Start()
    {
        _speedAdjuster = DependencyManager.Instance.SpeedAdjuster;
        _collisionHandler = DependencyManager.Instance.CollisionHandler;
        _triggerHandler = DependencyManager.Instance.TriggerHandler;
        _healthSystem = DependencyManager.Instance.HealthSystem;
    }

    void Update()
    {
        if(GameManager.Instance.CurrentState.Equals(GameState.Gameplay))
        {
            transform.Rotate(0, 0, _speedAdjuster.GetSteerAmount());
            transform.Translate(0, _speedAdjuster.GetMoveAmount(), 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_collisionHandler.IsDecorationCollision(collision))
        {
            StartCoroutine(_speedAdjuster.AdjustSpeedOverTime(0.7f, 5f));

            if (!isTakingDamage)
            {
                isTakingDamage = true;
                _healthSystem.TakeDamage(20f);
                StartCoroutine(CarVulnerabilityDuration());
            }
        }
    }

    private IEnumerator CarVulnerabilityDuration()
    {
        yield return new WaitForSeconds(1f);
        //Add Animation that point on vulnerability for 1 sec
        isTakingDamage = false;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(_triggerHandler.IsBoostTriggered(collider))
        {
            StartCoroutine(_speedAdjuster.AdjustSpeedOverTime(0.7f, 30f));
        }
    }
}
