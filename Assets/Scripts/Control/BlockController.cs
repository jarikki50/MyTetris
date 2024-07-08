using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<<제어 클래스>>
    블럭 상하좌우 이동*/
public class BlockController : MonoBehaviour
{
    //현재 활성화된 블럭 참조 변수
    private GameObject activatedBlock;
    //현재 활성화된 블럭의 블럭 클래스 참조 변수
    private Block blockData;
    //맵 클래스 참조 변수
    private Map map;
    //블럭 생성기 참조 변수
    private BlockSpawner spawner;
    void Start()
    {
        //map변수에 오브젝트 GameManager에 있는 Map 클래스 할당
        map = GameObject.Find("GameManager").gameObject.GetComponent<Map>();
        spawner = GameObject.Find("GameManager").gameObject.GetComponent<BlockSpawner>();
    }

    void Update()
    {
        //현재 활성화된 블럭 오브젝트 할당
        activatedBlock = GameObject.Find("GameManager").GetComponent<BlockSpawner>().activatedBlock;
        if (activatedBlock != null)
        {
            blockData = activatedBlock.GetComponent<Block>();
            BlockMove();
        }
        
    }
    //블럭 상하좌우 조작 메서드
    void BlockMove()
    {
        if (!blockData.isAtLeftEdge && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
        {                
            //좌로 1만큼 하강
            activatedBlock.transform.Translate(Vector3.left, Space.World);

            //블럭 위치 업데이트 메서드 호출
            map.TilePosUpdate(activatedBlock);
        }
        else if (!blockData.isAtRightEdge && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
        {
            //우로 1만큼 이동
            activatedBlock.transform.Translate(Vector3.right, Space.World);

            //블럭 위치 업데이트 메서드 호출
            map.TilePosUpdate(activatedBlock);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            //아래로 1만큼 하강
            activatedBlock.transform.Translate(Vector3.down, Space.World);

            //블럭 위치 업데이트 메서드 호출
            map.TilePosUpdate(activatedBlock);
        }
    }
    //블럭 이동 가능 확인 메서드
    public void checkCanMove()
    {
        //needToStop이 true일 시 블럭이 바닥에 있는 것이고 이동을 중지한다.
        checkCanFall();
        checkCanMoveSide();
    }

    private void checkCanFall()
    {
        bool needToStop = false;

        for (int i = 0; i < blockData.tilePos.GetLength(0); i++)
        {
            //블럭이 바닥에 위치할 시 더 이상 떨어지지 못하게 needToStop이 true
            if (blockData.tilePos[i, 1] == 0)
            {
                needToStop = true;
                break;
            }
            //블럭이 타 블록 위에 위치할 시 더 이상 떨어지지 못하게 needToStop이 true
            if (map.tileExist[blockData.tilePos[i, 1] - 1, blockData.tilePos[i, 0]])
            {
                needToStop = true;
                break;
            }
        }
        if (needToStop)
        {
            //needToStop이 참이 되면 블럭 스폰 가능하게 하고 블럭이 떨어지지 못하게 함
            blockData.falling = false;
            spawner.canSpawn = true;
        }
    }

    private void checkCanMoveSide()
    {
        for (int i = 0; i < blockData.tilePos.GetLength(0); i++)
        {
            //블럭이 왼쪽 끝에 위치할 시 왼쪽으로 움직이지 못하게 isAtLeftEdge가 true
            if (blockData.tilePos[i, 0] == 0 ||
                map.tileExist[blockData.tilePos[i, 1], blockData.tilePos[i, 0] - 1])
            {
                blockData.isAtLeftEdge = true;
                break;
            }
            else
            {
                blockData.isAtLeftEdge = false;
            }
        }

        for(int i = 0; i < blockData.tilePos.GetLength(0); i++)
        {
            //블럭이 왼쪽 끝에 위치할 시 왼쪽으로 움직이지 못하게 isAtLeftEdge가 true
            if (blockData.tilePos[i, 0] == 9 ||
                map.tileExist[blockData.tilePos[i, 1], blockData.tilePos[i, 0] + 1])
            {
                blockData.isAtRightEdge = true;
                break;
            }
            else
            {
                blockData.isAtRightEdge = false;
            }
        }
    }
}
