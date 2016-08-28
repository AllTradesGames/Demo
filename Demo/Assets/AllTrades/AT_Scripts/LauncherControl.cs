using UnityEngine;
using System.Collections;

public class LauncherControl : MonoBehaviour
{
    public static bool launchable = true;

    public float forcePerSec;
    public float forceCap;
    public Rigidbody2D ball;

    private float currentForce = 0f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Submit") && (currentForce < forceCap))
        {
            // Increase launch force while charging
            currentForce += forcePerSec * Time.deltaTime;
            //Debug.Log(currentForce);
        }
        else if(Input.GetButtonUp("Submit"))
        {
            // Apply launch force to Ball
            if (launchable)
            {
                ball.AddForce(Vector2.up * currentForce);
                launchable = false;
            }

            currentForce = 0f;
        }
	}





}
