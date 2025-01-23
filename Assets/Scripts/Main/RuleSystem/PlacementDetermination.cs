using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Block;

namespace Main.Rule
{
    public class PlacementDetermination : MonoBehaviour
    {
        [SerializeField]private int[] GridLocation = new int[3];
        [SerializeField]BlockGroup blockGroup = default;
        ProcessSystem processSystem= default;
        private RuleSystem ruleSystem = default;
        public void Initialize(int x, int y, int z, RuleSystem ruleSystem,ProcessSystem processSystem)
        {
            GridLocation = new int[3] { x, y, z };
            this.ruleSystem = ruleSystem;
            this.transform.position = new Vector3(x, (float)z - 0.45f, y);
            this.processSystem = processSystem;
        }

        public void PlacementBlock(int blockId, GameObject block)
        {
            // ブロックの位置を固定？する関数を呼び出しもしくはブロック側で書く
            ruleSystem.AddBlock(GridLocation[0], GridLocation[1], GridLocation[2], blockId, block);
        }

        public int[] GetGridLocation()
        {
            return GridLocation;
        }

        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Block")
            {
                if (collision.gameObject.GetComponent<Block.Block>().IsActive() == false)
                {
                    return;
                }
                PlacementBlock(collision.gameObject.GetComponent<Block.Block>().GetBlockId(), collision.gameObject);
                collision.gameObject.GetComponent<Block.Block>().SetInactive();
                collision.gameObject.GetComponent<Block.Block>().Relocation(GridLocation);
                blockGroup = processSystem.blockGroupSC;
                if(!blockGroup.HasChild())
                {
                    processSystem.SetProcessStateToPostPlacementProcessing();
                    processSystem.PostPlacementProcessing();
                    blockGroup.DestroyBlockGroup();
                }
            }
        }

        public void MovePlacementDetermination(int x, int y, int z)
        {
            this.transform.position = new Vector3(x, (float)z - 0.45f, y);
            GridLocation = new int[3] { x, y, z };
        }

        public void Fall()
        {
            this.transform.position += new Vector3(0, -1, 0);
            GridLocation[2] -= 1;
        }


    }
}
