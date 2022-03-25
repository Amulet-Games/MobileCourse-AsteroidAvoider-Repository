using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace AG
{
    public class GameOverScreen : MonoBehaviour
    {
        [Header("Canvas (Drops).")]
        public Canvas screenCanvas;

        [Header("TMP Text (Drops).")]
        public TMP_Text endGameScoreText;

        [Header("Buttons (Drops).")]
        public Button continueGameButton;

        private void Start()
        {
            continueGameButton.onClick.AddListener(AdManager.singleton.UIButton_Continue);
        }

        #region Show / Hide Screen
        public void ShowScreen()
        {
            screenCanvas.enabled = true;
        }

        public void HideScreen()
        {
            screenCanvas.enabled = false;
        }
        #endregion

        #region UI Buttons.
        public void UIButton_Retry()
        {
            SceneManager.LoadScene(1);
        }

        public void UIButton_Title()
        {
            SceneManager.LoadScene(0);
        }
        #endregion

        #region Set Score Text.
        public void SetEndGameScoreText(float score)
        {
            endGameScoreText.text = score.ToString("0");
        }
        #endregion
    }
}