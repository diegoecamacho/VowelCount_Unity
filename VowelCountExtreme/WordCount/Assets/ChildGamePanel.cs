#region

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Amazon;
using Amazon.Lambda;
using Amazon.CognitoIdentity;
using Amazon.Lambda.Model;
using System.Text;
using System.Net;
using System.IO;

public class ChildGamePanel : UIMenu
{
    [SerializeField] private float countDownTime = 5;
    
    [SerializeField] private List<string> phrasesList;
    
    [SerializeField] private int playerScore = 0;

    private bool runCountdown = false;

    private float timeElapsed = 0.0f;
    
    private int currentPhraseIndex = 0;
    
    private readonly char[] vowels = {'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'};

    
    [Header("Error Panel")] 
    [SerializeField] private ErrorFadePanel errorFadePanel;

    [Header("Controlled Menus")]
    [SerializeField] private UIMenu gameOverMenu;

    [Header("Debug")] public string activePhrase;
    public int vowelCount = 0;

     private TMP_InputField playerInputField;
     
    [Header("Component References")]

    [SerializeField] private TextMeshProUGUI playerScoreText;

    [SerializeField] private TextMeshProUGUI childNameText;

    [SerializeField] private TextMeshProUGUI activePhraseText;
    
    [SerializeField] private Transform playerInputSpawn;
    
    [SerializeField] private Image radialTimer;


    [Header("Prefabs")]
    [SerializeField] private GameObject playerInputPrefab;

    const string HTTTP_ADDRESS = "https://kh00ccm6r0.execute-api.us-east-2.amazonaws.com/default/CalculateVowels?phrase=#";




    public override void InitializeMenu()
    {
        var parentMenuParameters = GetParameters<ParentTurnParameter>();
        phrasesList = parentMenuParameters.ParentPhrases;

        childNameText.text = $"{parentMenuParameters.ChildName} Turn";

        playerScore = 0;

        playerInputField = Instantiate(playerInputPrefab, playerInputSpawn).GetComponentInChildren<TMP_InputField>();

        playerInputField.onEndEdit.AddListener(OnInputFinalized);

        GetNewPhraseFromList();

        runCountdown = true;
    }

    public override void DisableMenu()
    {
        Destroy(playerInputField.gameObject);
    }

    private int AWSCalculateVowels()
    {
       var client = new WebClient();
       
       var response = client.DownloadString(HTTTP_ADDRESS.Replace("#", $"{ activePhrase }"));
       
       var cleanResponse = ReadResponse(response);
       
       Debug.Log(cleanResponse);


       return cleanResponse;
    }


    public int ReadResponse(string response)
    {
        StringBuilder sb = new StringBuilder();
        foreach(var character in response)
        {
            if(int.TryParse(character.ToString() , out int result))
            {
                sb.Append(character);
            }
        }

        return int.Parse(sb.ToString());
    }


    private void GetNewPhraseFromList()
    {
        if (phrasesList.Count <= 0)
        {
            var gameOverParameters = new GameOverUIParameters(playerScore);
            UIController.LoadMenu(gameOverMenu, gameOverParameters);
            return;
        }
        
        currentPhraseIndex = Random.Range(0, phrasesList.Count);
        activePhrase = phrasesList[currentPhraseIndex];

        phrasesList.RemoveAt(currentPhraseIndex);

        activePhraseText.text = activePhrase;

        vowelCount = AWSCalculateVowels();


        timeElapsed = countDownTime;
    }

    private void Update()
    {
        if (!runCountdown) return;

        if (timeElapsed <= 0)
        {
            GetNewPhraseFromList();
        }

        timeElapsed -= Time.deltaTime;
        radialTimer.fillAmount = timeElapsed / countDownTime;
        playerScoreText.text = playerScore.ToString();
    }

    public void OnInputFinalized(string input)
    {
        if (int.Parse(input) == vowelCount)
        {
            print("Player Is Correct");
            playerScore++;
            
            GetNewPhraseFromList();
            ResetUIElements();
            
            playerInputField.text = "";
        }
        else
        {
            errorFadePanel.EnableError();
            playerInputField.text = "";
        }
        
    }

    private void ResetUIElements()
    {
        playerInputField.text = "";
    }
    public int CalculateVowel(string phrase)
    {
        var vowelNum = 0;
        foreach (var character in phrase)
        foreach (var vowel in vowels)
            if (character == vowel)
                vowelNum++;

        return vowelNum;
    }
 }
#endregion