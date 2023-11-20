using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuiManager : MonoBehaviour
{
    [SerializeField] Button rollButton, goButton;
    [SerializeField] RawImage dateCard;
    [SerializeField] TextMeshProUGUI dateIdea, dateDetails;

    Dictionary<string, Vector2> guiElementPositionData = new Dictionary<string, Vector2>
        {
            // DateCard
            { "defaultCardPosition", new Vector2(0, -500) },
            { "showCardPosition", new Vector2(0, 0) },

            // RollButton and GoButton
            { "defaultRollButtonPosition", new Vector2(0, 0) },
            { "revealedRollButtonPosition", new Vector2(-480, 0) },
            { "defaultGoButtonPosition", new Vector2(0, -90) },
            { "revealedGoButtonPosition", new Vector2(-480, -90) },
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

    public Dictionary<string, Vector2> GuiElementPositionsData
    {
        get { return guiElementPositionData; }
    }
}