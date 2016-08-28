using UnityEngine;
using System.Collections;

public class LauncherControl : MonoBehaviour
{
    public static bool launchable = true;

    public float forcePerSec;
    public float forceCap;
    public Rigidbody2D ball;

    private float currentForce = 0f;
    private bool touchedLastFrame = false;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((InputControl.leftTouched||InputControl.rightTouched) && (currentForce < forceCap))
        {
            // Increase launch force while charging
            touchedLastFrame = true;
            currentForce += forcePerSec * Time.deltaTime;
            //Debug.Log(currentForce);
        }
        else if((!InputControl.leftTouched && !InputControl.rightTouched) && touchedLastFrame)
        {
            // Apply launch force to Ball
            if (launchable)
            {
                ball.AddForce(Vector2.up * currentForce);
                launchable = false;
            }

            currentForce = 0f;
            touchedLastFrame = false;
        }
	}





}
