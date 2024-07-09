using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*<<엔티티 클래스>>
    맵 개체*/
public class Map : MonoBehaviour
{
    //맵의 칸(22x10)에 타일 존재 여부 변수 [세로,가로]
    //맵의 제일 왼쪽 위는 tileExist[21,0]
    //맵의 제일 오른쪽 아래는 tileExist[0,9]
    public bool[,] tileExist = new bool[22,10];
    //생성된 블럭 리스트
    public List<GameObject> blockList;
    //블럭 조작기 참조 변수
    private BlockController controller;
    //블럭 생성기 참조 변수
    private BlockSpawner spawner;
    void Start()
    {
        controller = GameObject.Find("GameManager").gameObject.GetComponent<BlockController>();
        spawner = GameObject.Find("GameManager").gameObject.GetComponent<BlockSpawner>();
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
        blockList = spawner.blockList;
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
    // 블럭 이동 시 맵(배열)에 블럭의 위치 저장
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
        controller.CheckCanMove();
        for (int i = 0; i < tileNum; i++)
        {
            //타일 존재 여부 표시(존재하게 됨)
            tileExist[tilePos[i, 1], tilePos[i, 0]] = true;
        }
        //블럭이 적재 되었을 시 라인 완성 여부 체크
        if (!blockData.falling)
        {
            //라인 완성 여부 체크 메서드 호출
            CheckLineFull();
        }
    }
    //라인 완성 여부 체크 메서드
    public void CheckLineFull() 
    {
        for (int y = 0; y < tileExist.GetLength(0); y++)
        {
            int rowCount = 0;
            for (int x = 0;x< tileExist.GetLength(1); x++)
            {
                if (tileExist[y,x])
                {
                    rowCount++;
                }
            }
            //맵의 열이 10줄이기 때문에 10개
            if(rowCount == 10)
            {
                Debug.Log("라인 클리어");
                ClearLine(y);
            }
            rowCount = 0;
        }
        //블럭 생산 가능 여부 체크
        controller.CheckCanSpawn();
    }
    //라인 완성시 클리어하는 메서드
    private void ClearLine(int rowNum)
    {
        //계층에 존재하는 블럭 순회
        foreach (GameObject block in blockList)
        {
            //블럭의 자식 오브젝트인 타일 목록 참조
            List<GameObject> tileList = GetTileList(block);
            //해당 블럭의 타일 순회
            foreach (GameObject tile in tileList)
            {
                //해당 행의 타일 삭제
                if (tile.transform.position.y == rowNum) 
                {
                    Destroy(tile);
                }
            }
        }

        for (int x = 0; x < tileExist.GetLength(1); x++)
        {
            //라인 완성 시 해당 행 false
            tileExist[rowNum, x] = false;
        }
        //라인 완성 됐기에 완성된 라인과 그 위 라인 하강
        LineFall(rowNum);
    }

    //라인 완성시 완성된 라인과 그 위의 행들이 끝까지 하강하는 메서드
    private void LineFall(int rowNum)
    {
        //계층에 존재하는 블럭 순회
        foreach (GameObject block in blockList)
        {
            Block blockData = block.GetComponent<Block>();
            
            //블럭의 자식 오브젝트인 타일 목록 참조
            List<GameObject> tileList = GetTileList(block);
            //타일이 블럭의 몇번째인지 표시
            int tileCount = 0;
            //해당 블럭의 타일 순회
            for (int i = 0; i < 4; i++)
            {
                /*타일 하나 제거 했을 때 블럭에는 그대로 배열 4개가 남아있는데 그거 해결해야함*/
                if (blockData.tilePos[i,1] > rowNum) {
                    blockData.tilePos[i, 1] -= 1;
                    //모든 블럭들 y 좌표 -1
                    block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y - 1, block.transform.position.z);
                }
                
            }
            foreach (GameObject tile in tileList)
            {
                int tileXPos = (int)tile.transform.position.x;
                int tileYPos = (int)tile.transform.position.y;
                bool temp = false;

                //맵 위치 갱신
                if (tileYPos > 0)
                {
                    temp = tileExist[tileYPos, tileXPos];
                    tileExist[tileYPos, tileXPos] = false;
                    Debug.Log($"Y:{tileYPos}, X:{tileXPos}");
                    tileExist[tileYPos - 1, tileXPos] = temp;
                }
                tileCount++;
            }

            
        }
    }

    //특정 블럭의 자식 오브젝트인 타일 목록을 반환하는 메서드
    private List<GameObject> GetTileList(GameObject block)
    {
        int TESTCOUNT = 0;
        List<GameObject> tileList = new List<GameObject>();
        int childNum = block.transform.childCount;
        TESTCOUNT++;
        Debug.Log($"{childNum}----{TESTCOUNT}");
        for (int i = 0; i < childNum; i++) { 
            Transform tileTransform = block.transform.GetChild(i);
            tileList.Add(tileTransform.gameObject);
        }
        return tileList;
    }

    //타일이 하나도 남지 않은 블록 삭제하기
    private void DestroyNullBlock()
    {
        //blockList 수정
    }

}
