using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class PlayerHealth : MonoBehaviour
    {
        public Rigidbody p_rb;

        public void Crash()
        {
            gameObject.SetActive(false);
            GameOverHandler.singleton.GameOver();
        }

        public void Revived()
        {
            gameObject.SetActive(true);
            p_rb.velocity = Vector3.zero;
        }
    }
}