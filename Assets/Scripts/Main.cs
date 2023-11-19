using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class Main : MonoBehaviour
{
    // Assuming the Data script is attached to the same GameObject
    Data dataScript;
    GUI guiScript;

    // Start is called before the first frame update
    void Start()
    {
        // Assuming the Data script is attached to the same GameObject
        dataScript = GetComponent<Data>();
        guiScript = GetComponent<GUI>();

        guiScript.RollButton.onClick.AddListener(OnRollButtonActivated);
        guiScript.GoButton.onClick.AddListener(OnGoButtonActivated);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnRollButtonActivated()
    {
        string key = GenerateDateCardKey();
        guiScript.DateIdea.text = key;
        guiScript.DateDetails.text = dataScript.Dateventures[key];

        // Now that a date card exists, enable the GoButton
        if (!guiScript.GoButton.IsActive())
        {
            guiScript.GoButton.gameObject.SetActive(true);
        }

        // TODO: Reset positions only if needed
        guiScript.DateCard.transform.localPosition = guiScript.GuiElementPositionsData["defaultCardPosition"];
        guiScript.RollButton.transform.localPosition = guiScript.GuiElementPositionsData["defaultRollButtonPosition"];
        guiScript.GoButton.transform.localPosition = guiScript.GuiElementPositionsData["defaultGoButtonPosition"];
    }

    void OnGoButtonActivated()
    {
        guiScript.DateCard.transform.localPosition = guiScript.GuiElementPositionsData["showCardPosition"];
        guiScript.RollButton.transform.localPosition = guiScript.GuiElementPositionsData["revealedRollButtonPosition"];
        guiScript.GoButton.transform.localPosition = guiScript.GuiElementPositionsData["revealedGoButtonPosition"];
    }

    string GenerateDateCardKey()
    {
        // Randomly select a date card key from AvailableRolls
        int availableRollsLength = dataScript.AvailableRolls.Count;
        string dateCardKey = dataScript.AvailableRolls[Random.Range(0, availableRollsLength)];

        // Move the selected spin from AvailableRolls to RecentSpins
        dataScript.AvailableRolls.Remove(dateCardKey);
        dataScript.RecentRolls.Add(dateCardKey);

        // If within threshold, no need to add back to AvailableRolls yet
        if (dataScript.AvailableRolls.Count > dataScript.RollsThreshold)
        {
            return dateCardKey;
        }

        // Add the first spin from RecentSpins back to AvailableRolls
        dataScript.AvailableRolls.Add(dataScript.RecentRolls[0]);
        dataScript.RecentRolls.RemoveAt(0);

        return dateCardKey;
    }
}