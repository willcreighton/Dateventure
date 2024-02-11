using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // Declare the classes we will utilize
    CoreData coreDataScript;
    GuiManager guiManagerScript;
    SaveSystem saveSystemScript;
    AudioManager audioManagerScript;

    // Define the Heart Prefab
    [SerializeField]
    private GameObject heartPrefab;

    // Define the Canvas RectTransform property
    RectTransform canvasRectTransform;

    bool overRollBtn = false;
    bool activatedAnimPlaying = false;

    float yPosScalar;

    // Start is called before the first frame update
    void Start()
    {
        // Define the Canvas RectTransform and a y-position scalar for y-axis anchor points
        canvasRectTransform = GetComponent<RectTransform>();
        yPosScalar = (canvasRectTransform.rect.height / 2);

        // Assign the scripts to their components to access their contents
        coreDataScript = GetComponent<CoreData>();
        guiManagerScript = GetComponent<GuiManager>();
        saveSystemScript = GetComponent<SaveSystem>();
        audioManagerScript = GetComponent<AudioManager>();

        // Default the Dateventures counter
        coreDataScript.DateventureCounter = saveSystemScript.LoadData();
        guiManagerScript.DateventureCounter.text = $"Dateventures: {coreDataScript.DateventureCounter.ToString()}";

        SetUpEvents();
    }

    // Update is called once per frame
    void Update()
    {
        Exit();
    }

    // Set up the event listeners
    void SetUpEvents()
    {
        // Click listeners
        guiManagerScript.RollButton.onClick.AddListener(OnRollButtonActivated);
        guiManagerScript.ShowButton.onClick.AddListener(OnShowButtonActivated);
        guiManagerScript.GoButton.onClick.AddListener(OnGoButtonActivated);
        guiManagerScript.ReturnButton.onClick.AddListener(OnReturnButtonActivated);

        // Enter and exit listeners
        AddEventTrigger(guiManagerScript.RollButton, EventTriggerType.PointerEnter, (data) => { OnMouseEnterButton(guiManagerScript.RollButton); });
        AddEventTrigger(guiManagerScript.RollButton, EventTriggerType.PointerExit, (data) => { OnMouseExitButton(guiManagerScript.RollButton); });

        AddEventTrigger(guiManagerScript.ShowButton, EventTriggerType.PointerEnter, (data) => { OnMouseEnterButton(guiManagerScript.ShowButton); });
        AddEventTrigger(guiManagerScript.ShowButton, EventTriggerType.PointerExit, (data) => { OnMouseExitButton(guiManagerScript.ShowButton); });

        AddEventTrigger(guiManagerScript.GoButton, EventTriggerType.PointerEnter, (data) => { OnMouseEnterButton(guiManagerScript.GoButton); });
        AddEventTrigger(guiManagerScript.GoButton, EventTriggerType.PointerExit, (data) => { OnMouseExitButton(guiManagerScript.GoButton); });

        AddEventTrigger(guiManagerScript.ReturnButton, EventTriggerType.PointerEnter, (data) => { OnMouseEnterButton(guiManagerScript.ReturnButton); });
        AddEventTrigger(guiManagerScript.ReturnButton, EventTriggerType.PointerExit, (data) => { OnMouseExitButton(guiManagerScript.ReturnButton); });
    }

    // Add a more custom event listener which accepts a callback function with parameters
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

    // Execute upon a mouse entering a button
    void OnMouseEnterButton(Button btn)
    {
        Vector2 scaleFactor = new Vector2(1.1f, 1.1f);
        float duration = 0.2f;

        if (btn == guiManagerScript.RollButton)
        {
            overRollBtn = true;
        }
        else if (btn == guiManagerScript.ReturnButton)
        {
            if (!btn.interactable)
            {
                // If the Return button has been entered, but the button is not interactable, return
                return;
            }
        }

        btn.transform.DOScale(scaleFactor, duration)
            .SetEase(Ease.OutSine);

        audioManagerScript.PlayHoverSound();
    }

    // Execute upon a mouse exiting a button
    void OnMouseExitButton(Button btn)
    {
        Vector2 scaleFactor = new Vector2(1f, 1f);
        float duration = 0.2f;

        if (btn == guiManagerScript.RollButton)
        {
            overRollBtn = false;
        }
        else if (btn == guiManagerScript.ReturnButton)
        {
            if (!btn.interactable)
            {
                // If the Return button has been entered, but the button is not interactable, return
                return;
            }
        }

        if (!activatedAnimPlaying)
        {
            btn.transform.DOScale(scaleFactor, duration)
                .SetEase(Ease.OutSine);
        }
    }

    // Reset the respective buttons
    void ResetButton(Button btn)
    {
        activatedAnimPlaying = false;

        // Roll button reset
        if (btn == guiManagerScript.RollButton)
        {
            if (overRollBtn)
            {
                guiManagerScript.RollButton.transform.DOScale(guiManagerScript.GuiElementSizeData["EnteredButtonSize"], 0.2f)
                   .SetEase(Ease.OutSine)
                   .OnComplete(() => guiManagerScript.RollButton.interactable = true);
            }
            else
            {
                guiManagerScript.RollButton.transform.DOScale(guiManagerScript.GuiElementSizeData["DefaultButtonSize"], 0.2f)
                   .SetEase(Ease.OutSine)
                   .OnComplete(() => guiManagerScript.RollButton.interactable = true);
            }
        }

        // Show button reset
        else if (btn == guiManagerScript.ShowButton)
        {
            guiManagerScript.ShowButton.transform.DOScale(guiManagerScript.GuiElementSizeData["DefaultButtonSize"], 0.2f)
                   .SetEase(Ease.OutSine)
                   .OnComplete(() => guiManagerScript.ShowButton.interactable = true);
        }

        // Go button reset
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

        // Return button reset
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

    // Increment the Dateventure counter and play feedback
    void IncrementDateventureCounter()
    {
        float expandSize = 1.2f;
        float animationDuration = 0.2f;

        coreDataScript.DateventureCounter++;
        guiManagerScript.DateventureCounter.text = $"Dateventures: {coreDataScript.DateventureCounter.ToString()}";

        audioManagerScript.PlayCounterSound();

        // Initial values
        Vector3 originalScale = guiManagerScript.DateventureCounter.rectTransform.localScale;
        Color originalColor = guiManagerScript.DateventureCounter.color;
        Color myColor = ColorUtility.TryParseHtmlString("#EF2D56", out Color color) ? color : Color.white;

        // Sequence for the animation
        Sequence sequence = DOTween.Sequence();

        // Color the text
        sequence.Append(guiManagerScript.DateventureCounter.DOColor(myColor, animationDuration))
                .Append(guiManagerScript.DateventureCounter.DOColor(originalColor, animationDuration))
                .Play();

        // Expand the text
        guiManagerScript.DateventureCounter.transform.DOScale(Vector2.one * expandSize, animationDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                guiManagerScript.DateventureCounter.transform.DOScale(Vector2.one, animationDuration)
                    .SetEase(Ease.OutSine);
            });
    }

    // Hide the date card
    void HideDateCard()
    {
        guiManagerScript.DateCard.transform.DOLocalMoveY(-300 - yPosScalar, 0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(() => 
            {
                guiManagerScript.RollShowGoContainer.transform.DOScale(Vector2.one, 0.2f)
                    .SetEase(Ease.OutBack);
            });

        guiManagerScript.RollShowGoContainer.transform.localPosition = guiManagerScript.GuiElementPositionData["DefaultContainerPos"];
    }

    // Reveal the top portion of the date card
    void SlightlyRevealDateCard()
    {
        guiManagerScript.DateCard.transform.DOLocalMoveY(-180 - yPosScalar, 0.2f)
            .SetEase(Ease.InSine);
        audioManagerScript.PlaySlightRevealSound();
    }

    // Fully reveal the date card
    void FullyRevealDateCard()
    {
        guiManagerScript.DateCard.transform.DOLocalMoveY(0, 0.3f)
            .SetEase(Ease.InBack);
        audioManagerScript.PlayFullRevealSound();
    }

    // Shift the RollShowGoContainer to the center of the screen
    void ShiftContainerPos()
    {
        guiManagerScript.RollShowGoContainer.transform.DOLocalMoveY(0, 0.2f)
            .SetEase(Ease.InOutSine);
    }

    // Reset the RollShowGoContainer
    void ResetContainerPos()
    {
        guiManagerScript.RollShowGoContainer.transform.DOScale(Vector2.zero, 0.2f)
            .SetEase(Ease.InBack)
            .OnComplete(() => ResetButton(guiManagerScript.GoButton));
    }

    // Hide a button with a popping shrinking effect
    void HideBtn(Button btn)
    {
        btn.transform.DOScale(Vector2.zero, 0.1f)
            .SetEase(Ease.InSine);
    }

    // Reveal a button with a popping expansion effect
    void RevealBtn(Button btn)
    {
        btn.transform.localScale = Vector2.zero;

        btn.transform.DOScale(Vector2.one, 0.2f)
            .SetEase(Ease.OutBack);
    }

    // Execute when the Roll button is pressed
    void OnRollButtonActivated()
    {
        guiManagerScript.RollButton.interactable = false;
        activatedAnimPlaying = true;

        guiManagerScript.RollButton.transform.DOScale(guiManagerScript.GuiElementSizeData["ActivatedButtonSize"], 0.1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => ResetButton(guiManagerScript.RollButton));
        audioManagerScript.PlayClickSound();

        // Move the RollShowGoContainer back to the center if applicable
        if (guiManagerScript.RollShowGoContainer.transform.localPosition.x == -480)
        {
            guiManagerScript.RollShowGoContainer.transform.DOLocalMoveX(0, 0.2f)
              .SetEase(Ease.InBack);

            // Perform a rolling text transition effect on the Show and Go button text
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

        ResetButton(guiManagerScript.ReturnButton);

        // Generate the date card
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
        if (guiManagerScript.DateCard.transform.localPosition.y != -500)
        {
            SlightlyRevealDateCard();
        }

        ShiftContainerPos();
    }

    // Execute when the Show button is pressed
    void OnShowButtonActivated()
    {
        guiManagerScript.ShowButton.interactable = false;

        guiManagerScript.ShowButton.transform.DOScale(guiManagerScript.GuiElementSizeData["ActivatedButtonSize"], 0.1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => ResetButton(guiManagerScript.ShowButton));
        audioManagerScript.PlayClickSound();

        // Perform a rolling text transition effect on the Show and Go button text
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

    // Execute when the Go button is pressed
    void OnGoButtonActivated()
    {
        guiManagerScript.GoButton.interactable = false;

        saveSystemScript.SaveData();
        audioManagerScript.PlayClickSound();

        ResetContainerPos();
        IncrementDateventureCounter();
        ApplyHeartsEffect();
    }

    // Execute when the Return button is pressed
    void OnReturnButtonActivated()
    {
        guiManagerScript.ReturnButton.interactable = false; 
        
        audioManagerScript.PlayClickSound();

        HideBtn(guiManagerScript.ReturnButton);
        HideDateCard();

        if (!guiManagerScript.RollButton.IsActive())
        {
            guiManagerScript.RollButton.gameObject.SetActive(true);
        }
    }

    // Spawn in the Heart Prefabs
    void ApplyHeartsEffect()
    {
        float xScalar = canvasRectTransform.rect.width / 2;
        float xMultiplier = xScalar / 5;

        // Instantiate 10 Heart Prefabs with controlled sliced random positions
        for (int i = 0; i < 10; i++)
        {
            // Calculate an x-value within the current 10% bounds, for every 10% of the screen
            float randomX = Random.Range(-xScalar + xMultiplier * i, -xScalar + xMultiplier * (i + 1));

            // Calculate a slightly random y-value to give the Hearts a more natural feel
            float randomY = Random.Range(-400 - yPosScalar, -600 - yPosScalar);

            Vector2 randomPosition = new Vector2(randomX, randomY);

            // Instantiate at the random local position
            GameObject heartInstance = Instantiate(heartPrefab, Vector2.zero, Quaternion.identity);
            heartInstance.transform.parent = transform;

            // Set the local position based on the random position
            heartInstance.transform.localPosition = randomPosition;

            // Ensure they render on top of everything
            heartInstance.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }

    // Generate a key to pull date card data from
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

    // Exit the game when 'Esc' is pressed
    void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            saveSystemScript.SaveData();
            Application.Quit();
        }
    }
}