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
    SoundPlayer soundPlayerScript;

    Vector3 btnSize;

    bool overRollBtn = false;
    bool overShowBtn = false;
    bool overGoBtn = false;

    bool activatedAnimPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        // Assuming these scripts are attached to the same GameObject
        coreDataScript = GetComponent<CoreData>();
        guiManagerScript = GetComponent<GuiManager>();
        saveSystemScript = GetComponent<SaveSystem>();
        soundPlayerScript = GetComponent<SoundPlayer>();

        coreDataScript.DateventureCounter = saveSystemScript.LoadData();
        guiManagerScript.DateventureCounter.text = $"Dateventures: {coreDataScript.DateventureCounter.ToString()}";

        guiManagerScript.RollButton.onClick.AddListener(OnRollButtonActivated);
        guiManagerScript.ShowButton.onClick.AddListener(OnShowButtonActivated);
        guiManagerScript.GoButton.onClick.AddListener(OnGoButtonActivated);
        guiManagerScript.ReturnButton.onClick.AddListener(OnReturnButtonActivated);

        btnSize = guiManagerScript.RollButton.transform.localScale * 1.2f;

        // Add an EventTrigger component to the RollButton and subscribe to the PointerEnter event
        // TODO: DO THIS WITH HELPER FUNCS AND MAKE SURE ITS SAFE AND RELIABLE // IMPROVE READABILITY
        AddEventTrigger(guiManagerScript.RollButton, EventTriggerType.PointerEnter, (data) => { OnMouseEnterButton(guiManagerScript.RollButton); });
        AddEventTrigger(guiManagerScript.RollButton, EventTriggerType.PointerExit, (data) => { OnMouseExitButton(guiManagerScript.RollButton); });

        AddEventTrigger(guiManagerScript.ShowButton, EventTriggerType.PointerEnter, (data) => { OnMouseEnterButton(guiManagerScript.ShowButton); });
        AddEventTrigger(guiManagerScript.ShowButton, EventTriggerType.PointerExit, (data) => { OnMouseExitButton(guiManagerScript.ShowButton); });

        AddEventTrigger(guiManagerScript.GoButton, EventTriggerType.PointerEnter, (data) => { OnMouseEnterButton(guiManagerScript.GoButton); });
        AddEventTrigger(guiManagerScript.GoButton, EventTriggerType.PointerExit, (data) => { OnMouseExitButton(guiManagerScript.GoButton); });

        AddEventTrigger(guiManagerScript.ReturnButton, EventTriggerType.PointerEnter, (data) => { OnMouseEnterButton(guiManagerScript.ReturnButton); });
        AddEventTrigger(guiManagerScript.ReturnButton, EventTriggerType.PointerExit, (data) => { OnMouseExitButton(guiManagerScript.ReturnButton); });
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

            case var _ when btn == guiManagerScript.ReturnButton:
                if (!btn.interactable)
                {
                    return;
                }
                break;

            default:
                break;
        }

        btn.transform.DOScale(scaleFactor, duration)
            .SetEase(Ease.OutSine);

        soundPlayerScript.PlayHoverSound();
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

            case var _ when btn == guiManagerScript.ReturnButton:
                if (!btn.interactable)
                {
                    return;
                }
                break;

            default:
                break;
        }

        if (!activatedAnimPlaying)
        {
            btn.transform.DOScale(scaleFactor, duration)
                .SetEase(Ease.OutSine);
        }

    }

    void ResetButtonSize(Button btn)
    {
        activatedAnimPlaying = false;

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
            guiManagerScript.GoButton.interactable = true;

            if (guiManagerScript.RollButton.IsActive())
            {
                guiManagerScript.RollButton.gameObject.SetActive(false);
            }
            if (guiManagerScript.GoButton.IsActive())
            {
                guiManagerScript.GoButton.gameObject.SetActive(false);
            }

            if (!guiManagerScript.ReturnButton.IsActive())
            {
                guiManagerScript.ReturnButton.gameObject.SetActive(true);
                RevealBtn(guiManagerScript.ReturnButton);
            }
        }
        else if (btn == guiManagerScript.ReturnButton)
        {
            guiManagerScript.ReturnButton.interactable = true;

            if (guiManagerScript.ReturnButton.IsActive())
            {
                guiManagerScript.ReturnButton.gameObject.SetActive(false);
            }

            guiManagerScript.ReturnButton.transform.localScale = Vector2.one;
        }
    }

    void SlightlyRevealDateCard()
    {
        // TODO: Remove magic nums
        guiManagerScript.DateCard.transform.DOLocalMoveY(-500, 0.2f)
            .SetEase(Ease.InOutSine);
        soundPlayerScript.PlaySlightRevealSound();
    }

    void FullyRevealDateCard()
    {
        // TODO: Remove magic nums
        guiManagerScript.DateCard.transform.DOLocalMoveY(-30, 0.3f)
            .SetEase(Ease.InBack);
    }

    void ShiftContainerPos()
    {
        guiManagerScript.RollShowGoContainer.transform.DOLocalMoveY(0, 0.2f)
            .SetEase(Ease.InOutSine);
    }

    void RevealBtn(Button btn)
    {
        btn.transform.localScale = Vector2.zero;

        btn.transform.DOScale(Vector2.one, 0.2f)
            .SetEase(Ease.OutBack);
    }

    void OnRollButtonActivated()
    {
        guiManagerScript.RollButton.interactable = false;

        activatedAnimPlaying = true;
        guiManagerScript.RollButton.transform.DOScale(guiManagerScript.GuiElementSizeData["activatedRollButtonSize"], 0.1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => ResetButtonSize(guiManagerScript.RollButton));
        soundPlayerScript.PlayClickSound();

        if (guiManagerScript.RollShowGoContainer.transform.localPosition != Vector3.zero)
        {
            guiManagerScript.RollShowGoContainer.transform.DOLocalMoveX(0, 0.2f)
              .SetEase(Ease.InBack);
        }

        ResetButtonSize(guiManagerScript.ReturnButton);

        // TODO: dont allow clicking again until tween completes

        string key = GenerateDateCardKey();
        guiManagerScript.DateIdea.text = key;
        guiManagerScript.DateDetails.text = coreDataScript.Dateventures[key];

        // Now that a date card exists, enable the ShowButton & DateCard, hide GoButton if needed
        if (!guiManagerScript.ShowButton.IsActive())
        {
            if (guiManagerScript.RollShowGoContainer.transform.localPosition.x != -480)
            {
                guiManagerScript.ShowButton.gameObject.SetActive(true);
                RevealBtn(guiManagerScript.ShowButton);
            }
            else
            {
                guiManagerScript.ShowButton.gameObject.SetActive(true);
            }
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

        if (guiManagerScript.DateCard.transform.localPosition.y != -500)
        {
            SlightlyRevealDateCard();
        }

        ShiftContainerPos();
    }

    void OnShowButtonActivated()
    {
        guiManagerScript.ShowButton.interactable = false;

        guiManagerScript.ShowButton.transform.DOScale(guiManagerScript.GuiElementSizeData["activatedRollButtonSize"], 0.1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => ResetButtonSize(guiManagerScript.ShowButton));
        soundPlayerScript.PlayClickSound();

        if (guiManagerScript.ShowButton.IsActive())
        {
            guiManagerScript.ShowButton.gameObject.SetActive(false);
        }
        if (!guiManagerScript.GoButton.IsActive())
        {
            guiManagerScript.GoButton.gameObject.SetActive(true);
        }

        guiManagerScript.RollShowGoContainer.transform.DOLocalMoveX(-480, 0.2f)
               .SetEase(Ease.InBack);

        FullyRevealDateCard();
        soundPlayerScript.PlayFullRevealSound();
    }

    void OnGoButtonActivated()
    {
        guiManagerScript.GoButton.interactable = false;

        guiManagerScript.RollShowGoContainer.transform.DOScale(Vector2.zero, 0.2f)
            .SetEase(Ease.InBack)
            .OnComplete(() => ResetButtonSize(guiManagerScript.GoButton));
        soundPlayerScript.PlayClickSound();

        coreDataScript.DateventureCounter++;
        guiManagerScript.DateventureCounter.text = $"Dateventures: {coreDataScript.DateventureCounter.ToString()}";

        saveSystemScript.SaveData();
    }

    void OnReturnButtonActivated()
    {
        guiManagerScript.ReturnButton.interactable = false;

        Vector2 btnSize = Vector2.zero;

        guiManagerScript.ReturnButton.transform.DOScale(btnSize, 0.1f)
            .SetEase(Ease.InSine);
        soundPlayerScript.PlayClickSound();

        if (!guiManagerScript.RollButton.IsActive())
        {
            guiManagerScript.RollButton.gameObject.SetActive(true);
        }

        guiManagerScript.RollShowGoContainer.transform.DOScale(Vector2.one, 0.2f)
            .SetEase(Ease.OutBack);

        // TODO: Reset screen back to starting screen with a function
        guiManagerScript.DateCard.transform.localPosition = guiManagerScript.GuiElementPositionData["hiddenCardPos"];
        guiManagerScript.RollShowGoContainer.transform.localPosition = guiManagerScript.GuiElementPositionData["defaultRollShowGoPos"];
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