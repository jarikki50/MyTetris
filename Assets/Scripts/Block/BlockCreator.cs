using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
[System.Serializable]
public class BlockColor
{
    //색깔 별 블럭 배열
    public GameObject[] color;
     
}
public class BlockCreator : MonoBehaviour
{
    //블럭 배열
    public BlockColor[] blocks;


    //블럭 인덱스, 블럭의 색깔 인덱스
    int blockIdx = 0, colorIdx = 0;

    //블럭 조종 가능 여부
    public bool ActivatedBlockExist = false;

    //현재 활성화된 블럭 참조 변수
    public GameObject activatedBlock;

    //현재 활성화된 블럭 설정 참조 변수
    public BlockSetting setting;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!ActivatedBlockExist)
        {
            Debug.Log("업데이트");
            //블럭 생성 메서드 호출, 생성된 블럭 저장
            activatedBlock = SpawnBlock();

            ActivatedBlockExist = true;
        }
    }

    //블럭 생성 메서드
    GameObject SpawnBlock()
    {
        Debug.Log("생성전");
        //각각 블럭 인덱스, 색깔 인덱스
        blockIdx = Random.Range(0, blocks.Length);
        colorIdx = Random.Range(0, blocks[blockIdx].color.Length);
        //블럭 생성
        Instantiate(blocks[blockIdx].color[colorIdx]);
        //블럭 설정 하기
        setting = blocks[blockIdx].color[colorIdx].AddComponent<BlockSetting>();
        Debug.Log("생성후");
        //생성된 블럭 반환
        return blocks[blockIdx].color[colorIdx];
    }
}
