using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Restartable : MonoBehaviour, IRestartable
{
    [SerializeField] private GameObject driver;
    [SerializeField] private Button[] restartButtons;

    private ILevelManager _levelManager;
    private IHealthSystem _healthSystem;

    void Start()
    {
        _levelManager = DependencyManager.Instance.LevelManager;
        _healthSystem = DependencyManager.Instance.HealthSystem;

        foreach (Button button in restartButtons)
        {
            button.onClick.AddListener(OnRestartGameClicked);
        }
    }

    public void Restart()
    {
        _levelManager.ResetCurrentLevel();
        _healthSystem.HealToMaxHealth();
        driver.transform.rotation = _levelManager.GetCurrentLevelCarSpawnRotation();
        driver.transform.position = _levelManager.GetCurrentLevelCarSpawnPosition();
    }

    private void OnRestartGameClicked()
    {
        if(GameManager.Instance.CurrentState != GameState.Settings)
        {
            restartButtons[0].onClick.RemoveListener(OnRestartGameClicked);
            StartCoroutine(EnableRestartButtonAfterDelay());
            Restart();
        }
    }

    private IEnumerator EnableRestartButtonAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        restartButtons[0].onClick.AddListener(OnRestartGameClicked);
    }
}
