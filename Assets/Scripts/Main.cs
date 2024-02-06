using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class Main : MonoBehaviour
{
    // Declare the classes we will utilize
    CoreData coreDataScript;
    GuiManager guiManagerScript;
    SaveSystem saveSystemScript;

    Vector3 btnSize;

    bool overRollBtn = false;
    bool overShowBtn = false;
    bool overGoBtn = false;

    // Start is called before the first frame update
    void Start()
    {
        // Assuming these scripts are attached to the same GameObject
        coreDataScript = GetComponent<CoreData>();
        guiManagerScript = GetComponent<GuiManager>();
        saveSystemScript = GetComponent<SaveSystem>();

        saveSystemScript.LoadData();

        guiManagerScript.RollButton.onClick.AddListener(OnRollButtonActivated);
        guiManagerScript.ShowButton.onClick.AddListener(OnShowButtonActivated);
        guiManagerScript.GoButton.onClick.AddListener(OnGoButtonActivated);

        btnSize = guiManagerScript.RollButton.transform.localScale * 1.2f;

        // Add an EventTrigger component to the RollButton and subscribe to the PointerEnter event
        // TODO: DO THIS WITH HELPER FUNCS AND MAKE SURE ITS SAFE AND RELIABLE // IMPROVE READABILITY
        AddEventTrigger(guiManagerScript.RollButton, EventTriggerType.PointerEnter, (data) => { OnMouseEnterButton(guiManagerScript.RollButton); });
        AddEventTrigger(guiManagerScript.RollButton, EventTriggerType.PointerExit, (data) => { OnMouseExitButton(guiManagerScript.RollButton); });

        AddEventTrigger(guiManagerScript.ShowButton, EventTriggerType.PointerEnter, (data) => { OnMouseEnterButton(guiManagerScript.ShowButton); });
        AddEventTrigger(guiManagerScript.ShowButton, EventTriggerType.PointerExit, (data) => { OnMouseExitButton(guiManagerScript.ShowButton); });

        AddEventTrigger(guiManagerScript.GoButton, EventTriggerType.PointerEnter, (data) => { OnMouseEnterButton(guiManagerScript.GoButton); });
        AddEventTrigger(guiManagerScript.GoButton, EventTriggerType.PointerExit, (data) => { OnMouseExitButton(guiManagerScript.GoButton); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddEventTrigger(Button button, EventTriggerType triggerType, UnityAction<BaseEventData> callback)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();

        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener(callback);
        trigger.triggers.Add(entry);
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

            case var _ when btn == guiManagerScript.ShowButton:
                overShowBtn = true;
                break;

            case var _ when btn == guiManagerScript.GoButton:
                overGoBtn = true;
                break;

            default:
                break;
        }

        btn.transform.DOScale(scaleFactor, duration)
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

            case var _ when btn == guiManagerScript.ShowButton:
                overShowBtn = false;
                break;

            case var _ when btn == guiManagerScript.GoButton:
                overGoBtn = false;
                break;

            default:
                break;
        }

        btn.transform.DOScale(scaleFactor, duration)
            .SetEase(Ease.OutSine);
    }

    void ResetButtonSize(Button btn)
    {

        if (btn == guiManagerScript.RollButton)
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
                   .OnComplete(() => guiManagerScript.RollButton.interactable = true);
            }
        }
        else if (btn == guiManagerScript.ShowButton)
        {
            guiManagerScript.ShowButton.transform.DOScale(guiManagerScript.GuiElementSizeData["defaultRollButtonSize"], 0.2f)
                   .SetEase(Ease.OutSine)
                   .OnComplete(() => guiManagerScript.ShowButton.interactable = true);
        }
        else if (btn == guiManagerScript.GoButton)
        {
            if (overGoBtn)
            {
                guiManagerScript.GoButton.transform.DOScale(guiManagerScript.GuiElementSizeData["enteredRollButtonSize"], 0.2f)
                   .SetEase(Ease.OutSine)
                   .OnComplete(() => guiManagerScript.GoButton.interactable = true);
            }
            else
            {
                guiManagerScript.GoButton.transform.DOScale(guiManagerScript.GuiElementSizeData["defaultRollButtonSize"], 0.2f)
                   .SetEase(Ease.OutSine)
                   .OnComplete(() => guiManagerScript.GoButton.interactable = true);
            }

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
            .OnComplete(() => ResetButtonSize(guiManagerScript.RollButton));
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
        guiManagerScript.ShowButton.interactable = false;

        guiManagerScript.ShowButton.transform.DOScale(guiManagerScript.GuiElementSizeData["activatedRollButtonSize"], 0.1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => ResetButtonSize(guiManagerScript.ShowButton));

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
        guiManagerScript.GoButton.interactable = false;

        guiManagerScript.GoButton.transform.DOScale(guiManagerScript.GuiElementSizeData["activatedRollButtonSize"], 0.1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => ResetButtonSize(guiManagerScript.GoButton));

        coreDataScript.DateventureCounter++;
        Debug.Log($"You've gone on {coreDataScript.DateventureCounter} dateventures!");

        saveSystemScript.SaveData();
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