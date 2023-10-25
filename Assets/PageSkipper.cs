using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageSkipper : MonoBehaviour
{
    private TMP_Text _textBox;
    private void OnEnable()
    {
        TypewriterEffect.CharacterRevealed += SkipPage;
    }

    private void OnDisable()
    {
        TypewriterEffect.CharacterRevealed -= SkipPage;
    }
    private void Awake()
    {
        _textBox = GetComponent<TMP_Text>();
    }
    private void SkipPage(char character)
    {
        string str = "\u200B";
        foreach(char c in str){
            if(c == character){
                _textBox.pageToDisplay++;
            }
        }
    }
}
