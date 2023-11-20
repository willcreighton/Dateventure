using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreData : MonoBehaviour
{
    // This is a dictionary, dateventures. It holds keys : Date Idea Header, and values : Date Idea Descriptions  
    Dictionary<string, string> dateventures = new Dictionary<string, string>
    {
        { "Pillow Fort!", "Who doesn't love a good pillow fort!? Grab your partner and make a pillow fort together. Sit back, relax, and have some fun!" },
        { "Date idea 1.", "Date idea 1 details..." },
        { "Date idea 2.", "Date idea 2 details..." },
        { "Date idea 3.", "Date idea 3 details..." }
    };

    // dateventuresLength : Amount of dateventures in the dateventures dictionary
    // dateventureCounter : Number of dateventures completed
    int dateventuresLength, dateventureCounter;

    // rollsThreshold : Represents the threshold for removing from availableRolls before adding back to it
    const int rollsThreshold = 2;

    // headerQuips : Pre-spin header quips, i.e. "Hey Name1 and Name2. :winky-emoji:"
    // rollingQuips : Header quips for while rolling, i.e. "What's it gonna be!? :eyes-emoji:"
    string[] headerQuips, rollingQuips;

    // recentRolls : Track the rollsThreshold most recent rolls
    // availableRolls : The currently available date cards
    // dateKeys : All of the keys within dateventures
    List<string> recentRolls, availableRolls, dateKeys; 

    // Start is called before the first frame update
    void Start()
    {
        dateventuresLength = dateventures.Count;
        //dateventureCounter = 0; // TODO: Dynamically store the correct amount of dateventures completed
        dateKeys = new List<string>(dateventures.Keys);
        availableRolls = dateKeys;
        recentRolls = new List<string>();
    }

    // Getter for dateventures
    public Dictionary<string, string> Dateventures
    {
        get { return dateventures; }
    }

    // Getter and setter for recentRolls
    public List<string> RecentRolls
    {
        get { return recentRolls; }
        set { recentRolls = value; }
    }

    // Getter and setter for availableRolls
    public List<string> AvailableRolls
    {
        get { return availableRolls; }
        set { availableRolls = value; }
    }

    // Getter for keys
    public List<string> DateKeys
    {
        get { return dateKeys; }
    }

    // Getter for dateventuresLength
    public int DateventuresLength
    {
        get { return dateventuresLength; }
    }

    public int RollsThreshold
    {
        get { return rollsThreshold; }
    }
}