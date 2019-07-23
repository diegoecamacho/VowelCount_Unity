using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParentGamePanelController : UIMenu
{
    [Header("Component References")]
    [SerializeField] private TextMeshProUGUI parentNameText;
    [SerializeField] private PhrasePanelScript panelScript;

    private PlayerNamesUIParameters namesUIParameters;

    public override void InitializeMenu()
    {
        namesUIParameters = GetParameters<PlayerNamesUIParameters>();

        parentNameText.text = $"{ namesUIParameters.ParentName } Turn";
    }

    public void LoadChildTurnMenu(UIMenu menu)
    {
        var phrases = panelScript.GetAllPhrases();

        if (phrases.Count == 0)
        {
            print("Please enter more phrases.");
        }
        else
        {
            var parameters = new ParentTurnParameter(phrases, namesUIParameters.ChildName);
            UIController.LoadMenu(menu, parameters);
        }
    }

    public void ResetFields()
    {
        panelScript.ResetFields();
    }
    
    
}
