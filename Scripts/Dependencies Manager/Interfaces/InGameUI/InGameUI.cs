using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour, IInGameUI
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private SpriteRenderer carSprite;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI deliveryStatusText;

    private IHealthSystem _healthSystem;
    private ISettingsUI _settingsUI;
    private ITimer _timer;

    void Awake()
    {
        //Dependecies
        _settingsUI = DependencyManager.Instance.SettingsUI;
        _healthSystem = DependencyManager.Instance.HealthSystem;
        _timer = DependencyManager.Instance.Timer;

        //Events
        carSprite.sprite = _settingsUI._changeCar.CurrentCarIamge;
        _settingsUI._changeCar.CarImageChanged += UpdateCarSprite;

        _healthSystem.OnHealthChanged += UpdateHealthBar;
        GameManager.Instance.GameStateChanged += OnGameStateChanged;
    }

    void Update()
    {
        _timer.Run();
    }

    private void UpdateHealthBar(float health)
    {
        slider.value = health;
    }

    private void UpdateCarSprite(Sprite newSprite)
    {
        carSprite.sprite = newSprite;
    }

    private void OnGameStateChanged()
    {
        if (GameManager.Instance.CurrentState == GameState.LevelEndedWithGameOver)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _settingsUI._changeCar.CarImageChanged -= UpdateCarSprite;
        GameManager.Instance.GameStateChanged -= OnGameStateChanged;
    }

    public void SetDeliveryStatus(object deliveryStatus)
    {
        if (deliveryStatus is string updateDeliveryStatusText)
        {
            StartCoroutine(SetDeliveryStatusCoroutine(updateDeliveryStatusText));
        }
        else
        {
            Debug.LogError("Invalid parameters for SetDeliveryStatusCoroutine");
        }
    }

    private IEnumerator SetDeliveryStatusCoroutine(string updateDeliveryStatusText)
    {
        deliveryStatusText.color = Color.red;
        deliveryStatusText.text = updateDeliveryStatusText;
        yield return new WaitForSeconds(1f);
        deliveryStatusText.text = "";
    }
}
