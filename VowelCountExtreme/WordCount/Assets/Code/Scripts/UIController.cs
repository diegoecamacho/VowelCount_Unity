#region

using Amazon;
using UnityEngine;

#endregion

/// <summary>
/// Controls all child UI's in the scene.
/// Singleton
/// </summary>
public class UIController : Singleton<UIController>
{
    public static UIMenu ActiveMenu { get; private set; }
    
    [SerializeField] private UIMenu startingMenu;

    private void Start()
    {
        UnityInitializer.AttachToGameObject(gameObject);
        //Initializing Singleton from script.
        Instance = this;
        
        var childCount = transform.childCount;
        for (var childIndex = 0; childIndex < childCount; childIndex++)
            transform.GetChild(childIndex).gameObject.SetActive(false);

        LoadMenu(startingMenu);
    }

    public void LoadMenu(UIMenu menu)
    {
        LoadMenu(menu,null);
    }

    /// <summary>
    /// Loads Next UI Menu
    /// </summary>
    /// <param name="menu"></param>
    public static void LoadMenu(UIMenu menu, UIParameters parameters = null)
    {
        if (menu == null)
        {
            Debug.LogWarning("No UI Menu to load!");
            return;
        }

        if (ActiveMenu != null)
        {
            ActiveMenu.DisableMenu();
            ActiveMenu.gameObject.SetActive(false);
        }
        
        ActiveMenu = menu;
        ActiveMenu.gameObject.SetActive(true);
        
        if (parameters != null)
        {
            ActiveMenu.parameters = parameters;
        }
        
        ActiveMenu.InitializeMenu();
        
    }
}