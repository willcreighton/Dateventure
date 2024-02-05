using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class Main : MonoBehaviour
{
    // Declare the classes we will utilize
    CoreData coreDataScript;
    GuiManager guiManagerScript;

    Vector3 btnSize;

    bool overRollBtn = false;

    // Start is called before the first frame update
    void Start()
    {
        // Assuming these scripts are attached to the same GameObject
        coreDataScript = GetComponent<CoreData>();
        guiManagerScript = GetComponent<GuiManager>();

        guiManagerScript.RollButton.onClick.AddListener(OnRollButtonActivated);
        guiManagerScript.ShowButton.onClick.AddListener(OnShowButtonActivated);
        guiManagerScript.GoButton.onClick.AddListener(OnGoButtonActivated);

        btnSize = guiManagerScript.RollButton.transform.localScale * 1.2f;

        // Add an EventTrigger component to the RollButton and subscribe to the PointerEnter event
        // TODO: DO THIS WITH HELPER FUNCS AND MAKE SURE ITS SAFE AND RELIABLE
        EventTrigger trigger = guiManagerScript.RollButton.gameObject.AddComponent<EventTrigger>();

        // Add OnPointerEnter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnMouseEnterButton(guiManagerScript.RollButton); });
        trigger.triggers.Add(entryEnter);

        // Add OnPointerExit
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnMouseExitButton(guiManagerScript.RollButton); });
        trigger.triggers.Add(entryExit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnterButton(Button btn)
    {
        Vector2 scaleFactor = new Vector2(1.1f, 1.1f);
        float duration = 0.2f;

        switch (btn)
        {
            case var _ when btn == guiManagerScript.RollButton:
                overRollBtn = true;
                break;

            default:
                break;
        }

        guiManagerScript.RollButton.transform.DOScale(scaleFactor, duration)
            .SetEase(Ease.OutSine);
    }

    void OnMouseExitButton(Button btn)
    {
        Vector2 scaleFactor = new Vector2(1f, 1f);
        float duration = 0.2f;

        switch (btn)
        {
            case var _ when btn == guiManagerScript.RollButton:
                overRollBtn = false;
                break;

            default:
                break;
        }

        guiManagerScript.RollButton.transform.DOScale(scaleFactor, duration)
            .SetEase(Ease.OutSine);
    }

    void ResetButtonSize()
    {
        if (overRollBtn)
        {
            guiManagerScript.RollButton.transform.DOScale(guiManagerScript.GuiElementSizeData["enteredRollButtonSize"], 0.2f)
               .SetEase(Ease.OutSine)
               .OnComplete(() => guiManagerScript.RollButton.interactable = true);
        }
        else
        {
            guiManagerScript.RollButton.transform.DOScale(guiManagerScript.GuiElementSizeData["defaultRollButtonSize"], 0.2f)
               .SetEase(Ease.OutSine)
               .OnComplete(() => guiManagerScript.RollButton.interactable = true); ;
        }
    }

    void RevealShowButton()
    {

    }

    void OnRollButtonActivated()
    {
        guiManagerScript.RollButton.interactable = false;

        guiManagerScript.RollButton.transform.DOScale(guiManagerScript.GuiElementSizeData["activatedRollButtonSize"], 0.1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => ResetButtonSize());
        // TODO: dont allow clicking again until tween completes

        string key = GenerateDateCardKey();
        guiManagerScript.DateIdea.text = key;
        guiManagerScript.DateDetails.text = coreDataScript.Dateventures[key];

        // Now that a date card exists, enable the ShowButton & DateCard, hide GoButton if needed
        if (!guiManagerScript.ShowButton.IsActive())
        {
            guiManagerScript.ShowButton.gameObject.SetActive(true);
        }
        if (!guiManagerScript.DateCard.IsActive())
        {
            guiManagerScript.DateCard.gameObject.SetActive(true);
        }
        if (guiManagerScript.GoButton.IsActive())
        {
            guiManagerScript.GoButton.gameObject.SetActive(false);
        }

        // TODO: Reset positions only if needed
        guiManagerScript.DateCard.transform.localPosition = guiManagerScript.GuiElementPositionData["defaultCardPosition"];
        guiManagerScript.RollButton.transform.localPosition = guiManagerScript.GuiElementPositionData["defaultRollButtonPosition"];
        guiManagerScript.ShowButton.transform.localPosition = guiManagerScript.GuiElementPositionData["showButtonPosition"];
    }

    void OnShowButtonActivated()
    {
        if (guiManagerScript.ShowButton.IsActive())
        {
            guiManagerScript.ShowButton.gameObject.SetActive(false);
        }
        if (!guiManagerScript.GoButton.IsActive())
        {
            guiManagerScript.GoButton.gameObject.SetActive(true);
        }

        //guiManagerScript.DateCard.transform.localPosition = guiManagerScript.GuiElementPositionData["showCardPosition"];
        guiManagerScript.RollButton.transform.localPosition = guiManagerScript.GuiElementPositionData["revealedRollButtonPosition"];
        guiManagerScript.GoButton.transform.localPosition = guiManagerScript.GuiElementPositionData["goButtonPosition"];

        guiManagerScript.DateCard.transform.DOMove(guiManagerScript.GuiElementPositionData["showCardPosition"], 0.4f)
            .SetEase(Ease.InOutSine);
    }

    void OnGoButtonActivated()
    {

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