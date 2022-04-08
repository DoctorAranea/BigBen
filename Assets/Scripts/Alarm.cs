using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Alarm : MonoBehaviour
{
    public static Alarm instance;

    public GameObject hours;
    public GameObject minutes;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hours.GetComponent<TextMeshProUGUI>().text = string.Format("{0:d2}", PlayerPrefs.GetInt("alarmHour"));
        minutes.GetComponent<TextMeshProUGUI>().text = string.Format("{0:d2}", PlayerPrefs.GetInt("alarmMinute"));
    }
}
