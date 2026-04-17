using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int cash = 0;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI openClosedText;
    private float days;
    public string isClosed;
    public Light sun;
    public float dayDuration;
    public float timeOfDay;

    public SavingManager savingManager;

    void Start()
    {
        instance = this;
        
        days = PlayerPrefs.GetFloat("Days", 1f);
        timeOfDay = PlayerPrefs.GetFloat("TimeOfDay", 20f);
        cash = PlayerPrefs.GetInt("Cash", 750);
        isClosed = PlayerPrefs.GetString("IsClosed", "True");

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            savingManager.LoadRoom(1);
        }
    }

    void Update()
    {
        updateUI();
        DayNightCycle();
    }

    void updateUI()
    {
        cashText.text = "CASH: £" + cash;
        dayText.text = "DAY " + days;
        timeText.text = FormatTime();

        if (isClosed == "False")
        {
            openClosedText.text = "OPEN";
            openClosedText.color = Color.green;
        }
        else
        {
            openClosedText.text = "CLOSED";
            openClosedText.color = Color.red;
        }
    }

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

            PlayerPrefs.SetFloat("Days", days);
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

        if (hour24 >= 6 && hour24 < 12)
        {
            timeOfDayLabel = "MORNING";
            isClosed = "False";
        }
        else if (hour24 >= 12 && hour24 < 17)
            timeOfDayLabel = "AFTERNOON";
        else if (hour24 >= 17 && hour24 < 21)
            timeOfDayLabel = "EVENING";
        else
        {
            timeOfDayLabel = "NIGHT";
            isClosed = "True";
        }

        PlayerPrefs.SetString("IsClosed", isClosed);
        PlayerPrefs.SetFloat("TimeOfDay", timeOfDay);

        return string.Format("{0}:{1:00} {2} [{3}]", 
            hour12, minutes, ampm, timeOfDayLabel);
    }
}