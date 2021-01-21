using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameMenu : MonoBehaviour
{
  [SerializeField] private Button _startButton = default;
  [SerializeField] private Button _exitButton = default;
  [SerializeField] private string _firstLevel = default;
  [SerializeField] private GameObject _fadeInPanel = default;

  public void OpenAboutPanel(GameObject panel)
  {
    panel.SetActive(true);
  }

  public void CloseAboutPanel(GameObject panel)
  {
    panel.SetActive(false);
  }

  private void OnEnable()
  {
    _startButton.onClick.AddListener(UseButtonClick);
    _exitButton.onClick.AddListener(UseExitClick);
  }

  private void OnDisable()
  {
    _startButton.onClick.RemoveListener(UseButtonClick);
    _exitButton.onClick.RemoveListener(UseExitClick);
  }

  private void UseExitClick()
  {
    Application.Quit();
  }

  private void UseButtonClick()
  {
    StartCoroutine(FadeIn());
  }

  private IEnumerator FadeIn()
  {
    _fadeInPanel.SetActive(true);
    yield return new WaitForSeconds(1f);

    SceneManager.LoadScene(_firstLevel);
  }
}
