using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class CharacterSelectionController : UIMenu
{
    [Header("Settings")]
    [SerializeField] private float errorPanelDisplayTime;

    [Header("Component References")]
    [SerializeField] private ErrorPanel errorTextPanel;
    [SerializeField] private TMP_InputField parentInputField;
    [SerializeField] private TMP_InputField childInputField;

    public void LoadParentTurnMenu(UIMenu menu)
    {
        if (!string.IsNullOrEmpty(parentInputField.text) && !string.IsNullOrEmpty(childInputField.text))
        {
            var playerNames = new PlayerNamesUIParameters(parentInputField.text, childInputField.text);
            UIController.LoadMenu(menu, playerNames);
        }
        else
        {
            errorTextPanel.gameObject.SetActive(true);
            var errorMessage =   ErrorMessageGeneration();
            errorTextPanel.PanelText = errorMessage;
            StartCoroutine(DisableErrorWarningRoutine());
        }
    }

    /// <summary>
    /// Generates Error Message for the missing input fields.
    /// </summary>
    /// <returns></returns>
    private string ErrorMessageGeneration()
    {
        var stringBuilder = new StringBuilder();
        var errorCount = 0;
        
        if (string.IsNullOrEmpty(parentInputField.text))
        {
            stringBuilder.Append("Parent ");
            errorCount++;
        }
        
        if (string.IsNullOrEmpty(childInputField.text))
        {
            stringBuilder.Append(string.IsNullOrEmpty(stringBuilder.ToString()) ? "Child " : "and Child ");
            errorCount++;
        }

        var nameSpelling = errorCount > 1 ? "names are" : "name is";
        stringBuilder.Append($"{ nameSpelling } missing!");

        return stringBuilder.ToString();
    }

    public void ResetFields()
    {
        parentInputField.text = "";
        childInputField.text = "";
    }

    /// <summary>
    /// Disables the error message panel after a specified time.
    /// </summary>
    /// <returns></returns>
    IEnumerator DisableErrorWarningRoutine()
    {
        yield return new WaitForSeconds(errorPanelDisplayTime);
        
        errorTextPanel.gameObject.SetActive(false);
        
        
    }
}
