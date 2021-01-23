using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private GameObject _fadeInPanel;
    [SerializeField] private Button _exitToMainMenuButton;
    [SerializeField] private string _mainMenuSceneName;

    private void OnEnable()
    {
        _exitToMainMenuButton.onClick.AddListener(LoadMainMenuScene);
    }

    private void OnDisable()
    {
        _exitToMainMenuButton.onClick.RemoveListener(LoadMainMenuScene);
    }

    private void LoadMainMenuScene()
    {
        StartCoroutine(FadeIn(_mainMenuSceneName));
    }

    private IEnumerator FadeIn(string sceneName)
    {
        Time.timeScale = 1;
        _fadeInPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }
}
