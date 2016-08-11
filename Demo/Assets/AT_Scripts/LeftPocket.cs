using UnityEngine;
using System.Collections;

public class LeftPocket : PocketControl
{

    public override void CaughtBall()
    {
        if (!holding)
        {
            caughtTime = Time.time;
            holding = true;
            Debug.Log("Left Pocket CaughtBall()");

            // TODO Left Pocket Game Mechanics Go Here
        }
    }
}