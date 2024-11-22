using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Rule
{
    public class RuleSystem
    {
        public int GridSize = 4;
        public int GridHeight = 10;
        public (int blockId, bool IsCheck)[ , , ] State;
        private List<(int x,int y, int z)> DeleteBlockGroupeList = new List<(int x,int y, int z)>();
        private List<(int x,int y, int z)> BlockGroupeList = new List<(int x,int y, int z)>();
        private PutBlockSystem putBlockSystem = default;

        public RuleSystem()
        {
            putBlockSystem = new PutBlockSystem(this);
            State = new (int blockId, bool IsCheck)[ GridSize, GridSize, GridHeight ];
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    for (int k = 0; k < GridHeight; k++)
                    {
                        State[ i, j, k ] = (0, false);
                    }
                }
            }
        }

        public void SetBlock(int x, int y, int z, int blockId)
        {
            State[ x, y, z ] = (blockId, false);
        }

        public void CheckDeleteBlockGroupe()
        {
            ResetCheck();
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    for (int k = 0; k < GridHeight; k++)
                    {
                        CheckBlockGroupe(i, j, k);
                    }
                }
            }


        }

        private void ResetCheck()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    for (int k = 0; k < GridHeight; k++)
                    {
                        State[ i, j, k ] = (State[ i, j, k ].blockId, false);
                    }
                }
            }
            DeleteBlockGroupeList.Clear();
            BlockGroupeList.Clear();
        }

        private void CheckBlockGroupe(int x, int y, int z)
        {   
            if (State[ x, y, z ].IsCheck)
            {
                return;
            }
            BlockGroupeList.Clear();
            CheckBlock(x, y, z, State[ x, y, z ].blockId);
            if (BlockGroupeList.Count >= 4)
            {
                foreach (var block in BlockGroupeList)
                {
                    DeleteBlockGroupeList.Add(block);
                }
            }

        }

        private void CheckBlock(int x, int y, int z, int blockId)
        {
            if (x < 0 || x >= GridSize || y < 0 || y >= GridSize || z < 0 || z >= GridHeight)
            {
                return;
            }
            if (State[ x, y, z ].blockId != blockId || State[ x, y, z ].IsCheck)
            {
                return;
            }
            BlockGroupeList.Add((x, y, z));
            State[ x, y, z ] = (State[ x, y, z ].blockId, true);
            CheckBlock(x + 1, y, z, blockId);
            CheckBlock(x - 1, y, z, blockId);
            CheckBlock(x, y + 1, z, blockId);
            CheckBlock(x, y - 1, z, blockId);
            CheckBlock(x, y, z + 1, blockId);
            CheckBlock(x, y, z - 1, blockId);
        }

        public void AddBlock(int x, int y, int z, int blockId)
        {
            State[ x, y, z ] = (blockId, false);
            putBlockSystem.ReloardlacementDetermination();
        }

        public void RemoveBlock(int x, int y, int z)
        {
            State[ x, y, z ] = (0, false);
            putBlockSystem.ReloardlacementDetermination();
        }

        public int[,] CheckTopBlock()
        {
            int[,] TopBlock = new int[4, 4];
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    for (int k = 0; k < GridHeight; k++)
                    {
                        if (State[ i, j, k ].blockId != 0)
                        {
                            TopBlock[ i, j ] = k;
                            break;
                        }
                    }
                }
            }
            return TopBlock;
        }

        // 終了判定
    }
}
