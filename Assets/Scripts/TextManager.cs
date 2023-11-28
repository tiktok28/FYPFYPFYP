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
    public List<string> demonstrationText = new List<string>();
    private string lectureName;
    private string demonstrationName;
    public int mode = 0; //0 = Lecture, 1 = Demonstration
    private TMP_Text _textBox;
    public int count = 0;
    void Start()
    {
        if (mode == 0)
        {
            _textBox = GetComponent<TMP_Text>();
            _textBox.text = lectureText[count];
        }
        else
        {
            _textBox = GetComponent<TMP_Text>();
            _textBox.text = demonstrationText[count];
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
            // Directory path where your text files are located
            string directoryPath = "Assets/Resources/" + demonstrationName;

            // Get all text files in the specified directory
            string[] textFiles = Directory.GetFiles(directoryPath, "*.txt").OrderBy(f =>int.Parse(Path.GetFileNameWithoutExtension(f))).ToArray();

            // Iterate over each text file
            foreach (string filePath in textFiles)
            {
                // Read the contents of the file
                string fileContents = File.ReadAllText(filePath);

                demonstrationText.Add(fileContents);
                // Debug.Log(filePath);
            }
        }
    }
}
