using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Block;
using Main.Player;

namespace Main.Rule
{
    public class ProcessSystem : MonoBehaviour
    {
        private Timer timer = default;
        [SerializeField] private RuleSystem ruleSystem = default;
        private GenerateBlockGroup genarateBlockGroup = default;
        public BlockGroup blockGroupSC = default; // ブロックグループのスクリプト
        private ProcessState processState = ProcessState.Initialize;
        GameObject blockGroup = default;
        [SerializeField] GameObject player = default;
        private bool[] useBlock = new bool[5];
        private GameObject[,,] BlockObjects = new GameObject[4, 4, 10];
        public enum ProcessState
        {
            Initialize,
            PutBlock,
            PostPlacementProcessing,
            EndJudgementProcessing,
            End
        }

        void Start()
        {
            timer = new Timer();
            timer.RunTimer();
            Initialize();
        }

        void Update()
        {
            timer.UpdateTimer();

            if (processState == ProcessState.EndJudgementProcessing)
            {
                EndJudgementProcessing();
            }
            else if (processState == ProcessState.End)
            {
                End();
            }
        }

        private void Initialize()
        {
            genarateBlockGroup = new GenerateBlockGroup(blockGroup);
            ruleSystem = new RuleSystem(this);
            processState = ProcessState.PutBlock;
            PutBlock();
        }

        private void PutBlock()
        {
            //Debug.Log("PutBlock");
            genarateBlockGroup.GenerateBlockGroupObjects();
            blockGroupSC = new BlockGroup(genarateBlockGroup.GenerateBlockGroupGameObject());
            player.GetComponent<Player.PlayerControls>().SetBrockGroup(blockGroupSC);
            useBlock = genarateBlockGroup.GetuseBlock();
            BlockObjects = ruleSystem.GetBlockObjects();
            //ChangeBlockMaterial();
        }

        public void PostPlacementProcessing()
        {
            // ブロックの配置後の処理
            //Debug.Log("PostPlacementProcessing");
            ruleSystem.DeleteBlockProcess();
            processState = ProcessState.EndJudgementProcessing;
            BlockObjects = ruleSystem.GetBlockObjects();
        }

        private void EndJudgementProcessing()
        {
            // ゲーム終了判定処理
            //Debug.Log("EndJudgementProcessing");
            processState = ProcessState.PutBlock;
            PutBlock();
        }

        private void End()
        {
            // ゲーム終了処理
        }

        public void SetProcessStateToPostPlacementProcessing()
        {
            processState = ProcessState.PostPlacementProcessing;
        }

        public void SetProcessStateToEndJudgementProcessing()
        {
            processState = ProcessState.EndJudgementProcessing;
        }

        private void ChangeBlockMaterial()
        {
            // ブロックのマテリアルを変更
            foreach (var block in BlockObjects)
            {
                if (block != null)
                {
                    var blockComponent = block.GetComponent<Block.Block>();
                    if (blockComponent != null)
                    {
                        if (useBlock[blockComponent.GetBlockId()-1] == false)
                        {
                            blockComponent.SetMaterial(1); // 透過
                        }
                        else
                        {
                            blockComponent.SetMaterial(0); // 通常
                        }
                    }
                }
            }
        }

    }
}

