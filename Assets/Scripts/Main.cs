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

        guiScript.Spinner.onClick.AddListener(OnSpinnerClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSpinnerClicked()
    {
        string key = GenerateDateCardKey();
        guiScript.dateIdea.text = key;
        guiScript.dateDetails.text = dataScript.Dateventures[key];
        guiScript.dateCard.transform.position = Vector2.zero;
    }

    string GenerateDateCardKey()
    {
        // Randomly select a date card key from AvailableSpins
        int availableSpinsLength = dataScript.AvailableSpins.Count;
        string dateCardKey = dataScript.AvailableSpins[Random.Range(0, availableSpinsLength)];

        // Move the selected spin from AvailableSpins to RecentSpins
        dataScript.AvailableSpins.Remove(dateCardKey);
        dataScript.RecentSpins.Add(dateCardKey);

        // TODO: Change 2 to a constant that tracks 10 most recent spins, so availableSpins - 10 here mayb, think on it tho
        // Check if there are more than 2 spins in AvailableSpins
        if (dataScript.AvailableSpins.Count > 2)
        {
            return dateCardKey;
        }

        // Add the first spin from RecentSpins back to AvailableSpins
        dataScript.AvailableSpins.Add(dataScript.RecentSpins[0]);
        dataScript.RecentSpins.RemoveAt(0);

        return dateCardKey;
    }
}