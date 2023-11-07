using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using TMPro;
using Object = UnityEngine.Object;
[RequireComponent(typeof(TMP_Text))]
public class TypewriterEffect : MonoBehaviour
{
    private TMP_Text _textBox;

    // Basic Typewriter Functionality
    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;
    private bool _readyForNewText = true;

    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;

    [Header("Typewriter Settings")] 
    [SerializeField] private float charactersPerSecond = 20;
    [SerializeField] private float interpunctuationDelay = 0.5f;


    // Skipping Functionality
    public bool CurrentlySkipping { get; private set; }
    private WaitForSeconds _skipDelay;

    [Header("Skip options")] 
    [SerializeField] private bool quickSkip;
    [SerializeField] [Min(1)] private int skipSpeedup = 5;


    // Event Functionality
    private WaitForSeconds _textboxFullEventDelay;
    [SerializeField] [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f; // In testing, I found 0.25 to be a good value

    public static event Action CompleteTextRevealed;
    public static event Action<char> CharacterRevealed;


    private void Awake()
    {
        
        _textBox = GetComponent<TMP_Text>();
        if(GameManager.Instance.lectureMode == 0){
            try
            {
                String filePath = "Assets/Resources/lightLecture.txt";
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    // Read the entire contents of the text file into a string
                    string filetext = reader.ReadToEnd();
                    _textBox.text = filetext;
                }
            }
            catch (Exception e)
            {
                Debug.Log("An error occurred: " + e.Message);
            }
        }
        else{
            try
                {
                    String filePath = "Assets/Resources/electricityLecture.txt";
                    using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                    {
                        // Read the entire contents of the text file into a string
                        string filetext = reader.ReadToEnd();
                        _textBox.text = filetext;
                    }
                }
            catch (Exception e)
            {
                Debug.Log("An error occurred: " + e.Message);
            }
        }

        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
        
        _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
        _textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);
    }
    
    private void OnEnable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(PrepareForNewText);
    }

    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(PrepareForNewText);
    }

    #region Skipfunctionality
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_textBox.maxVisibleCharacters != _textBox.textInfo.characterCount - 1)
                Skip();
        }
    }

    // Example for how to implement it in the New Input system
    // You'd have to use a PlayerController component on this gameobject and write the function's name as On[Input Action name] for this to work.
    // In this case, my Input Action is called "RightMouseClick". But: There are a ton of ways to implement checking if a button
    // has been pressed in this system. Go watch Piti's video on the different ways of utilizing the new input system: https://www.youtube.com/watch?v=Wo6TarvTL5Q
    
        // private void OnRightMouseClick()
        // {
        //     if (_textBox.maxVisibleCharacters != _textBox.textInfo.characterCount - 1)
        //         Skip();
        // }
        #endregion

        private void PrepareForNewText(Object obj)
        {
            // Debug.Log("PrepareForNewText");
            // Debug.Log(this.gameObject);
            // Debug.Log(obj);
            // // if (obj != _textBox || !_readyForNewText || _textBox.maxVisibleCharacters >= _textBox.textInfo.characterCount){
            // //     return;
            // // }
            // if(obj != _textBox){
            //     Debug.Log("Textbox");
            //     return;
            // }
            // if(!_readyForNewText){
            //     Debug.Log("ReadyForNewTest");
            //     return;
            // }
            // if(_textBox.maxVisibleCharacters >= _textBox.textInfo.characterCount){
            //     Debug.Log(_textBox.maxVisibleCharacters);
            //     Debug.Log(_textBox.textInfo.character);
            //     Debug.Log("characters");
            //     return;
            // }
            if (!_readyForNewText){
                return;
            }

            // CurrentlySkipping = false;
            _readyForNewText = false;

            if (_typewriterCoroutine != null){
                StopCoroutine(_typewriterCoroutine);
            }

            _textBox.maxVisibleCharacters = 0;
            _currentVisibleCharacterIndex = 0;

            _typewriterCoroutine = StartCoroutine(Typewriter());
        }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = _textBox.textInfo;

        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            var lastCharacterIndex = textInfo.characterCount - 1;

            if (_currentVisibleCharacterIndex >= lastCharacterIndex)
            {
                _textBox.maxVisibleCharacters++;
                yield return _textboxFullEventDelay;
                CompleteTextRevealed?.Invoke();
                _readyForNewText = true;
                yield break;
            }

            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;

            _textBox.maxVisibleCharacters++;
            
            if (!CurrentlySkipping &&
                (character == '?' || character == '.' || character == ',' || character == ':' ||
                    character == ';' || character == '!' || character == '-'))
            {
                yield return _interpunctuationDelay;
            }
            else
            {
                yield return CurrentlySkipping ? _skipDelay : _simpleDelay;
            }
            
            CharacterRevealed?.Invoke(character);
            _currentVisibleCharacterIndex++;
        }
    }

    private void Skip(bool quickSkipNeeded = false)
    {
        if (CurrentlySkipping)
            return;
        
        CurrentlySkipping = true;

        if (!quickSkip || !quickSkipNeeded)
        {
            StartCoroutine(SkipSpeedupReset());
            return;
        }

        StopCoroutine(_typewriterCoroutine);
        _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
        _readyForNewText = true;
        CompleteTextRevealed?.Invoke();
    }

    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => _textBox.maxVisibleCharacters == _textBox.textInfo.characterCount - 1);
        CurrentlySkipping = false;
    }
}