using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Block
{
    public class Block : IBlock
    {
        [SerializeField]private int blockId;
        public int GetBlockId()
        {
            return blockId;
        }
    }
}

