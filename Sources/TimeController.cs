using Oculus.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private Button plusButton;
    [SerializeField] private Button minusButton;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private float minTime = 1f;
    [SerializeField] private float maxTime = 60f;
    public static float currentTime = 5;
    private float timeIncrement = 5f;

    private void Start()
    {
        currentTime = float.Parse(timeText.text);
        UpdateTimeText();
        CheckTimeLimits();

        plusButton.onClick.AddListener(OnPlusButtonPress);
        minusButton.onClick.AddListener(OnMinusButtonPress);
    }

    private void OnPlusButtonPress()
    {
        currentTime += timeIncrement;
        if (currentTime > maxTime) currentTime = maxTime;
        UpdateTimeText();
        CheckTimeLimits();
        Debug.Log(currentTime);
    }

    private void OnMinusButtonPress()
    {
        currentTime -= timeIncrement;
        if (currentTime < minTime) currentTime = minTime;
        UpdateTimeText();
        CheckTimeLimits();
        Debug.Log(currentTime);
    }

    private void UpdateTimeText()
    {
        timeText.text = currentTime.ToString();
    }

    public void CheckTimeLimits()
    {
        currentTime = float.Parse(timeText.text);
        minusButton.interactable = currentTime > minTime;
        plusButton.interactable = currentTime < maxTime;
    }
}
