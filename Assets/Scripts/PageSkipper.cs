using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageSkipper : MonoBehaviour
{
    public GameObject board;
    private TMP_Text _textBox;
    public GameObject button;
    private TextManager textManager;
    private void OnEnable()
    {
        TypewriterEffect.CompleteTextRevealed += ButtonReveal;
    }

    private void OnDisable()
    {
        TypewriterEffect.CompleteTextRevealed -= ButtonReveal;
    }
    private void Awake()
    {
        _textBox = board.GetComponent<TMP_Text>();
        textManager = board.GetComponent<TextManager>();
    }
    // private void SkipPage(char character)
    // {
    //     // string str = "\u200B";
    //     // foreach(char c in str){
    //     //     if(c == character){
    //     //         _textBox.pageToDisplay++;
    //     //     }
    //     // }
    //     char c = '_';
    //     if(c == character){
    //         _textBox.pageToDisplay++;
    //     }
    // }
    private void ButtonReveal()
    {
        if(!(textManager.count >= textManager.lectureText.Count))
        {
            button.SetActive(true);
        }
    }
    public void ButtonClick()
    {
        if(!(textManager.count >= textManager.lectureText.Count))
        {
            _textBox.text = textManager.lectureText[textManager.count];
            textManager.count++;
            button.SetActive(false);
        }
    }
}
