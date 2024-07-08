using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<<제어 클래스>>
    블럭 생성*/
public class BlockSpawner : MonoBehaviour
{
    //블럭 프리팹 참조 변수
    public GameObject[] blockPrefab;
    //블럭 인스턴스 참조 변수
    public GameObject activatedBlock;
    //블럭 인덱스
    private int blockIdx = 0;
    //블럭 생성 x 좌표
    private float positionX = 0.0f;
    //블럭 생성 가능 여부
    public bool canSpawn = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            canSpawn = false;
            SpawnBlock();    
        }
    }

    //블럭 생성 메서드
    void SpawnBlock () 
    {
        //블럭의 일곱 개 인덱스(0 ~ 6) 중 하나 선택
        blockIdx = Random.Range(0, blockPrefab.Length);
        switch (blockIdx)
        {
            case 5:
                positionX = Random.Range(0, 9);
                break;
            case 6:
                positionX = Random.Range(0, 10);
                break;
            default:
                positionX = Random.Range(0, 8);
                break;
        }
        //블럭 생성 후 참조
        activatedBlock = Instantiate(blockPrefab[blockIdx]);
        Debug.Log("블럭 생성");
        //블럭 초기 위치 설정
        activatedBlock.transform.position = new Vector3(positionX, blockPrefab[blockIdx].transform.position.y);
    }
}
