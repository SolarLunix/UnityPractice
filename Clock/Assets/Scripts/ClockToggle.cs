using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockToggle : MonoBehaviour
{
    public GameObject digital;
    public GameObject standard;

    bool dig;
    bool std;

    void Start()
    {
        dig = digital.active;
        std = standard.active;
    }

    public void SwitchClocks()
    {
        dig = !dig;
        std = !std;

        digital.active = dig;
        standard.active = std;
    }
}
