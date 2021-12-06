using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Meat : Ingredient {
    private float cook;
    private int temp1;
    private int idealCook;

    private int tenderise;
    private int idealTenderise;

    private float salt;
    private int saltStage;
    private int idealSalt;

    private bool stepIsComplete;

    public float cooking {
        get { return cook; }
        set { cook = value; }
    }

    public int TempSide1
    {
        get { return temp1; }
        set { temp1 = value; }
    }

    public int IdealCook
    {
        get { return idealCook; }
        set { idealCook = value; }
    }


    public int tenderiseStage {
        get { return tenderise; }
        set { tenderise = value; }
    }

    public int IdealTenderisation
    {
        get { return idealTenderise; }
        set { idealTenderise = value; }
    }

    public int SaltAmount
    {
        get { return saltStage; }
        set { saltStage = value; }
    }

    public int IdealSalt
    {
        get { return idealSalt; }
        set { idealSalt = value; }
    }

    // Update is called once per frame
    public override void Update() {
        temp1 = (int)cook;
        tenderiseStage = (int)tenderise;
        saltStage = (int)salt;

        if (ingredientSteps.Count != 0) {
            
            nextStep = ingredientSteps[stepInt];
        }
    }

    public override void Generate() {
        stepInt = 0;
        cook = 0;

        idealCookPos = new Vector3(1.106f, 0.896f, -2.039f);
        idealPosition = new Vector3(-0.441f, 0.8f, -2.4f);

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));

        idealTenderise = Random.Range(1, 6);
        idealSalt = Random.Range(1, 6);
        idealCook = Random.Range(0, 10);

        ingredientSteps.Add("Tenderise");
        ingredientSteps.Add("Salt");
        ingredientSteps.Add("Cook");
        ingredientSteps.Add("Complete");
    }

}
