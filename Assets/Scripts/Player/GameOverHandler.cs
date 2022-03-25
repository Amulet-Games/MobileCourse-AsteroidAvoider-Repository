using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class GameOverHandler : MonoBehaviour
    {
        [Header("Refs.")]
        public AsteroidSpawner asteroidSpawner;
        public GameOverScreen gameoverScreen;
        public SetScoreDisplay scoreDisplay;
        public PlayerHealth p_Health;

        [Header("Static.")]
        public static GameOverHandler singleton;

        #region Callbacks.
        private void Awake()
        {
            if (singleton != null)
                Destroy(gameObject);
            else
                singleton = this;

            AdManager.singleton.gameoverHandler = this;
        }
        #endregion

        public void GameOver()
        {
            asteroidSpawner.DeactivateSpawner();
            scoreDisplay.DeactivateScoreSystem();

            gameoverScreen.ShowScreen();
            gameoverScreen.SetEndGameScoreText(scoreDisplay.score);
        }

        public void ContinueGame()
        {
            asteroidSpawner.ReactivateSpawner();
            scoreDisplay.ActivateScoreSystem();
            gameoverScreen.HideScreen();
            p_Health.Revived();
        }
    }
}