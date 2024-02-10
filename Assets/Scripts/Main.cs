using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Tracing;

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

    [SerializeField]
    private GameObject heartPrefab;

    RectTransform canvasRectTransform;

    float yPosScalar;

    // Start is called before the first frame update
    void Start()
    {
        canvasRectTransform = GetComponent<RectTransform>();

        yPosScalar = (canvasRectTransform.rect.height / 2);

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
        Exit();
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

    void IncrementDateventureCounter()
    {
        float expandSize = 1.2f;
        float animationDuration = 0.2f;

        // Initial values
        Vector3 originalScale = guiManagerScript.DateventureCounter.rectTransform.localScale;
        Color originalColor = guiManagerScript.DateventureCounter.color;
        Color myColor = ColorUtility.TryParseHtmlString("#EF2D56", out Color color) ? color : Color.white;

        // Sequence for the animation
        Sequence sequence = DOTween.Sequence();

        // Expand the text
        sequence.Append(guiManagerScript.DateventureCounter.DOColor(myColor, animationDuration))
                .Append(guiManagerScript.DateventureCounter.DOColor(originalColor, animationDuration))
                .Play();

        guiManagerScript.DateventureCounter.transform.DOScale(Vector2.one * expandSize, animationDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                guiManagerScript.DateventureCounter.transform.DOScale(Vector2.one, animationDuration)
                    .SetEase(Ease.OutSine);
            });
    }

    void HideDateCard()
    {
        guiManagerScript.DateCard.transform.DOLocalMoveY(-300 - yPosScalar, 0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(() => 
            {
                guiManagerScript.RollShowGoContainer.transform.DOScale(Vector2.one, 0.2f)
                    .SetEase(Ease.OutBack);
            });
    }

    void SlightlyRevealDateCard()
    {
        // TODO: Remove magic nums
        guiManagerScript.DateCard.transform.DOLocalMoveY(-180 - yPosScalar, 0.2f)
            .SetEase(Ease.InSine);
        soundPlayerScript.PlaySlightRevealSound();
    }

    void FullyRevealDateCard()
    {

        // TODO: Remove magic nums
        guiManagerScript.DateCard.transform.DOLocalMoveY(0, 0.3f)
            .SetEase(Ease.InBack);
        soundPlayerScript.PlayFullRevealSound();
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

        if (guiManagerScript.RollShowGoContainer.transform.localPosition.x == -480)
        {
            guiManagerScript.RollShowGoContainer.transform.DOLocalMoveX(0, 0.2f)
              .SetEase(Ease.InBack);

            guiManagerScript.GoText.rectTransform.DOLocalMoveY(-60, 0.1f)
            .SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                guiManagerScript.GoButton.gameObject.SetActive(false);
                guiManagerScript.GoText.rectTransform.anchoredPosition = new Vector2(0, 0);

                guiManagerScript.ShowButton.gameObject.SetActive(true);
                guiManagerScript.ShowText.rectTransform.anchoredPosition = new Vector2(0, 60);
                guiManagerScript.ShowText.rectTransform.DOLocalMoveY(0, 0.1f)
                    .SetEase(Ease.InSine);
            });
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
        }
        if (!guiManagerScript.DateCard.IsActive())
        {
            guiManagerScript.DateCard.gameObject.SetActive(true);
        }
        if (guiManagerScript.GoButton.IsActive())
        {
            //guiManagerScript.GoButton.gameObject.SetActive(false);
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
            //guiManagerScript.ShowButton.gameObject.SetActive(false);
        }
        if (!guiManagerScript.GoButton.IsActive())
        {
            //guiManagerScript.GoButton.gameObject.SetActive(true);
        }

        guiManagerScript.ShowText.rectTransform.DOLocalMoveY(-60, 0.1f)
            .SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                guiManagerScript.ShowButton.gameObject.SetActive(false);
                guiManagerScript.ShowText.rectTransform.anchoredPosition = new Vector2(0, 0);

                guiManagerScript.GoButton.gameObject.SetActive(true);
                guiManagerScript.GoText.rectTransform.anchoredPosition = new Vector2(0, 60);
                guiManagerScript.GoText.rectTransform.DOLocalMoveY(0, 0.1f)
                    .SetEase(Ease.InSine);
            });
        guiManagerScript.RollShowGoContainer.transform.DOLocalMoveX(-480, 0.2f)
            .SetEase(Ease.InBack);

        FullyRevealDateCard();
    }

    void ApplyHeartsEffect()
    {
        // TODO: Fix hearts on all screen sizes

        // Randomly determine the number of prefabs to instantiate
        //int numberOfPrefabs = Random.Range(10, 20 + 1);
        float xScalar = canvasRectTransform.rect.width / 2;
        float xMultiplier = xScalar / 5;

        // Instantiate random prefabs with random positions within the bounds
        for (int i = 0; i < 10; i++)
        {
            float randomX = Random.Range(-xScalar + xMultiplier * i, -xScalar + xMultiplier * (i + 1));
            float randomY = Random.Range(-400 - yPosScalar, -600 - yPosScalar);

            Vector2 randomPosition = new Vector2(randomX, randomY);

            // Instantiate prefab at the random local position
            GameObject heartInstance = Instantiate(heartPrefab, Vector2.zero, Quaternion.identity);

            heartInstance.transform.parent = transform;

            // Set the local position based on the random position
            heartInstance.transform.localPosition = randomPosition;

            heartInstance.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
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

        soundPlayerScript.PlayCounterSound();

        IncrementDateventureCounter();
        ApplyHeartsEffect();

        // Animate counter change here... ideas: maybe make a back easing growth, animated heart pop up... ?

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

        // TODO: Reset screen back to starting screen with a function
        HideDateCard();
        guiManagerScript.RollShowGoContainer.transform.localPosition = guiManagerScript.GuiElementPositionData["defaultRollShowGoPos"];
    }

    void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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