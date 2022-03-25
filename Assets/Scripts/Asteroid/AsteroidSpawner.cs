using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [Header("Prefabs.")]
        public Rigidbody[] a_prefabs;

        [Header("Config.")]
        public float a_spawnRate = 1.5f;
        public Vector2 a_forceRange = new Vector2(4, 6);

        [Header("Refs.")]
        [ReadOnlyInspector] public Camera mainCamera;

        [Header("Status.")]
        [ReadOnlyInspector] public float timer;

        #region Callbacks.
        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            MonitorSpawnTimer();
        }
        #endregion

        #region Tick.
        void MonitorSpawnTimer()
        {
            timer += Time.deltaTime;
            if (timer >= a_spawnRate)
            {
                timer = 0;
                SpawnAsteroid();
            }
        }

        void SpawnAsteroid()
        {
            int sideIndex = Random.Range(0, 4);

            Vector2 spawnPoint = Vector2.zero;
            Vector2 direction = Vector2.zero;

            switch (sideIndex)
            {
                case 0:
                    spawnPoint.x = 0;
                    spawnPoint.y = Random.value;
                    direction = new Vector2(1, Random.Range(-1f, 1f));
                    break;
                case 1:
                    spawnPoint.x = 1;
                    spawnPoint.y = Random.value;
                    direction = new Vector2(-1, Random.Range(-1f, 1f));
                    break;
                case 2:
                    spawnPoint.x = Random.value;
                    spawnPoint.y = 0;
                    direction = new Vector2(Random.Range(-1f, 1f), 1);
                    break;
                case 3:
                    spawnPoint.x = Random.value;
                    spawnPoint.y = 1;
                    direction = new Vector2(Random.Range(-1f, 1f), -1);
                    break;
            }

            Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
            worldSpawnPoint.z = 0;

            Rigidbody newAsteroid = Instantiate(
                a_prefabs[Random.Range(0, a_prefabs.Length)], 
                worldSpawnPoint, 
                Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

            newAsteroid.velocity = direction.normalized * Random.Range(a_forceRange.x, a_forceRange.y);
        }
        #endregion

        #region Public.
        public void ReactivateSpawner()
        {
            gameObject.SetActive(true);
        }

        public void DeactivateSpawner()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}