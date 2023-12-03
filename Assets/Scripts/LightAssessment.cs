using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using System;
using UnityEngine.Experimental.AI;

public class LightAssessment : MonoBehaviour
{
    public Timer timer;
    public GameObject BoardText;
    public GameObject BoardCanvas;
    public GameObject ResultsCanvas;
    public GameObject EndCanvas;
    private GameObject start;
    private GameObject end;
    private GameObject results;
    private GameObject reset;
    private Button back;
    private TMP_InputField incident;
    private TMP_InputField refracted;
    private TMP_InputField sini;
    private TMP_InputField sinr;
    private TMP_InputField refractionIndex;
    private TMP_Text YourAnswerValue;
    private TMP_Text CorrectAnswerValue;
    private TMP_Text PercentageValue;
    private TMP_Text Rating;
    private TMP_Text TimeTakenValue;
    private GameObject laser;
    private GameObject prism;
    private GameObject measuringTool;
    public List<GameObject> gameObjects = new List<GameObject>();
    public bool loadSaved = false;
    public float generatedIndex1 = (float)Math.Round(GetPseudoDoubleWithinRange(1, 2), 2);
    // public float generatedIndex2 = (float)Math.Round(GetPseudoDoubleWithinRange(1, 2), 2);
    private void Awake()
    {
        start = BoardCanvas.transform.Find("Start").gameObject;
        end = BoardCanvas.transform.Find("End").gameObject;
        results = BoardCanvas.transform.Find("Results").gameObject;
        reset = BoardCanvas.transform.Find("Reset").gameObject;
        back = ResultsCanvas.transform.Find("Panel").transform.Find("Back").gameObject.GetComponent<Button>();
        incident = ResultsCanvas.transform.Find("Panel").transform.Find("InputFields").transform.Find("Incident").gameObject.GetComponent<TMP_InputField>();
        refracted = ResultsCanvas.transform.Find("Panel").transform.Find("InputFields").transform.Find("Refracted").gameObject.GetComponent<TMP_InputField>();
        sini = ResultsCanvas.transform.Find("Panel").transform.Find("InputFields").transform.Find("SinI").gameObject.GetComponent<TMP_InputField>();
        sinr = ResultsCanvas.transform.Find("Panel").transform.Find("InputFields").transform.Find("SinR").gameObject.GetComponent<TMP_InputField>();
        refractionIndex = ResultsCanvas.transform.Find("Panel").transform.Find("InputFields").transform.Find("RefractionIndex").gameObject.GetComponent<TMP_InputField>();
        YourAnswerValue = EndCanvas.transform.Find("Panel").transform.Find("Text").transform.Find("YourAnswerValue").gameObject.GetComponent<TMP_Text>();
        CorrectAnswerValue = EndCanvas.transform.Find("Panel").transform.Find("Text").transform.Find("CorrectAnswerValue").gameObject.GetComponent<TMP_Text>();
        PercentageValue = EndCanvas.transform.Find("Panel").transform.Find("Text").transform.Find("PercentageValue").gameObject.GetComponent<TMP_Text>();
        Rating = EndCanvas.transform.Find("Panel").transform.Find("Text").transform.Find("Rating").gameObject.GetComponent<TMP_Text>();
        TimeTakenValue = EndCanvas.transform.Find("Panel").transform.Find("Text").transform.Find("TimeTakenValue").gameObject.GetComponent<TMP_Text>();
        laser = gameObject.transform.Find("LaserPointer").gameObject;
        prism = gameObject.transform.Find("Prism").gameObject;
        measuringTool = gameObject.transform.Find("Measuring Tool").gameObject;

        start.GetComponent<Button>().onClick.AddListener(StartAssessment);
        end.GetComponent<Button>().onClick.AddListener(EndAssessment);
        results.GetComponent<Button>().onClick.AddListener(ShowResults);
        reset.GetComponent<Button>().onClick.AddListener(ResetPosition);
        back.onClick.AddListener(CloseResults);
    }
    private void StartAssessment()
    {
        timer.StartTimer();
        BoardText.SetActive(false);
        results.SetActive(true);
        end.SetActive(true);
        reset.SetActive(true);

        if(loadSaved)
        {
            prism.SetActive(true);
        }
        prism.GetComponent<PrismVariables>().refractiveIndex = generatedIndex1;

        gameObjects.Add(Instantiate(laser));
        gameObjects.Add(Instantiate(prism));
        gameObjects.Add(Instantiate(measuringTool));
        
        laser.SetActive(true);
        prism.SetActive(true);
        measuringTool.SetActive(true);

        start.SetActive(false);
    }
    private void EndAssessment()
    {
        try
        {
            double givenRefractiveIndex = double.Parse(refractionIndex.text);
            double percentageError = Math.Round(Math.Abs((givenRefractiveIndex - generatedIndex1)/generatedIndex1) * 100);
            double closeness = 100 - percentageError;
            timer.StopTimer();
            EndCanvas.GetComponent<Canvas>().enabled = true;
            YourAnswerValue.text = givenRefractiveIndex.ToString();
            CorrectAnswerValue.text = generatedIndex1.ToString();
            PercentageValue.text = closeness.ToString() + "%";
            Rating.text = ratingCalculator(closeness);
            float minutes = Mathf.FloorToInt(timer.timeElapsed/ 60);  
            float seconds = Mathf.FloorToInt(timer.timeElapsed % 60);
            TimeTakenValue.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            Debug.Log("Experiment Ended");
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }
    private void ShowResults()
    {
        DisableButton(end);
        DisableButton(reset);
        ResultsCanvas.GetComponent<Canvas>().enabled = true;
        DisableButton(results);
    }
    private void CloseResults()
    {
        EnableButton(end);
        EnableButton(results);
        EnableButton(reset);
        ResultsCanvas.GetComponent<Canvas>().enabled = false;
    }
    private void ResetPosition()
    {
        laser.GetComponent<Transform>().localPosition = gameObjects[0].GetComponent<Transform>().localPosition;
        laser.GetComponent<Rigidbody>().velocity = Vector3.zero;
        prism.GetComponent<Transform>().localPosition = gameObjects[1].GetComponent<Transform>().localPosition;
        prism.GetComponent<Rigidbody>().velocity = Vector3.zero;
        measuringTool.GetComponent<Transform>().localPosition = gameObjects[2].GetComponent<Transform>().localPosition;
        measuringTool.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    private void DisableButton(GameObject button)
    {
        button.GetComponent<Button>().interactable = false;
        button.GetComponent<Image>().enabled = false;
        button.transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().enabled = false;
    }
    private void EnableButton(GameObject button)
    {
        button.GetComponent<Button>().interactable = true;
        button.GetComponent<Image>().enabled = true;
        button.transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().enabled = true;
    }

    void Update()
    {
        try
        {
            sini.text = Math.Sin(double.Parse(incident.text)*Math.PI/180).ToString();
            sinr.text = Math.Sin(double.Parse(refracted.text)*Math.PI/180).ToString();
            refractionIndex.text = Math.Round(double.Parse(sini.text)/double.Parse(sinr.text), 2).ToString();
        }
        catch(Exception e)
        {

        }
    }
    public static double GetPseudoDoubleWithinRange(double lowerBound, double upperBound)
    {
        var random = new System.Random();
        var rDouble = random.NextDouble();
        var rRangeDouble = rDouble * (upperBound - lowerBound) + lowerBound;
        return rRangeDouble;
    }

    public string ratingCalculator(double percentage)
    {
        if (percentage >= 90 && percentage <= 100)
        {
            return "A+";
        }
        else if (percentage >= 80 && percentage < 90)
        {
            return "A";
        }
        else if (percentage >= 70 && percentage < 80)
        {
            return "B";
        }
        else if (percentage >= 60 && percentage < 70)
        {
            return "C";
        }
        else if (percentage >= 50 && percentage < 60)
        {
            return "D";
        }
        else if (percentage >= 0 && percentage < 50)
        {
            return "F";
        }
        else
        {
            return "Invalid Percentage";
        }
    }
}
