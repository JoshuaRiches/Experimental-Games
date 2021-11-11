using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer : MonoBehaviour
{
    private float timer;
    private bool timeUp;

    [SerializeField]
    private GameplayManager gameManager;

    void Initialize(float a_timer)
    {
        timer = a_timer;
    }

    // Update is called once per frame
    void Update()
    {
        // lower the timers
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timeUp = true;
            Finish();
        }
    }
    
    bool Finish()
    {
        return true;
    }
}
