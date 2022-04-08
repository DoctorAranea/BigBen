using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAlarm : MonoBehaviour
{
    public static CheckAlarm instance;

    private bool flag = false;
    private AudioSource audioSource;
    private BigBenScript bbs;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        bbs = GetComponent<BigBenScript>();
    }

    void Update()
    {
        if (bbs.hours == PlayerPrefs.GetInt("alarmHour") && bbs.minutes == PlayerPrefs.GetInt("alarmMinute") && !flag)
        {
            flag = true;
            audioSource.Play();
        }
        if (bbs.hours != PlayerPrefs.GetInt("alarmHour") || bbs.minutes != PlayerPrefs.GetInt("alarmMinute") && flag)
            flag = false;
    }
}
