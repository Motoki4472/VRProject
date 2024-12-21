using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Main.Block
{
    public class GenerateBlockGroup
    {
        public GameObject[] blockPrefabList;
        public GenerateBlockGroup()
        {
            // Prefabのロードとエラーチェック
            blockPrefabList = new GameObject[]
            {
                LoadPrefab("Prefab/Block/RedBlock"), // ID = 1:赤
                LoadPrefab("Prefab/Block/BlueBlock"), // ID = 2:青
                LoadPrefab("Prefab/Block/GreenBlock"), // ID = 3:緑
                LoadPrefab("Prefab/Block/YellowBlock"), // ID = 4:黄
                LoadPrefab("Prefab/Block/PurpleBlock") // ID = 5:紫
            };
        }

        private GameObject LoadPrefab(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            if (prefab == null)
            {
                Debug.LogError($"Failed to load prefab at path: {path}");
            }
            return prefab;
        }

        public void GenerateBlock(int blockId, int x, int y, int z)
        {
            GameObject obj = GameObject.Instantiate(blockPrefabList[blockId - 1], new Vector3(x, y, z), Quaternion.identity);
            var block = obj.GetComponent<Block>();
        }

        public void GenerateBlockGroupObjects()
        {
            int[,,] blockGroupPattern = GenerateBlockGroupPattern();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        if (blockGroupPattern[i, j, k] != 0)
                        {
                            GenerateBlock(blockGroupPattern[i, j, k], i, j, k);
                            // 座標は要検証
                        }
                    }
                }
            }
        }

        public int[,,] GenerateBlockGroupPattern()
        {
            int[,,] blockGroupPattern = new int[2, 2, 2]
            {
                {
                    { 0, 0 },
                    { 0, 0 }
                },
                {
                    { 0, 0 },
                    { 0, 0 }
                },
            };
            int x = 1;
            int y = 1;
            int z = 1;

            for(int i = 0;i < 3;i++)
            {
                x = Random.Range(0, 2);
                y = Random.Range(0, 2);
                z = Random.Range(0, 2);
                blockGroupPattern[x, y, z] = Random.Range(1, 6);
            }

            return blockGroupPattern;
        
        }
    }
}
