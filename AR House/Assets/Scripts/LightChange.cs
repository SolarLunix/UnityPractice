using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public Color[] colors;

    public int currentIndex = 0;
    private int nextIndex;

    public float changeColourTime = 2.0f;

    private float lastChange = 0.0f;
    private float timer = 0.0f;

    Light lt;
    Renderer parent;

    void Start()
    {

        lt = gameObject.GetComponent<Light>();
        parent = GetComponentInParent<Renderer>();

        if (colors == null || colors.Length < 2)
            Debug.Log("Need to setup colors array in inspector");

        nextIndex = (currentIndex + 1) % colors.Length;
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer > changeColourTime)
        {
            currentIndex = (currentIndex + 1) % colors.Length;
            nextIndex = (currentIndex + 1) % colors.Length;
            timer = 0.0f;
        }

        SwitchColour();

    }

    void SwitchColour()
    {
        Color currentColour = lt.color;
        parent.material.color = Color.Lerp(currentColour, colors[nextIndex], timer / changeColourTime);
        lt.color = Color.Lerp(currentColour, colors[nextIndex], timer / changeColourTime);
    }
}
