using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
       
        StartCoroutine("DoFade");

    }

    IEnumerator DoFade()
    {
     
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime ;
            yield return null;
        }
       

    }
}
