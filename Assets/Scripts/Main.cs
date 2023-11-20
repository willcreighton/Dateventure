using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class Main : MonoBehaviour
{
    // Declare the classes we will utilize
    CoreData coreDataScript;
    GuiManager guiManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Assuming these scripts are attached to the same GameObject
        coreDataScript = GetComponent<CoreData>();
        guiManagerScript = GetComponent<GuiManager>();

        guiManagerScript.RollButton.onClick.AddListener(OnRollButtonActivated);
        guiManagerScript.GoButton.onClick.AddListener(OnGoButtonActivated);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnRollButtonActivated()
    {
        string key = GenerateDateCardKey();
        guiManagerScript.DateIdea.text = key;
        guiManagerScript.DateDetails.text = coreDataScript.Dateventures[key];

        // Now that a date card exists, enable the GoButton & DateCard
        if (!guiManagerScript.GoButton.IsActive())
        {
            guiManagerScript.GoButton.gameObject.SetActive(true);
        }
        if (!guiManagerScript.DateCard.IsActive())
        {
            guiManagerScript.DateCard.gameObject.SetActive(true);
        }

        // TODO: Reset positions only if needed
        guiManagerScript.DateCard.transform.localPosition = guiManagerScript.GuiElementPositionsData["defaultCardPosition"];
        guiManagerScript.RollButton.transform.localPosition = guiManagerScript.GuiElementPositionsData["defaultRollButtonPosition"];
        guiManagerScript.GoButton.transform.localPosition = guiManagerScript.GuiElementPositionsData["defaultGoButtonPosition"];
    }

    void OnGoButtonActivated()
    {
        guiManagerScript.DateCard.transform.localPosition = guiManagerScript.GuiElementPositionsData["showCardPosition"];
        guiManagerScript.RollButton.transform.localPosition = guiManagerScript.GuiElementPositionsData["revealedRollButtonPosition"];
        guiManagerScript.GoButton.transform.localPosition = guiManagerScript.GuiElementPositionsData["revealedGoButtonPosition"];
    }

    string GenerateDateCardKey()
    {
        // Randomly select a date card key from AvailableRolls
        int availableRollsLength = coreDataScript.AvailableRolls.Count;
        string dateCardKey = coreDataScript.AvailableRolls[Random.Range(0, availableRollsLength)];

        // Move the selected spin from AvailableRolls to RecentSpins
        coreDataScript.AvailableRolls.Remove(dateCardKey);
        coreDataScript.RecentRolls.Add(dateCardKey);

        // If within threshold, no need to add back to AvailableRolls yet
        if (coreDataScript.AvailableRolls.Count > coreDataScript.RollsThreshold)
        {
            return dateCardKey;
        }

        // Add the first spin from RecentSpins back to AvailableRolls
        coreDataScript.AvailableRolls.Add(coreDataScript.RecentRolls[0]);
        coreDataScript.RecentRolls.RemoveAt(0);

        return dateCardKey;
    }
}