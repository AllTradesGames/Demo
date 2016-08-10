using UnityEngine;
using System.Collections;

public class PocketControl : MonoBehaviour
{
    public float holdTime;
    public float cooldownTime;

    public float caughtTime = 0f;
    private float releaseTime = 0f;
    public bool holding = false;
    private AreaEffector2D eff;

	// Use this for initialization
	void Start ()
    {
        eff = GetComponent<AreaEffector2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // TODO Convert to Coroutines to improve performance
	    if(holding && (Time.time > (caughtTime + holdTime)))
        {
            holding = false;
            eff.enabled = false;
            releaseTime = Time.time;
        }
        else if(!eff.isActiveAndEnabled && (Time.time > (releaseTime + cooldownTime)))
        {
            eff.enabled = true;
        }
	}


    public virtual void CaughtBall()
    {
        caughtTime = Time.time;
        holding = true;
        Debug.Log("CaughtBall()");
    }


}
