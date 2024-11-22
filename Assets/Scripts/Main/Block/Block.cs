using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Rule;

namespace Main.Block
{
    public class Block : MonoBehaviour,IBlock
    {
        [SerializeField]private int blockId;
        private int[] GridLocation = new int[3];
        public int GetBlockId()
        {
            return blockId;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "PlacementDetermination")
            {
                collision.gameObject.GetComponent<PlacementDetermination>().PlacementBlock(blockId);
                GridLocation = collision.gameObject.GetComponent<PlacementDetermination>().GetGridLocation();
                this.GetComponent<Collider>().enabled = false;
                //きれいに配置しなおす
                Relocation(GridLocation);
            }
        }

        public void Relocation(int[] GridLocation)
        {
            this.transform.position = new Vector3(GridLocation[0], GridLocation[1], GridLocation[2]);
        }

    }
}

