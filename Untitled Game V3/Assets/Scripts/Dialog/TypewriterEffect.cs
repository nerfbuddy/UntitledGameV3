﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typerwriterspeed = 50f;
    public Coroutine Run(string textToType, TMP_Text textLable)
    {
        return StartCoroutine(TypeText(textToType, textLable));
    }
    private IEnumerator TypeText(string textToType, TMP_Text textLable)
    {
        float t = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length)
        {
            t += Time.deltaTime * typerwriterspeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLable.text = textToType.Substring(0, charIndex);

            yield return null;
        }
        textLable.text = textToType;
    }
}
