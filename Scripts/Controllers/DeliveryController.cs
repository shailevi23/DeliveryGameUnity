using UnityEngine;

public class DeliveryController : MonoBehaviour
{
    private ITriggerHandler _triggerHandler;
    private IDeliveryAnimation _deliveryAnimation;
    private ILevelManager _levelManager;
    private IInGameUI _inGameUI;

    bool hasPackage = false;

    private void Start()
    {
        _triggerHandler = DependencyManager.Instance.TriggerHandler;
        _deliveryAnimation = DependencyManager.Instance.DeliveryAnimation;
        _levelManager = DependencyManager.Instance.LevelManager;
        _inGameUI = DependencyManager.Instance.InGameUI;

        GameManager.Instance.GameStateChanged += OnGameStateChanged;
    }

    private void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (_triggerHandler.IsPackageTriggered(collider))
        {
            if (!hasPackage)
            {
                hasPackage = true;
                _inGameUI.SetDeliveryStatus("Package picked up");
                _levelManager.ReturnLevelPrefab(collider.gameObject);
            }
            else
            {
                _inGameUI.SetDeliveryStatus("Already has a package!");
            }
        }
        else if (_triggerHandler.IsCustomerTriggered(collider))
        {
            if (hasPackage)
            {
                hasPackage = false;
                _inGameUI.SetDeliveryStatus("Package delivered!");
                _levelManager.IncreaseCurrentLevelPackageDelivered();
                StartCoroutine(_deliveryAnimation.PackageDelivered(collider.GetComponent<SpriteRenderer>()));
            }
            else
            {
                _inGameUI.SetDeliveryStatus("You don't have a package!");
            }
        }
    }

    private void OnGameStateChanged()
    {
        if (GameManager.Instance.CurrentState.Equals(GameState.LevelIntro))
        {
            hasPackage = false;
        }
    }
}
