using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    public GameObject alarmHours;
    public GameObject alarmMinutes;

    private int bufHour;
    private int bufMinute;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        bufHour = PlayerPrefs.GetInt("alarmHour");    
        bufMinute = PlayerPrefs.GetInt("alarmMinute");
    }

    public void HourUp()
    {
        bufHour++;
        if (bufHour >= 24)
            bufHour = 0;
        alarmHours.GetComponent<TextMeshProUGUI>().text = string.Format("{0:d2}", bufHour);
    }

    public void HourDown()
    {
        bufHour--;
        if (bufHour < 0)
            bufHour = 23;
        alarmHours.GetComponent<TextMeshProUGUI>().text = string.Format("{0:d2}", bufHour);
    }

    public void MinuteUp()
    {
        bufMinute++;
        if (bufMinute >= 60)
            bufMinute = 0;
        alarmMinutes.GetComponent<TextMeshProUGUI>().text = string.Format("{0:d2}", bufMinute);
    }

    public void MinuteDown()
    {
        bufMinute--;
        if (bufMinute < 0)
            bufMinute = 59;
        alarmMinutes.GetComponent<TextMeshProUGUI>().text = string.Format("{0:d2}", bufMinute);
    }

    public void SetAlarm()
    {
        PlayerPrefs.SetInt("alarmHour", bufHour);
        PlayerPrefs.SetInt("alarmMinute", bufMinute);
    }
}
