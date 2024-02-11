using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuiManager : MonoBehaviour
{
    [SerializeField] Button rollButton, showButton, goButton, returnButton;
    [SerializeField] RawImage dateCard;
    [SerializeField] TextMeshProUGUI dateIdea, dateDetails, dateventureCounter, showText, goText;
    [SerializeField] GameObject rollShowGoContainer;

    Dictionary<string, Vector2> guiElementPositionData = new Dictionary<string, Vector2>
        {
        // TODO: Improve naming conventions

            // DateCard
            { "hiddenCardPos", new Vector2(0, -670) },
            { "defaultCardPosition", new Vector2(0, -500) },
            { "showCardPosition", new Vector2(0, 0) },
            
            // RollButton, ShowButton, and GoButton
            { "defaultRollButtonPosition", new Vector2(0, 40) },
            { "revealedRollButtonPosition", new Vector2(-480, 40) },
            { "showButtonPosition", new Vector2(0, -40) },
            { "goButtonPosition", new Vector2(-480, -40) },

            // RollShowGoContainer
            { "defaultRollShowGoPos", new Vector2(0, -40) },
            { "centerRollShowGoPos", new Vector2(0, 0) },
            { "leftRollShowGoPos", new Vector2(-480, 0) },
        };

    Dictionary<string, Vector2> guiElementSizeData = new Dictionary<string, Vector2>
        {
            // RollButton
            { "defaultRollButtonSize", new Vector2(1, 1) },
            { "enteredRollButtonSize", new Vector2(1.1f, 1.1f) },
            { "activatedRollButtonSize", new Vector2(0.9f, 0.9f) },
        };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Getter and setter for rollButton
    public Button RollButton 
    {  
        get { return rollButton; } 
        set { rollButton = value; }
    }

    // Getter and setter for showButton
    public Button ShowButton
    {
        get { return showButton; }
        set { showButton = value; }
    }

    // Getter and setter for goButton
    public Button GoButton
    {
        get { return goButton; }
        set { goButton = value; }
    }

    // Getter and setter for returnButton
    public Button ReturnButton
    {
        get { return returnButton; }
        set { returnButton = value; }
    }

    // Getter and setter for rollShowGoContainer
    public GameObject RollShowGoContainer
    {
        get { return rollShowGoContainer; }
        set { rollShowGoContainer = value; }
    }

    // Getter and setter for dateCard
    public RawImage DateCard
    {
        get { return dateCard; }
        set { dateCard = value; }
    }

    // Getter and setter for dateIdea
    public TextMeshProUGUI DateIdea
    {
        get { return dateIdea; }
        set { dateIdea = value; }
    }

    // Getter and setter for dateDetails
    public TextMeshProUGUI DateDetails
    {
        get { return dateDetails; }
        set { dateDetails = value; }
    }

    // Getter and setter for dateventureCounter
    public TextMeshProUGUI DateventureCounter
    {
        get { return dateventureCounter; }
        set { dateventureCounter = value; }
    }

    // Getter and setter for showText
    public TextMeshProUGUI ShowText
    {
        get { return showText; }
        set { showText = value; }
    }

    // Getter and setter for goText
    public TextMeshProUGUI GoText
    {
        get { return goText; }
        set { goText = value; }
    }

    public Dictionary<string, Vector2> GuiElementPositionData
    {
        get { return guiElementPositionData; }
    }

    public Dictionary<string, Vector2> GuiElementSizeData
    {
        get { return guiElementSizeData; }
    }
}