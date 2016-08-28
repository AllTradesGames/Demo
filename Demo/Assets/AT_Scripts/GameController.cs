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


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Ball");
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        ActivateBallSaver();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddScore(int inputScore)
    {
        score += inputScore;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            Debug.Log("HighScore set: " + highScore);
        }

        Debug.Log("Score: "+score);
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
            Debug.Log("Gutter, lives: " + lives);

            if (lives > 0)
            {
                player.transform.localPosition = ballStartPosition;
                // TODO Call to update GUIManager's lives
                ActivateBallSaver();
            }
            else
            {
                Debug.Log("Game Over");
            }
        }           
    }


    public static void ActivateBallSaver()
    {
        saverStartTime = Time.time;
        saver = true;
        // TODO update saver in GUIManager
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().StartCoroutine("SaverTimer");
    }


    IEnumerator SaverTimer()
    {
        while((Time.time - saverStartTime) < ballSaverDuration)
        {
            yield return new WaitForSeconds(ballSaverDuration - (Time.time - saverStartTime));
        }
        saver = false;
        // TODO update saver in GUIManager
        Debug.Log("Ball Saver Ended");
    }


}
