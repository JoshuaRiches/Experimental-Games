using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatControl : MonoBehaviour
{
    [SerializeField]
    private Slider heatSlider; // the value of the slider (1-100
    [SerializeField]
    private int heatPerFlick; // the heat that should be added per flick exposed to the editor
    private float heatToAdd; // a float to add the amount of remaining heat to add
    private float addTimer; // a timer between each time you add 1 heat
    private float currentHeat; // to track the current heat
    [SerializeField]
    private float timePerCool; // how long it takes for the slider to cool
    private float coolingTimer; // a timer until we next need to cool
    [SerializeField]
    private float fluidness; // a float that makes our slider more fluid (higher number = more fluid)
    [SerializeField]
    private float secondsToHeat; // exposed to the editor - how long it takes to heat after we flick
    private float floatTimeForHeat; // a float used for a real version of the above ^^ 
    // Start is called before the first frame update
    void Start()
    {
        // set initial values
        heatSlider.value = 20f;
        heatSlider.maxValue = 100;
        addTimer = 0;
        coolingTimer = timePerCool / fluidness;
        currentHeat = 20f;
        floatTimeForHeat = secondsToHeat / 40;
    }

    // Update is called once per frame
    void Update() {
        // lower both our timers
        coolingTimer -= Time.deltaTime;
        addTimer -= Time.deltaTime;
        // if the correct key is pressed, add to our heat to add float
        if (Input.GetKeyDown(KeyCode.Q)) {
            heatToAdd = heatPerFlick;
        }
        // if we have remaining heat to add, and the timer is at 0
        if (heatToAdd > 0 && addTimer <= 0) {
            // add to current heat
            currentHeat += 1/ fluidness;
            // take away from remaining heat to add
            heatToAdd -= 1/fluidness;
            // reset the timer
            addTimer = floatTimeForHeat / fluidness;
        }
        if (coolingTimer <= 0) {
            // lower the heat
            currentHeat -= 1/fluidness;
            // reset the timer
            coolingTimer = timePerCool/fluidness;
        }
        // set the slider to the correct value
        heatSlider.value = currentHeat;

        if (currentHeat > 100 || currentHeat < 0) {
            // failure mechanic
        }

    }

}
