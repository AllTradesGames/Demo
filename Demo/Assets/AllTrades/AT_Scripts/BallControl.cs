using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour
{

    private GameController gc;
    private bool blocked = false;

	// Use this for initialization
	void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	

	// Update is called once per frame
	void Update()
    {
	
	}


    void OnCollisionEnter2D(Collision2D coll)
    {

        if(coll.gameObject.GetComponent<IScoreable>()!=null)
        {
            coll.gameObject.GetComponent<IScoreable>().AddScore();
        }

        switch(coll.gameObject.tag)
        {
            case "Reset":
                LauncherControl.launchable = true;
                GameObject.FindGameObjectWithTag("LaunchBlock").GetComponent<EdgeCollider2D>().enabled = false;
                blocked = false;
                break;
            case "Pocket":
                coll.gameObject.GetComponent<PocketControl>().CaughtBall();
                break;

        }
    }


    void OnCollisionStay2D(Collision2D coll)
    {
        switch (coll.gameObject.tag)
        {            
            case "Pocket":
                coll.gameObject.GetComponent<PocketControl>().CaughtBall();
                break;
        }
    }


    void OnCollisionExit2D(Collision2D coll)
    {
        switch (coll.gameObject.tag)
        {

        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Gutter":
                GameController.Gutter();
                break;
            case "LaunchBlock":
                if (!blocked)
                {
                    other.GetComponent<EdgeCollider2D>().enabled = true;
                    GameController.ActivateBallSaver();
                    blocked = true;
                }
                break;

        }
    }


}
