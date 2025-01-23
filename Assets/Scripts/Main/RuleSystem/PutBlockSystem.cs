using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Rule
{
    public class PutBlockSystem
    {
        [SerializeField] private RuleSystem ruleSystem = default;
        private int[,] TopBlock = new int[4, 4];
        private GameObject[,] gameObjects = new GameObject[4, 4];

        public PutBlockSystem(RuleSystem ruleSystem, GameObject[,] PlacementDeterminationPrefab)
        {
            this.ruleSystem = ruleSystem;
            this.gameObjects = PlacementDeterminationPrefab;
        }


        public void ReloardPlacementDetermination()
        {
            TopBlock = ruleSystem.CheckTopBlock();
            // Gridの座標でそれぞれのブロックの上にブロックを設置するためのオブジェクトを生成
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (TopBlock[i, j] < 10)
                    {
                        gameObjects[i,j].GetComponent<PlacementDetermination>().MovePlacementDetermination(i, j, TopBlock[i, j]);
                    }
                }
            }
        }

        public void FallPlacementDetermination(int x, int y)
        {
            gameObjects[x, y].GetComponent<PlacementDetermination>().Fall();
        }

    }
}