using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Rule
{
    public class PlacementDetermination : MonoBehaviour
    {
        private int[] GridLocation = new int[3];
        private RuleSystem ruleSystem = default;
        public void Initialize(int x, int y, int z, RuleSystem ruleSystem)
        {
            GridLocation = new int[3] { x, y, z };
            this.ruleSystem = ruleSystem;
        }

        public void PlacementBlock(int blockId)
        {
            // ブロックの位置を固定？する関数を呼び出しもしくはブロック側で書く
            ruleSystem.AddBlock(GridLocation[0], GridLocation[1], GridLocation[2], blockId);
        }

        public int[] GetGridLocation()
        {
            return GridLocation;
        }

    }
}
