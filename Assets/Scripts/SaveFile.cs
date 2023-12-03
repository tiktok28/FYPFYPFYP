using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveFile
{
    public string playerName;
    public List<GameObject> gameObjects = new List<GameObject>();
    public float timePlayed;
    public float timerTimeElapsed;
    public float timerTimeLeft;
    public string incident;
    public string refracted;
    public int score; 
}
