using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Block
{
    public class BlockGroup
    {
        private GameObject blockGroup = default;
        public BlockGroup(GameObject blockGroup)
        {
            this.blockGroup = blockGroup;
        }

        public void HorizontalXMove()
        {
            blockGroup.transform.position += new Vector3(1, 0, 0);
        }
        public void AntiHorizontalXMove()
        {
            blockGroup.transform.position += new Vector3(-1, 0, 0);
        }
        public void HorizontalZMove()
        {
            blockGroup.transform.position += new Vector3(0, 0, 1);
        }
        public void AntiHorizontalZMove()
        {
            blockGroup.transform.position += new Vector3(0, 0, -1);
        }
        public void HorizontalClockwiseRotation()
        {
            blockGroup.transform.Rotate(0, 90, 0);
        }
        public void HorizontalCounterClockwiseRotation()
        {
            blockGroup.transform.Rotate(0, -90, 0);
        }
        public void VerticalClockwiseRotation()
        {
            blockGroup.transform.Rotate(90, 0, 0);
        }
        public void VerticalCounterClockwiseRotation()
        {
            blockGroup.transform.Rotate(-90, 0, 0);
        }

        public void PutDown()
        {
            blockGroup.transform.position += new Vector3(0, -1, 0);
        }

        public void Reset()
        {
            blockGroup.transform.position = new Vector3(0, 0, 0);
            blockGroup.transform.rotation = Quaternion.identity;
        }

        //子オブジェクトが存在するか確認
        public bool HasChild()
        {
            if (blockGroup.transform.childCount > 0)
            {
                return true;
            }
            return false;
        }

        public void DestroyBlockGroup()
        {
            GameObject.Destroy(blockGroup);
        }
        
    }
}