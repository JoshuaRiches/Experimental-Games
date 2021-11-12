using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vegetable : Ingredient
{

    [SerializeField]
    private bool hasLeaves;
    [SerializeField]
    private bool needsChopping;

    private void Awake() {

        // Generate();

    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    public override void Update() {
        if (ingredientSteps.Count != 0) {
            Debug.Log("int is: " + stepInt);
            Debug.Log("Length is: " + ingredientSteps.Count);
            nextStep = ingredientSteps[stepInt];
        }
    }

    public override void Generate() {
        stepInt = 0;

        if (hasLeaves) {
            ingredientSteps.Add("PeelLeaves");
            Debug.Log("Adds");
        }
        if (needsChopping) {
            ingredientSteps.Add("Chop");
            Debug.Log("Adds");
        }
        ingredientSteps.Add("Complete");
        // Debug.Log("Steps: " + ingredientSteps);
    }

}
