#region

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class GameOverUIParameters : UIParameters
{
    public int score = 0;

    public GameOverUIParameters(int score)
    {
        this.score = score;
    }
}

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

    [Header("Component References")]

    [SerializeField] private TextMeshProUGUI playerScoreText;

    [SerializeField] private TextMeshProUGUI childNameText;

    [SerializeField] private TextMeshProUGUI activePhraseText;
    
    [SerializeField] private TMP_InputField playerInputField;

    [SerializeField] private Image radialTimer;
   


    public override void InitializeMenu()
    {
        var parentMenuParameters = GetParameters<ParentTurnParameter>();
        phrasesList = parentMenuParameters.ParentPhrases;

        childNameText.text = $"{parentMenuParameters.ChildName} Turn";

        playerScore = 0;

        playerInputField.onEndEdit.AddListener(OnInputFinalized);

        GetNewPhraseFromList();

        runCountdown = true;
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
        vowelCount = CalculateVowel(activePhrase);

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
            print($"Player Is Correct");
            playerScore++;
            
            GetNewPhraseFromList();
            ResetUIElements();
        }
        else
        {
            errorFadePanel.EnableError();
        }
        
    }

    private void ResetUIElements()
    {
        playerInputField.text = "";
    }

    //Todo: Move to AWS
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