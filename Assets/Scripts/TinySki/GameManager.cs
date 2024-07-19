using System;
using UnityEngine;

namespace TinySki
{
    public class GameManager : MonoBehaviour
    {
        public GameObject flagPrefab;

        public float flagSpawnX = 2f;

        public float flagAmountPerLine = 40;
        public float flagYSpace = 0.32f;

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
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

