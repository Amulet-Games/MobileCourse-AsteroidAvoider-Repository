using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AG
{
    public class TitleScreen : MonoBehaviour
    {
        public void UIButton_Play()
        {
            SceneManager.LoadScene(1);
        }
    }
}