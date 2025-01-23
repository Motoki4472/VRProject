using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem; // 新しいInput System用
using Main.Block;

namespace Main.Player
{
    public class PlayerControls : MonoBehaviour
    {
        private GenerateBlockGroup generateBlockGroup = default;
        private BlockGroup blockGroupSC = default; // ブロックグループのスクリプト
        [SerializeField] GameObject blockGroup = default;

        // コントローラーの入力
        [SerializeField] private InputActionProperty moveAction;
        [SerializeField] private InputActionProperty rotateXAction;
        [SerializeField] private InputActionProperty rotateYAction;
        [SerializeField] private InputActionProperty dropAction;
        [SerializeField] private InputActionProperty resetAction;
        private bool isMove = false;

        void Start()
        {
            generateBlockGroup = new GenerateBlockGroup(blockGroup);
            
        }

        void Update()
        {
            if (blockGroupSC == null)
            {
                return;
            }

            if(Input.GetKeyDown(KeyCode.RightArrow) && !isMove)
            {
                blockGroupSC.VerticalClockwiseRotation(); // 右移動
                isMove = true;
                StartCoroutine(MoveWait());
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMove)
            {
                blockGroupSC.HorizontalClockwiseRotation(); // 左移動
                isMove = true;
                StartCoroutine(MoveWait());
            }

            // 右スティックで横移動
            Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
            if (moveInput.x > 0.5f && !isMove)
            {
                blockGroupSC.HorizontalXMove(); // 右移動
                isMove = true;
                StartCoroutine(MoveWait());
            }
            else if (moveInput.x < -0.5f && !isMove)
            {
                blockGroupSC.AntiHorizontalXMove(); // 左移動
                isMove = true;
                StartCoroutine(MoveWait());
            }

            if(moveInput.y > 0.5f && !isMove)
            {
                blockGroupSC.HorizontalZMove(); // 前移動
                isMove = true;
                StartCoroutine(MoveWait());
            }
            else if (moveInput.y < -0.5f && !isMove)
            {
                blockGroupSC.AntiHorizontalZMove(); // 後ろ移動
                isMove = true;
                StartCoroutine(MoveWait());
            }

            // 左トリガーで回転
            if (rotateXAction.action.triggered)
            {
                blockGroupSC.HorizontalClockwiseRotation();
            }
            // 左グリップで回転
            else if (rotateYAction.action.triggered)
            {
                blockGroupSC.HorizontalCounterClockwiseRotation();
            }

            // トリガーで落下
            if (dropAction.action.triggered)
            {
                blockGroupSC.PutDown();
            }

            // 特定のボタンでリセット
            if (resetAction.action.triggered)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
            }
        }

        public void SetBrockGroup(BlockGroup blockGroupSC)
        {
            this.blockGroupSC = blockGroupSC;
        }

        private IEnumerator MoveWait()
        {
            yield return new WaitForSeconds(0.2f);
            isMove = false;
        }
    }
}
