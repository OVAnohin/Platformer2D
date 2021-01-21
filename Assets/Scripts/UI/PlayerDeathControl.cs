using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeathControl : MonoBehaviour
{
  [SerializeField] private Player _player = default;
  [SerializeField] private GameObject _screenOfDeathPanel = default;
  [SerializeField] private GameObject _fadeInPanel = default;
  [SerializeField] private Button _restartGameButton = default;
  [SerializeField] private Button _exitToMainMenuButton = default;
  [SerializeField] private string _mainMenuSceneName = default;

  private void OnEnable()
  {
    _player.PlayerDied += OnPlayerDied;
    _restartGameButton.onClick.AddListener(RestartLevel);
    _exitToMainMenuButton.onClick.AddListener(LoadMainMenuScene);
  }

  private void OnDisable()
  {
    _player.PlayerDied -= OnPlayerDied;
    _restartGameButton.onClick.RemoveListener(RestartLevel);
    _exitToMainMenuButton.onClick.RemoveListener(LoadMainMenuScene);
  }

  private void RestartLevel()
  {
    StartCoroutine(FadeIn(SceneManager.GetActiveScene().name));
  }

  private void LoadMainMenuScene()
  {
    StartCoroutine(FadeIn(_mainMenuSceneName));
  }

  private void OnPlayerDied()
  {
    _screenOfDeathPanel.SetActive(true);
  }

  private IEnumerator FadeIn(string sceneName)
  {
    _fadeInPanel.SetActive(true);
    yield return new WaitForSeconds(1f);

    SceneManager.LoadScene(sceneName);
  }
}
