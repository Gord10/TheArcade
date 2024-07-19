using System;
using UnityEngine;

namespace TinySki
{
    public class GameCamera : MonoBehaviour
    {
        private Player[] players;
        private float targetY = 0;
        
        private void Awake()
        {
            players = FindObjectsByType<Player>(FindObjectsSortMode.None);
        }

        void CalculateTargetY()
        {
            int i;
            float totalY = 0;
            for (i = 0; i < players.Length; i++)
            {
                totalY += players[i].transform.position.y;
            }

            targetY = totalY / players.Length;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            CalculateTargetY();
            Vector3 newPosition = transform.position;
            newPosition.y = targetY;
            transform.position = newPosition;
        }
    }
}
