﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject HUDPanel;
    [SerializeField] private GameObject endGameMenu;

    private Image fillBar;
    private Text score;

    private void Awake()
    {
        if (mainCanvas == null)
        {
            mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        if (mainMenuPanel == null)
        {
            mainMenuPanel = GameObject.Find("MainMenu");
        }

        if (HUDPanel == null)
        {
            HUDPanel = GameObject.Find("HUD");
        }

        fillBar = HUDPanel.transform.Find("HealthUI").Find("FillBar").GetComponent<Image>();
        score = HUDPanel.transform.Find("Score").Find("Text").GetComponent<Text>();
        GameManager.StageChanged += OnStageChanged;
    }

    public void OnPlayerDamageTaken(PlayerStats playerStats)
    {
        UpdateHpBar(playerStats);
    }

    public void UpdateHpBar(PlayerStats playerStats)
    {
        fillBar.fillAmount = playerStats.CurrentHp / playerStats.MaxHp;
    }

    void OnStageChanged(GameStage stage, bool isGamePaused)
    {
        switch (stage)
        {
            case GameStage.MainMenu:
                PrepareMainMenu();
                break;
            case GameStage.Game:
                PrepareHUD();
                break;
            case GameStage.EndGameMenu:
                PrepareEndGameMenu();
                break;
        }
    }

    void PrepareMainMenu()
    {
        mainMenuPanel.SetActive(true);
        HUDPanel.SetActive(false);
        endGameMenu.SetActive(false);
    }

    void PrepareHUD()
    {
        mainMenuPanel.SetActive(false);
        HUDPanel.SetActive(true);
        endGameMenu.SetActive(false);
    }

    void PrepareEndGameMenu()
    {
        mainMenuPanel.SetActive(false);
        HUDPanel.SetActive(false);
        endGameMenu.SetActive(true);
    }
}