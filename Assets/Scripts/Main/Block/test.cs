using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Block;

public class test : MonoBehaviour
{

    private GenerateBlockGroup genarateBlockGroup = default;
    private BlockGroup blockGroupSC = default; // ブロックグループのスクリプト
    [SerializeField] GameObject blockGroup = default;
    // Start is called before the first frame update
    void Start()
    {
        genarateBlockGroup = new GenerateBlockGroup(blockGroup);
    }

    // Update is called once per frame
    void Update()
    {
        // space押すとブロックを生成
        if (Input.GetKeyDown(KeyCode.Space)){
        
            genarateBlockGroup.GenerateBlockGroupObjects();
            blockGroupSC = new BlockGroup(genarateBlockGroup.GenerateBlockGroupGameObject());

        }
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            blockGroupSC.HorizontalXMove();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            blockGroupSC.HorizontalZMove();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            blockGroupSC.HorizontalClockwiseRotation();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            blockGroupSC.PutDown();
        }
        if (Input.GetKeyDown(KeyCode.R)){
            blockGroupSC.Reset();
        }

    }

}
