using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StandardClock : MonoBehaviour
{
    public Transform hrsTrans, minTrans, secTrans;

    const float degressPerHour = 30f;
    const float degressPerMin = 6f;
    const float degressPerSec = 6f;

    public bool dis = false;

    private void Update()
    {
        if (dis)
        {
            UpdateDis();
        }
        else
        {
            UpdateCon();
        }
    }

    void UpdateCon()
    {
        TimeSpan timeNow = DateTime.Now.TimeOfDay;
        hrsTrans.localRotation = Quaternion.Euler(0f, (float)timeNow.TotalHours * degressPerHour, 0f);
        minTrans.localRotation = Quaternion.Euler(0f, (float)timeNow.TotalMinutes * degressPerMin, 0f);
        secTrans.localRotation = Quaternion.Euler(0f, (float)timeNow.TotalSeconds * degressPerSec, 0f);
    }

    void UpdateDis()
    {
        DateTime timeNow = DateTime.Now;
        hrsTrans.localRotation = Quaternion.Euler(0f, (timeNow.Hour + (timeNow.Minute / 60f)) * degressPerHour, 0f);
        minTrans.localRotation = Quaternion.Euler(0f, timeNow.Minute * degressPerMin, 0f);
        secTrans.localRotation = Quaternion.Euler(0f, timeNow.Second * degressPerSec, 0f);
    }
}
