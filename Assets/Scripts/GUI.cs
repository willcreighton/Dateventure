using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI : MonoBehaviour
{
    public Button spinner;
    public RawImage dateCard;
    public TextMeshProUGUI dateIdea, dateDetails;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Getter for spinner
    public Button Spinner 
    {  
        get { return spinner; } 
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
}