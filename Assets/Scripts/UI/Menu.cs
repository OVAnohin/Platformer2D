using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void OpenMenu(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMenu(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }
}
