using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }

    public Text remainingLives;
    public Text currentPoints;

    public int level = 1;
    public int score = 0;
    public int lives = 3;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded; // it will get called when scene is loaded
    }

    private void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        LoadLevel(1);
    }
    private void LoadLevel(int level) 
    {
        this.level = level;

        if(level > 3 ){
            SceneManager.LoadScene("WinScreen");
                } else {
            SceneManager.LoadScene("Level" + level);
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
    }
    
    public void Hit(Brick brick)
    {
        this.score += brick.points;

        if (Cleared()) {
            LoadLevel(this.level + 1);
        }
    }
    public void Miss()
    {
        this.lives--;
        this.remainingLives.text = "Lives: " + lives.ToString();
        this.currentPoints.text = "Point: " + score.ToString();

        if(this.lives > 0) {
            ResetLevel();
        } else {
            GameOver();
        }
    }
    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();

    }
    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");

    }
    private bool Cleared() // check with loop if there are any bricks left, if not, move to the next level
    {
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable) {
                return false;
            }
        }

        return true;
    }
}
