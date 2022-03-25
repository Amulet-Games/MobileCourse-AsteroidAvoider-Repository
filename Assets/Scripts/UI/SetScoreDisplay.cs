using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AG
{
    public class SetScoreDisplay : MonoBehaviour
    {
        [Header("UI Text (Drops).")]
        public TMP_Text scoreText;

        [Header("Config.")]
        public float scoreMulti;

        [Header("Private.")]
        [ReadOnlyInspector] public float score;

        private void Update()
        {
            score += scoreMulti * Time.deltaTime;
            scoreText.text = score.ToString("0");
        }

        public void DeactivateScoreSystem()
        {
            gameObject.SetActive(false);
        }

        public void ActivateScoreSystem()
        {
            gameObject.SetActive(true);
        }
    }
}