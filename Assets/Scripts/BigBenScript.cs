using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.IO;
using UnityEngine.Networking;
using TMPro;

public class BigBenScript : MonoBehaviour
{
    public static BigBenScript instance;

    public int hours;
    public int minutes;
    public int seconds;

    public GameObject hourArrow;
    public GameObject minuteArrow;
    public GameObject secondArrow;

    public GameObject textTime;

    public GameObject loadingScreen;
    public GameObject alarmPanel;

    [SerializeField] private string url;
    [SerializeField] private string url2;

    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("alarmHour"))
            PlayerPrefs.SetInt("alarmHour", 12);
        if (!PlayerPrefs.HasKey("alarmMinute"))
            PlayerPrefs.SetInt("alarmMinute", 0);
    }

    void Start()
    {
        StartCoroutine(SendRequest());
        StartCoroutine(OneSecond());
    }
    
    private IEnumerator OneSecond()
    {
        while (true)
        {
            seconds++;
            if (seconds > 59)
            {
                seconds = 0;
                minutes++;
            }
            if (minutes > 59)
            {
                minutes = 0;
                hours++;
            }
            if (hours >= 24)
            {
                hours = 0;
            }
            yield return new WaitForSecondsRealtime(1);
        }
    }

    private IEnumerator SendRequest()
    {
        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Get(this.url);
            yield return request.SendWebRequest();
            TimeStruct timeStruct = JsonUtility.FromJson<TimeStruct>(request.downloadHandler.text);

            int fHours = int.Parse($"{timeStruct.datetime[11]}{timeStruct.datetime[12]}");
            int fMinutes = int.Parse($"{timeStruct.datetime[14]}{timeStruct.datetime[15]}");
            int fSeconds = int.Parse($"{timeStruct.datetime[17]}{timeStruct.datetime[18]}");

            request = UnityWebRequest.Get(this.url2);
            yield return request.SendWebRequest();
            TimeStruct2 timeStruct2 = JsonUtility.FromJson<TimeStruct2>(request.downloadHandler.text);

            hours = (timeStruct2.hour + fHours) / 2;
            minutes = (timeStruct2.minute + fMinutes) / 2;
            seconds = fSeconds;

            loadingScreen.GetComponent<Animator>().SetTrigger("Loaded");
            Debug.Log($"\nПервый запрос: {fHours}:{fMinutes}:{fSeconds}\nВторой запрос: {timeStruct2.hour}:{timeStruct2.minute}");
            yield return new WaitForSecondsRealtime(60 * 60);
        }
    }

    void Update()
    {        
        hourArrow.transform.rotation = Quaternion.Euler(0, 0, 360 / 12 * (float)hours * -1);
        minuteArrow.transform.rotation = Quaternion.Euler(0, 0, 360 / 60 * (float)minutes * -1);
        secondArrow.transform.rotation = Quaternion.Euler(0, 0, 360 / 60 * (float)seconds * -1);
        string bufHours = string.Format("{0:d2}", hours);
        string bufMinutes = string.Format("{0:d2}", minutes);
        string bufSeconds = string.Format("{0:d2}", seconds);
        textTime.GetComponent<TextMeshProUGUI>().text = $"{bufHours}:{bufMinutes}:{bufSeconds}";
    }

    public void EnableAlarmPanel()
    {
        alarmPanel.SetActive(true);
    }

    public void DisableAlarmPanel()
    {
        alarmPanel.SetActive(false);
    }
}
