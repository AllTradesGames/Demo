using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static int Score
    {
        get
        {
            return score;
        }
    }
    public static int HighScore
    {
        get
        {
            return highScore;
        }
    }
    public static int Lives
    {
        get
        {
            return lives;
        }
    }

    private static int score = 0;
    private static int highScore = 0;
    private static int lives = 3;

    private static GameObject player;
    private static Vector3 ballStartPosition = new Vector3(1.81f, 0f, 0f);

    private static bool saver = false;
    private static float ballSaverDuration = 30f;
    private static float saverStartTime = 0f;

    private static SpriteMessageControl message;
    private static SpriteMessageControl scoreMessage;
    private static SpriteMessageControl highScoreMessage;



    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Ball");
        message = GameObject.FindGameObjectWithTag("Message").GetComponent<SpriteMessageControl>();
        scoreMessage = GameObject.FindGameObjectWithTag("ScoreMessage").GetComponent<SpriteMessageControl>();
        highScoreMessage = GameObject.FindGameObjectWithTag("HighScoreMessage").GetComponent<SpriteMessageControl>();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            highScore = 0;
        }

        highScoreMessage.SetMessage("High Score: "+highScore);
        scoreMessage.SetMessage("Your Score: " + score);
        message.SetMessage("Press and Hold to Launch!");

    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("timescale: "+Time.timeScale);
    }

    public static void AddScore(int inputScore)
    {
        score += inputScore;
        scoreMessage.SetMessage("Your Score: " + score);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreMessage.SetMessage("High Score: " + highScore);
        }
    }


    public static void Gutter()
    {     
        if(saver)
        {
            player.transform.localPosition = ballStartPosition;
            // TODO visual/sound effects for ball saved
        }   
        else
        {
            lives--;
            message.SetMessage("Oh No! Lives: " + lives);

            if (lives > 0)
            {
                player.transform.localPosition = ballStartPosition;
                // TODO Call to update GUIManager's lives
            }
            else
            {
                message.SetMessage("Game Over");
            }
        }           
    }


    public static void ActivateBallSaver()
    {
        saverStartTime = Time.time;
        saver = true;
        // TODO update saver in GUIManager
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().StartCoroutine("SaverTimer");
        message.SetMessage("Ball Saver Activated");
    }


    IEnumerator SaverTimer()
    {
        while((Time.time - saverStartTime) < ballSaverDuration)
        {
            yield return new WaitForSeconds(ballSaverDuration - (Time.time - saverStartTime));
        }
        saver = false;
        // TODO update saver in GUIManager
        message.SetMessage("Ball Saver Ended");
    }


}
