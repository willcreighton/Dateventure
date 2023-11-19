using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
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
    // dateVentureCounter : Number of dateventures completed
    int dateventuresLength, dateVentureCounter; 

    // headerQuips : Pre-spin header quips, i.e. "Hey Name1 and Name2. :winky-emoji:"
    // spinningQuips : Header quips for while spinning, i.e. "What's it gonna be!? :eyes-emoji:"
    string[] headerQuips, spinningQuips;

    // recentSpins : Track the 10 most recent spins
    // availableSpins : The currently available date cards
    // keys : All of the keys within dateventures
    List<string> recentSpins, availableSpins, keys; 

    // Start is called before the first frame update
    void Start()
    {
        dateventuresLength = dateventures.Count;
        //dateventureCounter = 0; // TODO: Dynamically store the correct amount of dateventures completed
        keys = new List<string>(dateventures.Keys);
        availableSpins = keys;
        recentSpins = new List<string>();
    }

    // Getter for dateventures
    public Dictionary<string, string> Dateventures
    {
        get { return dateventures; }
    }

    // Getter and setter for recentSpins
    public List<string> RecentSpins
    {
        get { return recentSpins; }
        set { recentSpins = value; }
    }

    // Getter and setter for availableSpins
    public List<string> AvailableSpins
    {
        get { return availableSpins; }
        set { availableSpins = value; }
    }

    // Getter for keys
    public List<string> Keys
    {
        get { return keys; }
    }

    // Getter for dateventuresLength
    public int DateventuresLength
    {
        get { return dateventuresLength; }
    }
}