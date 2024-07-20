using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arena
{
    public class GameManager : MonoBehaviour
    {
        public Gladiator[] gladiators;

        public float throwInterval = 2f;
        public Throwable[] throwablePrefabs;
        public Transform[] throwableSpawnPoints;
        public float throwSpeed = 10;

        public Throwable latestThrowable;

        private void Awake()
        {
            StartCoroutine(ThrowItemsToGladiators());
        }

        public Vector3 GetRandomPointInCenter()
        {
            Vector3 pos = new Vector3();
            pos.x = Random.Range(-2f, 2f);
            pos.z =Random.Range(-2f, 2f);
            return pos;
        }

        IEnumerator ThrowItemsToGladiators()
        {
            WaitForSeconds wait = new WaitForSeconds(throwInterval);

            while (true)
            {
                int spawnPointIndex = Random.Range(0, throwableSpawnPoints.Length);
                Transform spawnTransform = throwableSpawnPoints[spawnPointIndex];
                Vector3 spawnPoint = spawnTransform.position;

                int randomThrowableIndex = Random.Range(0, throwablePrefabs.Length);
                Throwable throwablePrefab = throwablePrefabs[randomThrowableIndex];
                Throwable throwable = Instantiate(throwablePrefab, spawnPoint, Quaternion.identity);

                int targetGladiatorIndex = Random.Range(0, gladiators.Length);
                Gladiator targetGladiator = gladiators[targetGladiatorIndex];
                Vector3 targetPos = targetGladiator.targetPoint.position;
                targetPos.x += Random.Range(-0.2f, 0.2f);
                
                throwable.Throw(targetPos, throwSpeed);
                latestThrowable = throwable;
                yield return wait;
            }
        }

    }
}
