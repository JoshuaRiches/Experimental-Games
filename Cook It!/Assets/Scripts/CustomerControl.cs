using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerControl : MonoBehaviour 
{
    [SerializeField]
    private bool customerActive; // whether the customer is active
    private int ingredientInt; // which ingredient of the order is the player on
    [SerializeField]
    private GameObject[] ingredients; // the list of possible ingredients
    [SerializeField]
    private GameObject bun;
    [SerializeField]
    private int maxNumberOfIngredientsPerOrder = 8; // maximum ingredients per order (+2 for buns on either side)

    [SerializeField]
    private GameObject panelForOrderPrefab;
    private GameObject canvas;

    private GameObject panelForOrder;
    // private Image imageToAdd;

    [SerializeField]
    private List<GameObject> items = new List<GameObject>();
    [SerializeField]
    private Ingredient ingredient;
    [SerializeField]
    private GameObject completed;

    private float impatience = 100; // value tbd
    private GameplayManager gManager;

    // Ingredient Sprites
    [SerializeField]
    private Sprite lettuceSprite;
    [SerializeField]
    private Sprite tomatoSprite;
    [SerializeField]
    private Sprite burgerSprite;
    [SerializeField]
    private Sprite onionSprite;
    [SerializeField]
    private Sprite bunSprite;
    [SerializeField]
    private Sprite knifeSprite;
    [SerializeField]
    private Sprite fireSprite;
    [SerializeField]
    private Sprite saltSprite;

    // for updating active ingredientSprites
    private List<GameObject> ingredientSprites = new List<GameObject>();
    private GameObject currentSprite;

    private GameObject StepSpriteObjectOne;
    private GameObject StepSpriteObjectTwo;
    private GameObject StepSpriteObjectThree;
    private Image StepSpriteOne;
    private Image StepSpriteTwo;
    private Image StepSpriteThree;
    private List<Image> stepSpriteList = new List<Image>();

    // getters and setters
    public Ingredient currentIngredient 
    {
        get { return ingredient; }
        set { ingredient = value; }
    }

    public bool isActiveCustomer
    {
        get { return customerActive; }
        set { customerActive = value; }
    }

    public int ingredientTracker 
    {
        get { return ingredientInt; }
        set { ingredientInt = value; }
    }

    private void Initialise() 
    {
        ingredientInt = 0;
        impatience = 500; //value tbd
        // panelForOrderPrefab.SetActive(false);
        canvas = new GameObject();
        canvas.AddComponent<Canvas>();
        canvas.name = "CustomerCanvas";
        canvas.transform.SetParent(this.gameObject.transform);
        canvas.transform.localPosition = new Vector3(-4.62f, -4.087f, -4.749f);

        StepSpriteObjectOne = new GameObject();
        StepSpriteOne = StepSpriteObjectOne.AddComponent<Image>();
        stepSpriteList.Add(StepSpriteOne);
        StepSpriteObjectTwo = new GameObject();
        StepSpriteTwo = StepSpriteObjectTwo.AddComponent<Image>();
        stepSpriteList.Add(StepSpriteTwo);
        StepSpriteObjectThree = new GameObject();
        StepSpriteThree = StepSpriteObjectThree.AddComponent<Image>();
        stepSpriteList.Add(StepSpriteThree);
    }

    void Start() 
    {
        customerActive = true;
        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameplayManager>();
        ingredientInt = 0; // go the start of their order
    }

    // Update is called once per frame
    void Update()
    {
        impatience -= Time.deltaTime;
        if (impatience <= 0)
        {
            StartCoroutine(gManager.ReadjustDelay()); // Triggers the function to wait before readjusting the customers as it wont sort them otherwise as this customer wont have been removed yet
            Destroy(gameObject);
        }


        if (items.Count != 0)
        {
            ingredient = items[ingredientInt].GetComponent<Ingredient>();
        }

        if (ingredient != null)
        {
            ingredient.Update();
        }
        else
        {
            customerActive = false;
        }

        if (customerActive) {
            UpdateSprites();
        }



    }

    public void GenerateCustomer() 
    {

        Initialise();
        // set the panel to be active
        panelForOrder = Instantiate(panelForOrderPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        panelForOrder.transform.SetParent(canvas.transform);

        panelForOrder.SetActive(true);
        panelForOrder.transform.localScale = new Vector3(0.0025f, 0.0025f, 0.0025f);
        panelForOrder.transform.localPosition = new Vector3(0f,0f,0f);
        panelForOrder.transform.localPosition = new Vector3(5, 5, 5);
        bool continueAdding = true; // whether we should keep adding
        float randomNumberForContinuation = (1/(float)maxNumberOfIngredientsPerOrder); // a float for probability
        int randomNumberForIngredient = Random.Range(0, ingredients.Length); // a float for randomizing ingredients



        GameObject bunImageObject = new GameObject();
        Image bunImageToAdd = bunImageObject.AddComponent<Image>();
        ingredientSprites.Add(bunImageObject);
        bunImageToAdd.sprite = bunSprite;
        bunImageToAdd.transform.SetParent(panelForOrder.transform, false);
        bunImageToAdd.transform.localPosition = new Vector3(-800, 500 - 100 * items.Count, 0);
        GameObject bunToAdd = Instantiate(bun, new Vector3(-10f, -10f, -10f), Quaternion.identity); ;
        bunToAdd.transform.parent = gameObject.transform;
        bunToAdd.GetComponent<Ingredient>().Generate();

        items.Add(bunToAdd);


        while (continueAdding == true) // while we should keep adding
        {
            GameObject objectToAdd = Instantiate(ingredients[randomNumberForIngredient], new Vector3(-10f, -10f, -10f), Quaternion.identity);
            objectToAdd.transform.parent = gameObject.transform;
            objectToAdd.GetComponent<Ingredient>().Generate();
            items.Add(objectToAdd); // add an ingredient
            float testNumber = Random.Range(0.0f, 1.0f); // create a random number between 0 and 1

            

            // test whether random number is bigger than our test value squared (squaring weights it towards bigger numbers)
            if (testNumber <= randomNumberForContinuation * randomNumberForContinuation) 
            { 
                continueAdding = false; // stop adding
            }
            else 
            {
                randomNumberForContinuation += (1 / (float)maxNumberOfIngredientsPerOrder); // increase our test number
                randomNumberForIngredient = Random.Range(0, ingredients.Length); // choose a new test value
            }

            GameObject imageObject = new GameObject();
            Image imageToAdd = imageObject.AddComponent<Image>();
            imageToAdd.gameObject.SetActive(true);
            imageToAdd.enabled = true;
            ingredientSprites.Add(imageObject);
            if (objectToAdd.name == "lettuce(Clone)") {
                imageToAdd.sprite = lettuceSprite;
            } else if (objectToAdd.name == "Tomato(Clone)") {
                imageToAdd.sprite = tomatoSprite;
            } else if (objectToAdd.name == "burger(Clone)") {
                imageToAdd.sprite = burgerSprite;
            } else if (objectToAdd.name == "Onion(Clone)") {
                imageToAdd.sprite = onionSprite;
            } else {
                imageToAdd.sprite = bunSprite;
            }

            imageToAdd.transform.SetParent(panelForOrder.transform, false);
            // No idea why this has to be -900 instead of -800 like the other 2 but for some reason it works
            imageToAdd.transform.localPosition = new Vector3(-900, 600 - 100 * items.Count, 0);



        }


        GameObject bunTwoImageObject = new GameObject();
        Image bunTwoImageToAdd = bunTwoImageObject.AddComponent<Image>();
        ingredientSprites.Add(bunTwoImageObject);
        bunTwoImageToAdd.sprite = bunSprite;
        bunTwoImageToAdd.transform.SetParent(panelForOrder.transform, false);
        bunTwoImageToAdd.transform.localPosition = new Vector3(-800, 500 - 100 * items.Count, 0);

        completed.name = "completed";
        items.Add(bunToAdd);

        items.Add(completed);

        for (int i = 0; i<=stepSpriteList.Count - 1; i++) {
            stepSpriteList[i].gameObject.transform.SetParent(panelForOrder.transform, false);
            stepSpriteList[i].gameObject.transform.localPosition = new Vector3(-110, -85, -900 - i *100);
            stepSpriteList[i].gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        }        
    }

    public void UpdateSprites() {
        currentSprite = ingredientSprites[ingredientInt];
        foreach (GameObject sprite in ingredientSprites) {
            Vector3 spritePos = sprite.transform.localPosition;
            if (sprite != currentSprite) {
                spritePos.x = -800f;
            } else {
                spritePos.x = -700f;
            }
            sprite.transform.localPosition = spritePos;
        }
        for (int i = 0; i <= currentIngredient.ingredientSteps.Count - 1; i++) {

            stepSpriteList[i].sprite = StepImage(currentIngredient.ingredientSteps[i]);
        }
    }

    private Sprite StepImage(string step) {
        switch (step) {
            case "PeelLeaves":
                return lettuceSprite;
            case "Chop":
                return knifeSprite;
            case "Tenderise":
                return burgerSprite;
            case "Salt":
                return saltSprite;    
            case "Cook":
                return fireSprite;
        }
        return null;
    }
}




