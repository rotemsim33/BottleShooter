using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    private Text flashingText;
    void Start()
    {
        flashingText = GetComponent<Text>();
    }
    void Update()
    {
        flashingText.color = new Color(flashingText.color.r, flashingText.color.g, flashingText.color.b, Mathf.Sin(Time.time * 6));
    }

}
