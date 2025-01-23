using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Main.Block;

namespace Main.Rule
{
    [System.Serializable]
    public class RuleSystem
    {
        public int GridSize = 4;
        public int GridHeight = 10;
        public (int blockId, bool IsCheck)[,,] State;
        public GameObject[,,] BlockObjects = new GameObject[4, 4, 10];
        private List<(int x, int y, int z)> DeleteBlockGroupeList = new List<(int x, int y, int z)>();
        private List<(int x, int y, int z)> BlockGroupeList = new List<(int x, int y, int z)>();
        private PutBlockSystem putBlockSystem = default;
        private GameObject PlacementDetermination;
        private GameObject[,] gameObjects = new GameObject[4, 4];
        private ProcessSystem processSystem = default;
        private bool isEnd = false;
        private bool isFall = false;
        public int Score = 0;
        private float ScoreFactor = 0;

        public RuleSystem(ProcessSystem processSystem)
        {
            this.processSystem = processSystem;
            PlacementDetermination = LoadPrefab("Prefab/PlacementDetermination");
            State = new (int blockId, bool IsCheck)[GridSize, GridSize, GridHeight];
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    for (int k = 0; k < GridHeight; k++)
                    {
                        State[i, j, k] = (0, false);
                    }
                    GeneratePlacementDetermination(i, j, 0);
                }
            }
            putBlockSystem = new PutBlockSystem(this, gameObjects);
        }

        public void DeleteBlockProcess()
        {
            ScoreFactor = 1;
            isEnd = true;
            isFall = true;
            while (isEnd)
            {
                ScoreFactor += 0.5f;
                CheckDeleteBlockGroupe();
                while (isFall)
                {
                    FallBlock();
                }
            }
            putBlockSystem.ReloardPlacementDetermination();
        }

        public void GeneratePlacementDetermination(int x, int y, int z)
        {
            GameObject obj = GameObject.Instantiate(PlacementDetermination, new Vector3(0, 0, 0), Quaternion.identity);
            obj.GetComponent<PlacementDetermination>().Initialize(x, y, z, this, processSystem);
            gameObjects[x, y] = obj;
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

        public void CheckDeleteBlockGroupe()
        {
            isEnd = true;
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
            RemoveBlockGroupe();
            //Debug.Log("CheckDeleteBlockGroupe");
        }



        private void ResetCheck()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    for (int k = 0; k < GridHeight; k++)
                    {
                        State[i, j, k] = (State[i, j, k].blockId, false);
                    }
                }
            }
            DeleteBlockGroupeList.Clear();
            BlockGroupeList.Clear();
        }
        private void CheckBlockGroupe(int x, int y, int z)
        {
            if (State[x, y, z].IsCheck)
            {
                return;
            }
            BlockGroupeList.Clear();
            CheckBlock(x, y, z, State[x, y, z].blockId);
            if (BlockGroupeList.Count >= 4)
            {
                foreach (var block in BlockGroupeList)
                {
                    //Debug.Log(block);
                    DeleteBlockGroupeList.Add(block);
                }
            }
            if (DeleteBlockGroupeList.Count == 0)
            {
                isEnd = false;
            }

        }



        private void CheckBlock(int x, int y, int z, int blockId)
        {
            if (x < 0 || x >= GridSize || y < 0 || y >= GridSize || z < 0 || z >= GridHeight || State[x, y, z].blockId != blockId || State[x, y, z].IsCheck)
            {
                return;
            }
            if (State[x, y, z].blockId == 0)
            {
                State[x, y, z] = (State[x, y, z].blockId, true);
                return;
            }
            if (State[x, y, z].blockId == blockId)
            {
                BlockGroupeList.Add((x, y, z));
                State[x, y, z] = (State[x, y, z].blockId, true);
            }
            CheckBlock(x + 1, y, z, blockId);
            CheckBlock(x - 1, y, z, blockId);
            CheckBlock(x, y + 1, z, blockId);
            CheckBlock(x, y - 1, z, blockId);
            CheckBlock(x, y, z + 1, blockId);
            CheckBlock(x, y, z - 1, blockId);
        }

        public void AddBlock(int x, int y, int z, int blockId, GameObject blockObject)
        {
            State[x, y, z] = (blockId, false);
            BlockObjects[x, y, z] = blockObject;
            putBlockSystem.ReloardPlacementDetermination();
            //Debug.Log("AddBlock" + x + y + z + blockId);
        }


        public void RemoveBlock(int x, int y, int z)
        {
            isEnd = false;
            State[x, y, z] = (0, false);
            GameObject.Destroy(BlockObjects[x, y, z]);
            BlockObjects[x, y, z] = null;
            putBlockSystem.ReloardPlacementDetermination();
            //Debug.Log("RemoveBlock");
        }

        private void RemoveBlockGroupe()
        {
            foreach (var block in DeleteBlockGroupeList)
            {
                RemoveBlock(block.x, block.y, block.z);
                Score += (int)(ScoreFactor * 100);
            }
        }

        public int[,] CheckTopBlock()
        {
            int[,] TopBlock = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    for (int k = 0; k < GridHeight; k++)
                    {
                        if (State[i, j, k].blockId != 0)
                        {
                            TopBlock[i, j] = k + 1;
                        }
                    }
                }
            }
            return TopBlock;
        }

        //　ブロックが落下する処理
        public void FallBlock()
        {
            isFall = false;
            //Debug.Log("FallBlock");

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    for (int k = 1; k < GridHeight; k++)
                    {
                        if (State[i, j, k].blockId != 0)
                        {
                            if (State[i, j, k - 1].blockId == 0)
                            {
                                State[i, j, k - 1] = (State[i, j, k].blockId, false);
                                State[i, j, k] = (0, false);
                                BlockObjects[i, j, k - 1] = BlockObjects[i, j, k];
                                BlockObjects[i, j, k - 1].GetComponent<Block.Block>().Relocation(new int[3] { i, j, k });
                                BlockObjects[i, j, k] = null;
                                isFall = true;
                            }
                        }
                    }
                }
            }
        }
        public GameObject[,,] GetBlockObjects()
        {
            return BlockObjects;
        }

        // 終了判定
    }
}
