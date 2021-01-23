using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _exitToMainMenuButton;
    [SerializeField] private string _mainMenuSceneName;
    [SerializeField] private GameObject _fadeInPanel;

    private void OnEnable()
    {
        _exitToMainMenuButton.onClick.AddListener(LoadMainMenuScene);
    }

    private void OnDisable()
    {
        _exitToMainMenuButton.onClick.RemoveListener(LoadMainMenuScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            _panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void LoadMainMenuScene()
    {
        Time.timeScale = 1;
        StartCoroutine(FadeIn(_mainMenuSceneName));
    }

    private IEnumerator FadeIn(string sceneName)
    {
        _fadeInPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }
}
