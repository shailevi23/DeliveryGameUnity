using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour, ITimer
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    public object TimerObj { get; private set; }

    private ILevelManager _levelManager;
    private float timer = 0f;

    void Awake()
    {
        TimerObj = timerText;
    }

    void Start()
    {
        //Dependecies
        _levelManager = DependencyManager.Instance.LevelManager;

        //Events
        GameManager.Instance.GameStateChanged += OnGameStateChanged;
    }


    public void Run()
    {
        if (GameManager.Instance.CurrentState.Equals(GameState.Gameplay))
        {
            if (_levelManager.GetCurrentLevelPackageDelivered() < _levelManager.GetCurrentLevelPackagesNumber())
            {
                timer += Time.deltaTime;
                timerText.text = timer.ToString("F2") + " | " + _levelManager.GetCurrentLevelPackageDelivered();
            }
        }
    }

    private void OnGameStateChanged()
    {
        if (GameManager.Instance.CurrentState == GameState.LevelIntro)
        {
            timer = 0;
            timerText.text = "0.00 | 0";
        }

        if(GameManager.Instance.CurrentState == GameState.LevelEndedSuccesfully)
        {
            timerText.text = timer.ToString("F2") + " | " + _levelManager.GetCurrentLevelPackageDelivered();
        }
    }


    private void OnDestroy()
    {
        GameManager.Instance.GameStateChanged -= OnGameStateChanged;
    }
}
