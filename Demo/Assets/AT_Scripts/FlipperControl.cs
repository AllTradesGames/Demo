using UnityEngine;
using System.Collections;

public class FlipperControl : MonoBehaviour
{
    public bool left = false;
    public float rotateSpeed;
    public float maxAngle = 35f;
    public float minAngle = 122f;
    public Vector2 centerOfMass;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = centerOfMass;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {        
        if (left)
        {
            if (Input.GetKey("left"))
            {
                if ((rb.rotation % 360) < maxAngle)
                {
                    if ((rb.rotation + rotateSpeed * Time.fixedDeltaTime) > maxAngle)
                    {
                        rb.MoveRotation(maxAngle);
                    }
                    else
                    {
                        rb.MoveRotation(rb.rotation + rotateSpeed * Time.fixedDeltaTime);
                    }
                }
            }
            else if ((rb.rotation % 360) > minAngle)
            {
                if ((rb.rotation - rotateSpeed * Time.fixedDeltaTime) < minAngle)
                {
                    rb.MoveRotation(minAngle);
                }
                else
                {
                    rb.MoveRotation(rb.rotation - rotateSpeed * Time.fixedDeltaTime);
                }
            }
        }
        else
        {
            if(Input.GetKey("right"))
            {
                if ((rb.rotation % 360) > maxAngle)
                {
                    if ((rb.rotation - rotateSpeed * Time.fixedDeltaTime) < maxAngle)
                    {
                        rb.MoveRotation(maxAngle);
                    }
                    else
                    {
                        rb.MoveRotation(rb.rotation - rotateSpeed * Time.fixedDeltaTime);
                    }
                }
            }
            else if((rb.rotation % 360) < minAngle)
            {
                if ((rb.rotation + rotateSpeed * Time.fixedDeltaTime) > minAngle)
                {
                    rb.MoveRotation(minAngle);
                }
                else
                {
                    rb.MoveRotation(rb.rotation + rotateSpeed * Time.fixedDeltaTime);
                }
            }
        }        
	}


}
