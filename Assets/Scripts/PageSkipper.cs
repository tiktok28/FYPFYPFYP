using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PageSkipper : MonoBehaviour
{
    public GameObject board;
    private TMP_Text _textBox;
    public GameObject button;
    private TextManager textManager;
    public static event Action FullTextCompleted;
    private void OnEnable()
    {
        TypewriterEffect.CompleteTextRevealed += ButtonReveal;
    }

    private void OnDisable()
    {
        TypewriterEffect.CompleteTextRevealed -= ButtonReveal;
    }
    private void Start()
    {
        _textBox = board.GetComponent<TMP_Text>();
        textManager = board.GetComponent<TextManager>();
    }
    private void ButtonReveal()
    {
        if(textManager.mode == 0)
        {
            if(textManager.lectureCount < textManager.lectureText.Count - 1)
            {
                button.SetActive(true);
            }
            else if(textManager.lectureCount == textManager.lectureText.Count - 1)
            {
                FullTextCompleted?.Invoke();
            }
        }
        else if(textManager.mode == 1)
        {
            if(textManager.demonstrationCount < textManager.demonstrationText.Count - 1)
            {
                button.SetActive(true);
            }    
            else if(textManager.demonstrationCount == textManager.demonstrationText.Count - 1)
            {
                FullTextCompleted?.Invoke();
            }
        }

    }
    public void ButtonClick()
    {
        if(textManager.mode == 0)
        {
            if(textManager.lectureCount < textManager.lectureText.Count - 1)
            {
                textManager.lectureCount++;
                _textBox.text = textManager.lectureText[textManager.lectureCount];
                button.SetActive(false);
            }
        }
        else if(textManager.mode == 1)
        {
            if(textManager.demonstrationCount < textManager.demonstrationText.Count - 1)
            {
                textManager.demonstrationCount++;
                _textBox.text = textManager.demonstrationText[textManager.demonstrationCount];
                button.SetActive(false);
            }    
        }
    }
}
