using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour, ISettingsUI
{
    //Main Settings
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button settingsButton;

    //InnerSettings
    [SerializeField] private GameObject changeCarPanel;
    [SerializeField] private Button changeCarButton;

    public IChangeCar _changeCar { get => DependencyManager.Instance.ChangeCar; }

    private void Start()
    {
        AddButtonListeners();
    }

    private void AddButtonListeners()
    {
        settingsButton.onClick.AddListener(ToggleSettings);
        changeCarButton.onClick.AddListener(ToggleChangeCar);
    }

    // -------------------------------------- Button Click Actions --------------------------------------
    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        UpdateGameState();
    }

    public void ToggleChangeCar()
    {
        changeCarPanel.SetActive(!changeCarPanel.activeSelf);
    }

    private void UpdateGameState()
    {
        if (settingsPanel.activeInHierarchy)
        {
            GameManager.Instance.SetGameState(GameState.Settings);
        }
        else
        { 
            GameManager.Instance.SetGameState(GameManager.Instance.PreviousState);
        }
    }
}
