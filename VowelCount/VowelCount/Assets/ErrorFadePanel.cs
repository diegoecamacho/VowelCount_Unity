
using System.Collections;

using TMPro;
using UnityEngine;

using Image = UnityEngine.UI.Image;

public class ErrorFadePanel : MonoBehaviour
{

    [SerializeField] private float errorLength;

    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private Image errorBackground;

    private Color originalBackgroundColor;
    private Color originalTextColor;

    private float timeElapsed;
    public void EnableError()
    {
        UIEnabled(true);

        timeElapsed = errorLength;
        StartCoroutine(FadeError());

    }

    public void UIEnabled(bool enabled)
    {
        errorText.gameObject.SetActive(enabled);
        errorBackground.gameObject.SetActive(enabled);
    }

    IEnumerator FadeError()
    {
        originalBackgroundColor = errorBackground.color;
        originalTextColor = errorText.color;
        do
        {
            timeElapsed -= Time.deltaTime;
            originalBackgroundColor.a = timeElapsed;
            originalTextColor.a = timeElapsed;

            errorBackground.color = originalBackgroundColor;
            errorText.color = originalTextColor;

            yield return null;


        } while (timeElapsed > 0);
        
        UIEnabled(false);
        
        
    }
}
