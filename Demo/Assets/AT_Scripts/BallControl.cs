using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour
{
    public Vector3 startPosition;

	// Use this for initialization
	void Start()
    {
        transform.localPosition = startPosition;
	}
	

	// Update is called once per frame
	void Update()
    {
	
	}


    void OnCollisionEnter2D(Collision2D coll)
    {
        switch(coll.gameObject.tag)
        {
            case "Reset":
                LauncherControl.launchable = true;
                GameObject.FindGameObjectWithTag("LaunchBlock").GetComponent<EdgeCollider2D>().enabled = false;
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
                transform.localPosition = startPosition;
                Debug.Log("Gutter");
                break;
            case "LaunchBlock":
                other.GetComponent<EdgeCollider2D>().enabled = true;
                break;

        }
    }


}
