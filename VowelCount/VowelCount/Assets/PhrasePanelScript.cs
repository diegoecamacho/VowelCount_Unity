#region

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#endregion

public class PhrasePanelScript : MonoBehaviour
{
    [SerializeField] private int maxPhraseCount;

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
        if (transform.GetChild(0).childCount > maxPhraseCount) return;

        var inputField = Instantiate(ParentInputFieldPrefab, transform.GetChild(0)).GetComponent<TMP_InputField>();

        if (inputField)
            inputFields.Add(inputField);
        else
            Debug.LogWarning("Prefab does not contain TMP_InputField");
    }

    public void ResetFields()
    {
        StartCoroutine(ResetFieldsCoroutine());
    }

    IEnumerator ResetFieldsCoroutine()
    {
        inputFields.Clear();
        var panelTransform = transform.GetChild(0);
        foreach (Transform child in panelTransform) Destroy(child.gameObject);

        yield return new WaitForSeconds(0.05f);
        
        AddNewInputField();

        yield return null;
    }
}