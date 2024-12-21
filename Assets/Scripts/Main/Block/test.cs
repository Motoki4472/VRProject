using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Block;

public class test : MonoBehaviour
{

    private GenerateBlockGroup genarateBlockGroup = default;
    private int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        genarateBlockGroup = new GenerateBlockGroup();
    }

    // Update is called once per frame
    void Update()
    {
        // space押すとブロックを生成
        if (Input.GetKeyDown(KeyCode.Space)){
        
            genarateBlockGroup.GenerateBlockGroupObjects();
            i++;

        }

    }

}
