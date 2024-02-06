using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuiManager : MonoBehaviour
{
    [SerializeField] Button rollButton, showButton, goButton;
    [SerializeField] RawImage dateCard;
    [SerializeField] TextMeshProUGUI dateIdea, dateDetails;

    Dictionary<string, Vector2> guiElementPositionData = new Dictionary<string, Vector2>
        {
            // DateCard
            { "defaultCardPosition", new Vector2(0, -500) },
            { "showCardPosition", new Vector2(0, 0) },
            
            // RollButton, ShowButton, and GoButton
            { "defaultRollButtonPosition", new Vector2(0, 0) },
            { "revealedRollButtonPosition", new Vector2(-480, 0) },
            { "showButtonPosition", new Vector2(0, -90) },
            { "goButtonPosition", new Vector2(-480, -90) },
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

    public Dictionary<string, Vector2> GuiElementPositionData
    {
        get { return guiElementPositionData; }
    }

    public Dictionary<string, Vector2> GuiElementSizeData
    {
        get { return guiElementSizeData; }
    }
}