using UnityEngine;
using System.Collections;

public class ScoredObject : MonoBehaviour, IScoreable
{
    // This script is used to implement the IScoreable interface on objects that don't otherwise have scripts attached.

    public int scoreValue = 0;

    public void AddScore()
    {
        GameController.AddScore(scoreValue);
    }
	
}
