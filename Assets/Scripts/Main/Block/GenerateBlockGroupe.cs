using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Main.Block
{
    public class GenerateBlockGroup : MonoBehaviour
    {
        public GameObject[] blockPrefabList = new GameObject[]
        {
            // ID = 0:未使用
            Resources.Load<GameObject>("Prefabs/Block/RedBlock"), // ID = 1:赤
            Resources.Load<GameObject>("Prefabs/Block/BlueBlock"), // ID = 2:青
            Resources.Load<GameObject>("Prefabs/Block/GreenBlock"), // ID = 3:緑
            Resources.Load<GameObject>("Prefabs/Block/YellowBlock"), // ID = 4:黄
            Resources.Load<GameObject>("Prefabs/Block/PurpleBlock"), // ID = 5:紫
        };

        public void GenerateBlock(int blockId, int x, int y, int z)
        {
            GameObject obj = GameObject.Instantiate(blockPrefabList[blockId - 1], new Vector3(x, y, z), Quaternion.identity);
            var block = obj.GetComponent<Block>();
        }

        public void GenerateBlockGroupObjects()
        {
            int[,,] blockGroupPattern = GenerateBlockGroupPattern();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
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
            int[,,] blockGroupPattern = new int[3, 3, 3]
            {
                {
                    { 0, 0, 0 },
                    { 0, 0, 0 },
                    { 0, 0, 0 }
                },
                {
                    { 0, 0, 0 },
                    { 0, 0, 0 },
                    { 0, 0, 0 }
                },
                {
                    { 0, 0, 0 },
                    { 0, 0, 0 },
                    { 0, 0, 0 }
                }
            };
            int x = 1;
            int y = 1;
            int z = 1;

            //[1,1,1]の中央からランダムな連続した1~3つのブロックを生成
            blockGroupPattern[x, y, z] = Random.Range(1, 6);
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                (blockGroupPattern, x, y, z) = SearchBlockPattern(blockGroupPattern, x, y, z);
            }

            var flatList = blockGroupPattern.Cast<int>().ToList();
            int maxCount = flatList.GroupBy(val => val).Max(group => group.Count());
            if (maxCount >= 4)
            {
                blockGroupPattern = GenerateBlockGroupPattern();
            }


            return blockGroupPattern;
        }

        private (int[,,], int, int, int) SearchBlockPattern(int[,,] blockGroupPattern, int x, int y, int z)
        {
            int direction = Random.Range(0, 6);
            switch (direction)
            {
                case 0:
                    if (x + 1 < 3)
                    {
                        x++;
                    }
                    else
                    {
                        x--;
                    }
                    break;
                case 1:
                    if (x - 1 >= 0)
                    {
                        x--;
                    }
                    else
                    {
                        x++;
                    }
                    break;
                case 2:
                    if (y + 1 < 3)
                    {
                        y++;
                    }
                    else
                    {
                        y--;
                    }
                    break;
                case 3:
                    if (y - 1 >= 0)
                    {
                        y--;
                    }
                    else
                    {
                        y++;
                    }
                    break;
                case 4:
                    if (z + 1 < 3)
                    {
                        z++;
                    }
                    else
                    {
                        z--;
                    }
                    break;
                case 5:
                    if (z - 1 >= 0)
                    {
                        z--;
                    }
                    else
                    {
                        z++;
                    }
                    break;
            }
            blockGroupPattern[x, y, z] = Random.Range(1, 6);
            return (blockGroupPattern, x, y, z);
        }
    }
}
