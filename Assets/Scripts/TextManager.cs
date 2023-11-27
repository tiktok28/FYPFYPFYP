using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using TMPro;
using System.Linq;

public class TextManager : MonoBehaviour
{
    public List<string> lectureText = new List<string>();
    public string lectureName;
    private TMP_Text _textBox;
    public int count = 0;
    void Start()
    {
        _textBox = GetComponent<TMP_Text>();
        _textBox.text = lectureText[count];
        count++;
    }
    void Awake()
    {
        if(GameManager.Instance.lectureMode == 0)
        {
            lectureName = "lightLecture";
        }
        else if(GameManager.Instance.lectureMode == 1)
        {
            lectureName = "electricityLecture";
        }
        // Directory path where your text files are located
        string directoryPath = "Assets/Resources/" + lectureName;

        // Get all text files in the specified directory
        string[] textFiles = Directory.GetFiles(directoryPath, "*.txt").OrderBy(f =>int.Parse(Path.GetFileNameWithoutExtension(f))).ToArray();

        // Iterate over each text file
        foreach (string filePath in textFiles)
        {
            // Read the contents of the file
            string fileContents = File.ReadAllText(filePath);

            lectureText.Add(fileContents);
            // Debug.Log(filePath);

        }
    }
}
