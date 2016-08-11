using UnityEngine;
using System.Collections;

public class RightPocket : PocketControl {

    public override void CaughtBall()
    {
        if (!holding)
        {
            caughtTime = Time.time;
            holding = true;
            Debug.Log("Right Pocket CaughtBall()");

            // TODO Right Pocket Game Mechanics Go Here
        }
    }
}
