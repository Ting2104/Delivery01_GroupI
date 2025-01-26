using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject player;
    float end = 0f;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(end);
        if (Input.GetKeyDown("escape"))
        {
            QuitGame();
            Debug.Log("salir");
        }
        if (Input.GetKeyDown("return"))
        {
            LoadGame();
            Debug.Log("cargar");
            end = 0f;
        }
        if(player.transform.position.y < -4f && end == 0f)
        {
            EndGame();
            Debug.Log("fin");
            end = 1f;
        }
    }

    public void LoadGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void EndGame()
    {
        //SceneManager.LoadScene(2, LoadSceneMode.Single);
        Time.timeScale = 0.0f;
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}
