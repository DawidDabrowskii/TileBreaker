using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Awake()
    {
        Destroy(GameObject.Find("Game Manager"));
    }
    public void PlayAgain()
        {
        SceneManager.LoadScene(0);
        }
}
