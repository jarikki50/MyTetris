using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<<엔티티 클래스>>
    맵 개체*/
public class Map : MonoBehaviour
{
    //맵의 칸(20x10)에 타일 존재 여부 변수 [세로,가로]
    //맵의 제일 왼쪽 위는 tileExist[19,0]
    //맵의 제일 오른쪽 아래는 tileExist[0,9]
    public bool[,] tileExist = new bool[22,10];
    //블럭 조작기 참조 변수
    private BlockController controller;
    //블럭 생성기 참조 변수
    private BlockSpawner spawner;
    //생성된 블럭 리스트 참조 변수
    //public List<GameObject> blockList;
    void Start()
    {
        controller = GameObject.Find("GameManager").gameObject.GetComponent<BlockController>();
        spawner = GameObject.Find("GameManager").gameObject.GetComponent<BlockSpawner>();
        //blockList = spawner.blockList;
        for (int i = 0; i < tileExist.GetLength(0); i++)
        {
            for(int j = 0; j < tileExist.GetLength(1); j++)
            {
                tileExist[i,j] = false;
            }
        }
    }

    void Update()
    {
        
    }

    //블럭의 타일 위치 저장 메서드
    public void TilePosSave(GameObject block)
    {
        //이동하는 블럭의 블럭 클래스 참조
        Block blockData = block.GetComponent<Block>();
        //블럭의 타일 개수 변수
        int tileNum = block.transform.childCount;
        //타일 위치(벡터) 변수
        Vector3[] tileVector = new Vector3[tileNum];

        //블럭의 타일들 좌표 변수(첫번째 차원은 블럭 인덱스(0~3), 두번째 차원은 x또는 y(0~1)
        //([0,0]은 블럭의 첫번째 타일의 x좌표, [0,1]은 블럭은 첫번째 타일의 y좌표)
        int[,] tilePos = new int[4, 2];

        for(int i = 0; i < tileNum; i++)
        {
            //타일들의 현재 위치 저장
            Transform tileTransform = block.transform.GetChild(i);
            //타일 위치(벡터) 저장
            tileVector[i] = tileTransform.position;
            //타일 위치(배열) 저장
            tilePos[i, 0] = (int)tileVector[i].x;
            tilePos[i, 1] = (int)tileVector[i].y;

            //블럭 오브젝트의 타일 위치 저장
            blockData.tilePos[i, 0] = tilePos[i, 0];
            blockData.tilePos[i, 1] = tilePos[i, 1];
            //블럭 이동 가능 확인 메서드 호출
            //controller.checkCanMove();
            //타일 존재 여부 표시(존재하게 됨)
            tileExist[tilePos[i, 1], tilePos[i, 0]] = true;
        }
    }
    // 블럭 이동 시 맵(배열)에 블럭의 위치 저장하는 메서드
    public void TilePosUpdate(GameObject block)
    {
        //이동하는 블럭의 블럭 클래스 참조
        Block blockData = block.GetComponent<Block>();
        //블럭의 타일 개수 변수
        int tileNum = block.transform.childCount;
        //타일 위치(벡터) 변수
        Vector3[] tileVector = new Vector3[tileNum];

        //블럭의 타일들 좌표 변수(첫번째 차원은 블럭 인덱스(0~3), 두번째 차원은 x또는 y(0~1)
        //([0,0]은 블럭의 첫번째 타일의 x좌표, [0,1]은 블럭은 첫번째 타일의 y좌표)
        int[,] tilePos = new int[4, 2];

        for (int i = 0; i < tileNum; i++)
        {
            //타일 이동 전 위치(배열)를 통해 tileExist(타일 존재 여부) 인덱스 계산
            int xIndex = 0, yIndex = 0;
            xIndex = blockData.tilePos[i, 0];
            yIndex = blockData.tilePos[i, 1];

            //타일 존재 여부 표시(존재하지 않게됨)
            //블럭이 이동하기에 해당 위치에 타일 사라짐
            tileExist[yIndex, xIndex] = false;

            //타일들의 현재 위치 저장
            Transform tileTransform = block.transform.GetChild(i);
            //타일 위치(벡터) 저장
            tileVector[i] = tileTransform.position;
            //타일 위치(배열) 저장
            tilePos[i, 0] = (int)tileVector[i].x;
            tilePos[i, 1] = (int)tileVector[i].y;

            //블럭 오브젝트의 타일 위치 저장
            blockData.tilePos[i, 0] = tilePos[i, 0];
            blockData.tilePos[i, 1] = tilePos[i, 1];
        }
        //블럭 이동 가능 확인 메서드 호출
        controller.checkCanMove();
        for (int i = 0; i < tileNum; i++)
        {
            //타일 존재 여부 표시(존재하게 됨)
            tileExist[tilePos[i, 1], tilePos[i, 0]] = true;
        }

    }
    //라인 한 줄이 가득 채워졌는 지 확인하는 메서드
    /*public void CheckIsLineFull()
    {
        for (int i = 0; i < tileExist.GetLength(0); i++)
        {
            int column = 0;
            for (int j = 0; j < tileExist.GetLength(1); j++)
            {
                //해당 행의 열 개수 카운트
                if (tileExist[i, j]) column++;
            }
            //해당 행의 열 개수가 10이면 한 줄 완성
            if (column == 10) DeleteFullLine(i);
        }
    }
    //한 줄 완성 시 해당 행 클리어
    private void DeleteFullLine(int rowNum)
    {
        foreach (GameObject block in blockList)
        {
            Debug.Log("=============");
            Debug.Log(block.name);
            Debug.Log("=============");
        }
        //완성된 줄들이 클리어 되면 블럭이 스폰 가능하게 함
        spawner.canSpawn = true;
    }*/
}
