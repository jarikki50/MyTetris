using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    //처음 블럭 생성 시간, 이후 블럭 생성 반복 간격
    private float startTime = 1.0f, fallingRate = 1.0f;

    public BlockCreator blockCreator;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("스타트");
        InvokeRepeating("Falling", startTime, fallingRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Falling()
    {
        //활성화된 블럭이 지정된 속도로 아래로 낙하
        if (blockCreator.ActivatedBlockExist && blockCreator.setting.canMove)
        {
            Debug.Log("낙하전");
            blockCreator.activatedBlock.transform.Translate(new Vector2(0.0f,-1.0f));
            Debug.Log("낙하전");
        }
    }
}
