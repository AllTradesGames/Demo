using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour
{

	// Use this for initialization
	void Start()
    {
	
	}
	

	// Update is called once per frame
	void Update()
    {
	
	}


    void OnCollisionEnter2D(Collision2D coll)
    {
        switch(coll.gameObject.tag)
        {
            case "test":
                Debug.Log("Test");
                break;

        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "test":
                Debug.Log("Test");
                break;

        }
    }


}
