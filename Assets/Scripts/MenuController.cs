using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            QuitGame();
        }
        if (Input.GetKeyDown("return"))
        {
            LoadGame();
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        Debug.Log("a jugar");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("salir");
    }
}
