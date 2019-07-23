using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all UI elements.
/// </summary>
public abstract class UIMenu : MonoBehaviour
{
    public UIParameters parameters { get; set; }
    public virtual void InitializeMenu(){ }

    public virtual void DisableMenu(){ }

    public T GetParameters<T>() where T : UIParameters
    {
        return (T) parameters;
    }
}

public abstract class UIParameters
{
    
}

public class ParentTurnParameter : UIParameters
{
    public string ChildName;
    
    public List<string> ParentPhrases;

    public ParentTurnParameter(List<string> parentPhrases, string childName)
    {
        ParentPhrases = parentPhrases;
        ChildName = childName;
    }
}

