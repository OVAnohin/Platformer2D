using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeathControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _screenOfDeathPanel;
    [SerializeField] private GameObject _fadeInPanel;
    [SerializeField] private Button _restartGameButton;
    [SerializeField] private Button _exitToMainMenuButton;
    [SerializeField] private string _mainMenuSceneName;

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
