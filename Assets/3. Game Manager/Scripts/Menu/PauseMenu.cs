using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Enter/exit pause state via "escape" - ok
    // Display Pause Menu when Paused - ok
    // Exit Pause State via "resume" or "escape" 1/2 ok
    // Pause simulation on Pause State
    // Set the cursor to use "Pointer"

    void Start()
    {
        Debug.Log("[PauseMenu] Start");
    }

    void Update() 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
		    GameRunning();
		}
	}

    void GameRunning()
    {
        GameManager.Instance.RunningGame();
    }
}
