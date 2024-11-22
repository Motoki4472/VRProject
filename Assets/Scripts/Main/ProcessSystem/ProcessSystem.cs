using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Rule;

namespace Main.ProcessSystem
{
    public class ProcessSystem : MonoBehaviour
    {
        private Timer timer = default;
        private RuleSystem ruleSystem = default;
        private ProcessState processState = ProcessState.Initialize;
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
        }

        void Update()
        {
            timer.UpdateTimer();
            if(processState == ProcessState.Initialize)
            {
                Initialize();
            }
            else if(processState == ProcessState.PutBlock)
            {
                PutBlock();
            }
            else if(processState == ProcessState.PostPlacementProcessing)
            {
                PostPlacementProcessing();
            }
            else if(processState == ProcessState.EndJudgementProcessing)
            {
                EndJudgementProcessing();
            }
            else if(processState == ProcessState.End)
            {
                End();
            }
        }

        private void Initialize()
        {
            processState = ProcessState.PutBlock;
            ruleSystem = new RuleSystem();
        }

        private void PutBlock()
        {
            ruleSystem.CheckDeleteBlockGroupe();
            // ブロックを配置する処理
            processState = ProcessState.PostPlacementProcessing;
        }

        private void PostPlacementProcessing()
        {
            // ブロックの配置後の処理
            processState = ProcessState.EndJudgementProcessing;
        }

        private void EndJudgementProcessing()
        {
            // ゲーム終了判定処理
            processState = ProcessState.End;
        }

        private void End()
        {
            // ゲーム終了処理
        }


    }
}

