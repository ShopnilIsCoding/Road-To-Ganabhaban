using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    public GameObject gameOverUI;
    public Button restartButton;
    public GameObject[] gameObjects;

    void Start()
    {
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("npckill");
        Debug.Log(npcs.Length);

        if (npcs.Length < 10)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameOverUI.SetActive(false);
    }
}
