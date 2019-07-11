using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DigitalTime : MonoBehaviour
{
    public GameObject hrs10s;
    public GameObject hrs1s;
    public GameObject min10s;
    public GameObject min1s;

    // Update is called once per frame
    void Update()
    {
        int hrs10 = 0;
        int hrs1 = 0;
        int min10 = 0;
        int min1 = 0;

        DateTime myTime = DateTime.Now;
        hrs1 = myTime.Hour % 10;
        hrs10 = (myTime.Hour - hrs1) / 10;
        min1 = myTime.Minute % 10;
        min10 = (myTime.Minute - min1) / 10;

        changeNum(hrs10s, hrs10);
        changeNum(hrs1s, hrs1);
        changeNum(min10s, min10);
        changeNum(min1s, min1);
    }

    void changeNum(GameObject pos, int num)
    {
        List<int> nums = getPos(num);
        foreach (Transform child in pos.transform)
        {
            int dotNum = int.Parse(child.name.Split('(')[1].Split(')')[0]);
            child.gameObject.active = nums.Contains(dotNum);
        }
    }

    List<int> getPos(int num)
    {
        List<int> pos;
        switch (num)
        {
            case 0:
                pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                break;
            case 1:
                pos = new List<int> { 6, 13, 20, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                break;
            case 2:
                pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35 };
                break;
            case 3:
                pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                break;
            case 4:
                pos = new List<int> { 0, 6, 7, 8, 9, 10, 11, 12, 13, 20, 21, 22, 23, 24, 25, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                break;
            case 5:
                pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 36, 37, 38, 39, 40 };
                break;
            case 6:
                pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 36, 37, 38, 39, 40 };
                break;
            case 7:
                pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 13, 20, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                break;
            case 9:
                pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                break;
            default:
                pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                break;
        }
        return pos;
    }
}
