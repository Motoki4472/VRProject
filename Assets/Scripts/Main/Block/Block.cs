using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Rule;

namespace Main.Block
{
    public class Block : MonoBehaviour,IBlock
    {
        [SerializeField]private int blockId;
        [SerializeField]private int[] GridLocation = new int[3];
        // unityのマテリアルを取得　0が通常1が透過
        [SerializeField]private Material[] materials = new Material[2];


        public enum State
        {
            Active,
            Inactive
        }
        State state = State.Inactive;
        public void Start()
        {
            SetActive();
        }
        public int GetBlockId()
        {
            return blockId;
        }
        public bool IsActive()
        {
            if(state == State.Active)
            {
                return true;
            }
            return false;
        }


        public void Relocation(int[] GridLocation)
        {
            this.transform.position = new Vector3(GridLocation[0], GridLocation[2] - 1, GridLocation[1]);
        }
        public void SetActive()
        {
            state = State.Active;
            Fall();
        }
        public void SetInactive()
        {
            state = State.Inactive;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            //親オブジェクトを削除
            this.transform.SetParent(null);
        }

        public void Fall()
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0);
        }

        public void DestroyBlock()
        {
            Destroy(this.gameObject);
        }

        public void SetMaterial(int materialId)
        {
            this.GetComponent<Renderer>().material = materials[materialId];
        }

    }
}

