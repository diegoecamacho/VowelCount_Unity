#region

using System.Collections.Generic;
using TMPro;
using UnityEngine;

#endregion

public class PhrasePanelScript : MonoBehaviour
{
    [Header("Settings")] [SerializeField] private GameObject ParentInputFieldPrefab;

    private readonly List<TMP_InputField> inputFields = new List<TMP_InputField>();

    private void Start()
    {
        AddNewInputField();
    }

    public List<string> GetAllPhrases()
    {
        var stringList = new List<string>();
        foreach (var field in inputFields)
            if (!string.IsNullOrEmpty(field.text))
                stringList.Add(field.text);

        return stringList;
    }
    
    public void AddNewInputField()
    {
        var inputField = Instantiate(ParentInputFieldPrefab, transform.GetChild(0)).GetComponent<TMP_InputField>();

        if (inputField)
            inputFields.Add(inputField);
        else
            Debug.LogWarning("Prefab does not contain TMP_InputField");
    }

    public void ResetFields()
    {
        inputFields.Clear();
        for (int i = transform.GetChild(0).childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(0).transform.GetChild(i);
            Destroy(child.gameObject);
        }
        
        AddNewInputField();
    }
}