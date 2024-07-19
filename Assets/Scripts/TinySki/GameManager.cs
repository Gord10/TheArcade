using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TinySki
{
    public class GameManager : MonoBehaviour
    {
        public Vector2 gravity; //This value will be added to desiredMovement.y of skiers
        [Header("Obstacles")]
        public GameObject[] obstaclePrefabs;

        public int obstacleAmount = 30;
        public int maxObstacleX = 100; //This will be divided to 100 later
        public float obstacleYSpace = 1f;

        [Header("Flags")]
        public GameObject flagPrefab;
        public float flagSpawnX = 2f;

        public float flagAmountPerLine = 40;
        public float flagYSpace = 0.32f;

        private GameObject[] obstacles;
        
        private void Awake()
        {
            int i;
            
            for (i = 0; i < flagAmountPerLine; i++)
            {
                Vector3 flagPosition = new Vector3();
                flagPosition.x = flagSpawnX;
                flagPosition.y = -flagYSpace * i;
                Instantiate(flagPrefab, flagPosition, Quaternion.identity, transform);

                flagPosition.x *= -1;
                Instantiate(flagPrefab, flagPosition, Quaternion.identity, transform);
            }
            
            obstacles = new GameObject[obstacleAmount];

            for (i = 0; i < obstacleAmount; i++)
            {
                Vector3 obstaclePosition = new Vector3();
                obstaclePosition.x = Random.Range(-maxObstacleX, maxObstacleX) / 100f;
                obstaclePosition.y = -i * obstacleYSpace;
                GameObject randomObstacle = obstaclePrefabs[0];
                obstacles[i] = Instantiate(randomObstacle, obstaclePosition, Quaternion.identity, transform);
            }
        }

        //NPC will try to avoid the next obstacle. They need to know the position of the next one for that.
        public GameObject GetNextObstacleForPlayer(Player player)
        {
            if (obstacles == null)
            {
                return null;
            }
            
            float playerY = player.transform.position.y;
            int i;
            
            for (i = 0; i < obstacles.Length; i++)
            {
                if (obstacles[i].transform.position.y < playerY)
                {
                    return obstacles[i];
                }
            }

            return null;
        }
    }
}

