using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using TMPro;
using System.Linq;

public class TextManager : MonoBehaviour
{
    private TextAsset[] textFiles;
    public List<string> lectureText = new List<string>();
    public List<string> demonstrationText = new List<string>();
    private string lectureName;
    private string demonstrationName;
    public int mode; //0 = Lecture, 1 = Demonstration
    private TMP_Text _textBox;
    public int lectureCount = 0;
    public int demonstrationCount = 0;
    void Start()
    {
        if (mode == 0)
        {
            _textBox = GetComponent<TMP_Text>();
            _textBox.text = lectureText[lectureCount];
        }
        else
        {
            _textBox = GetComponent<TMP_Text>();
            _textBox.text = demonstrationText[demonstrationCount];
        }

    }
    void Awake()
    {
        if(mode == 0)
        {
            if(GameManager.Instance.lectureMode == 0)
            {
                lectureName = "lightLecture";
            }
            else if(GameManager.Instance.lectureMode == 1)
            {
                lectureName = "electricityLecture";
            }

            // Get all text files in the specified directory
            textFiles = Resources.LoadAll<TextAsset>(lectureName);
            textFiles = textFiles.OrderBy(file => GetNumericValue(file.name)).ToArray();

            // Iterate over each text file
            foreach (var text in textFiles)
            {
                lectureText.Add(text.text);
                // Debug.Log(fileContents);
            }
        }
        else
        {
            if(GameManager.Instance.demonstrationMode == 0)
            {
                demonstrationName = "lightDemonstration";
            }
            else if(GameManager.Instance.demonstrationMode == 1)
            {
                demonstrationName = "electricityDemonstration";
            }

            // Get all text files in the specified directory
            textFiles = Resources.LoadAll<TextAsset>(demonstrationName);
            textFiles = textFiles.OrderBy(file => GetNumericValue(file.name)).ToArray();

            // Iterate over each text file
            foreach (var text in textFiles)
            {
                demonstrationText.Add(text.text);
                // Debug.Log(fileContents);
            }
        }
    }
    //Ensures that the files are read in numerical ascending order
    int GetNumericValue(string fileName)
    {
        string numericPart = new string(fileName.Where(char.IsDigit).ToArray());

        if (int.TryParse(numericPart, out int numericValue))
        {
            return numericValue;
        }
        return 0;
    }
}
