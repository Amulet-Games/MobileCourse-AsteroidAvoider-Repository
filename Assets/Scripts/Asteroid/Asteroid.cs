using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class Asteroid : MonoBehaviour
    {
        [Header("Config.")]
        public float maxSpinSpeed = 1f;
        public float minSpinSpeed = 0.3f;

        Transform mTransform;
        Vector3 spinVector3;

        private void Start()
        {
            mTransform = transform;
            spinVector3 = new Vector3(Random.Range(minSpinSpeed, maxSpinSpeed), 0, Random.Range(minSpinSpeed, maxSpinSpeed));
        }

        private void Update()
        {
            Spin();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
            {
                other.GetComponent<PlayerHealth>().Crash();
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        void Spin()
        {
            mTransform.localEulerAngles += spinVector3 * Time.deltaTime;
        }
    }
}