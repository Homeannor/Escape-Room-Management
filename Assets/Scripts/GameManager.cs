using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int cash = 0;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText;
    private float days;

    public Light sun;
    private float dayDuration = 60f;
    private float timeOfDay;

    private void Start()
    {
        Instance = this;
        
        days = 1f;
        cash = PlayerPrefs.GetInt("Cash", 750);
    }
    void Update()
    {
        updateUI();
        DayNightCycle();
    }

    void updateUI()
    {
        cashText.text = "CASH: " + cash;
        dayText.text = "DAY " + days;
        timeText.text = FormatTime();
    }

    //Temporary method to add cash for testing
    public void AddCash()
    {
        cash += 100;
        PlayerPrefs.SetInt("Cash", cash);
    }

    void DayNightCycle()
    {
        if (timeOfDay >= dayDuration)
        {
            timeOfDay = 0;
            days++;
        }

        timeOfDay += Time.deltaTime;

        float angle = (timeOfDay / dayDuration) * 360f;
        float t = Mathf.Sin(timeOfDay / dayDuration * Mathf.PI * 2f) * 0.5f * 0.5f;

        sun.transform.rotation = Quaternion.Euler(angle - 90, 170, 0f);
    }

    string FormatTime()
    {
        float normalTime = timeOfDay / dayDuration;
        float totalHours = normalTime * 24f;

        int hour24 = Mathf.FloorToInt(totalHours);
        int minutes = Mathf.FloorToInt((totalHours - hour24) * 60f);

        string ampm = hour24 >= 12 ? "PM" : "AM";

        int hour12 = hour24 % 12;
        if (hour12 == 0) hour12 = 12;

        string timeOfDayLabel;

        if (hour24 >= 5 && hour24 < 12)
            timeOfDayLabel = "MORNING";
        else if (hour24 >= 12 && hour24 < 17)
            timeOfDayLabel = "AFTERNOON";
        else if (hour24 >= 17 && hour24 < 21)
            timeOfDayLabel = "EVENING";
        else
            timeOfDayLabel = "NIGHT";

        return string.Format("{0}:{1:00} {2} [{3}]", 
            hour12, minutes, ampm, timeOfDayLabel);
    }
}