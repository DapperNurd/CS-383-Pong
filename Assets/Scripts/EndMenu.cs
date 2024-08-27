using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    GameObject endMenu;

    TMP_Text endTitle;

    // Start is called before the first frame update
    void Start()
    {
        endMenu = transform.Find("End Menu").gameObject;
        endTitle = endMenu.transform.Find("Title").GetComponent<TMP_Text>();

        if (endMenu.activeSelf) DisableMenu();
    }
    
    public void EnableMenu() {
        endMenu.SetActive(true);
    }

    public void DisableMenu() {
        endMenu.SetActive(false);
    }

    public void SetEndTitle(string title) {
        endTitle.text = title;
    }

    public void ReloadCurrentScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
