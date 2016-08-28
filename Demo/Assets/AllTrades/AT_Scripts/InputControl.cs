using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour 
{
    [Range(-1f, 1f)] public static float[] axis = {0f};
    public static float[] gravity = {4.0f};
    public static bool[] snap = {true};
    public static float[] dead = {0.001f};
    public static float[] sensitivity = {10.0f};

    [Range(-1, 1)] public static int touchWeight = 0;

    public SpriteRenderer leftMask;
    public SpriteRenderer rightMask;
    public float inputFlashDuration;

    private float tempVal;
    private Color leftOrigColor;
    private Color rightOrigColor;
   

	// Use this for initialization
	void Start () 
    {
        leftOrigColor = leftMask.color;
        rightOrigColor = rightMask.color;
	}
	
	// Update is called once per frame
	void Update () 
    {
//        if (Input.touches.Length < 0)
//        {
//            Debug.Log(Input.touches[0].position);
//        }

        touchWeight = 0;
        leftMask.color = new Color(leftOrigColor.r, leftOrigColor.g, leftOrigColor.b, 0f);
        rightMask.color = new Color(rightOrigColor.r, rightOrigColor.g, rightOrigColor.b, 0f);

        foreach (Touch touch in Input.touches)
        {
            // The y position is checked so the player isn't moved while pressing pause
            if (touch.position.y > Screen.height / 10f)
            {
                if (touch.position.x <= Screen.width / 2f)
                {
                    touchWeight--;
                    if (Time.time < inputFlashDuration)
                    {
                        leftMask.color = leftOrigColor;
                    }
                }
                else
                {
                    touchWeight++;
                    if (Time.time < inputFlashDuration)
                    {
                        rightMask.color = rightOrigColor;
                    }
                }
            }
        }

        // For mouse
        if (Input.GetMouseButton(0))
        {
            // The y position is checked so the player isn't moved while pressing pause
            if (Input.mousePosition.y > Screen.height / 10f)
            {
                if (Input.mousePosition.x <= Screen.width / 2f)
                {
                    touchWeight = -1;
                    if (Time.time < inputFlashDuration)
                    {
                        leftMask.color = leftOrigColor;
                    }
                }
                else
                {
                    touchWeight = 1;
                    if (Time.time < inputFlashDuration)
                    {
                        rightMask.color = rightOrigColor;
                    }
                }
            }
        }
        else
        {
            touchWeight = 0;
        }
        
        for (int ii = 0; ii < axis.Length; ii++)
        {
            if ((touchWeight == 0) && (axis[ii]!=0f))
            {
                tempVal = axis[ii];
                axis[ii] = ((axis[ii] < 0f) ? axis[ii] + gravity[ii] * Time.deltaTime : axis[ii] - gravity[ii] * Time.deltaTime);
                if ((Mathf.Floor(tempVal) + Mathf.Floor(axis[ii]))==-1f)
                {
                    axis[ii] = 0f;
                }
            }
            else if (snap[ii] && (((float)touchWeight + ((axis[ii] < 0f) ? Mathf.Floor(axis[ii]) : Mathf.Ceil(axis[ii]))) == 0f))
            {
                axis[ii] = 0f;
            }
            else
            {
                axis[ii] += sensitivity[ii] * Time.deltaTime * (float)touchWeight;

                if (axis[ii] > 1f)
                {
                    axis[ii] = 1f;
                }
                else if (axis[ii] < -1f)
                {
                    axis[ii] = -1f;
                }
                else if (Mathf.Abs(axis[ii]) < dead[ii])
                {
                    axis[ii] = 0f;
                }
            }
        }

	}


}
