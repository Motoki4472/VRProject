using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Rule
{
    public class PutBlockSystem
    {
        [SerializeField] private RuleSystem ruleSystem = default;
        [SerializeField] private GameObject PlacementDeterminationPrefab = default;
        private int[,] TopBlock = new int[4, 4];

        public PutBlockSystem(RuleSystem ruleSystem)
        {
            this.ruleSystem = ruleSystem;
        }

        public void ReloardlacementDetermination()
        {
            // 一度全てのPlacementDeterminationを削除
            GameObject[] objs = GameObject.FindGameObjectsWithTag("PlacementDetermination");
            foreach (var obj in objs)
            {
                GameObject.Destroy(obj);
            }
            TopBlock = ruleSystem.CheckTopBlock();
            // Gridの座標でそれぞれのブロックの上にブロックを設置するためのオブジェクトを生成
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (TopBlock[i, j] < 10)
                    {
                        GameObject obj = GameObject.Instantiate(PlacementDeterminationPrefab, new Vector3(i, j, TopBlock[i, j]), Quaternion.identity);
                        var placementDetermination = obj.GetComponent<PlacementDetermination>();
                        if (placementDetermination != null)
                        {
                            placementDetermination.Initialize(i, j, TopBlock[i, j] + 1, ruleSystem);
                        }
                        // 座標は要検証
                    }
                }
            }

        }
    }
}