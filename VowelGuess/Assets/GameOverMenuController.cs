using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenuController : UIMenu
{
   [SerializeField] private TextMeshProUGUI playerScoreText;

   private GameOverUIParameters gameOverUIParameters;

   public override void InitializeMenu()
   {
      gameOverUIParameters = GetParameters<GameOverUIParameters>();

      playerScoreText.text = gameOverUIParameters.score.ToString();

   }
}
