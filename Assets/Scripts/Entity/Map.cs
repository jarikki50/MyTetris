using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*<<��ƼƼ Ŭ����>>
    �� ��ü*/
public class Map : MonoBehaviour
{
    //���� ĭ(22x10)�� Ÿ�� ���� ���� ���� [����,����]
    //���� ���� ���� ���� tileExist[21,0]
    //���� ���� ������ �Ʒ��� tileExist[0,9]
    public bool[,] tileExist = new bool[22,10];
    //������ �� ����Ʈ
    public List<GameObject> blockList;
    //�� ���۱� ���� ����
    private BlockController controller;
    //�� ������ ���� ����
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

    //���� Ÿ�� ��ġ ���� �޼���
    public void TilePosSave(GameObject block)
    {
        //�̵��ϴ� ���� �� Ŭ���� ����
        Block blockData = block.GetComponent<Block>();
        //���� Ÿ�� ���� ����
        int tileNum = block.transform.childCount;
        //Ÿ�� ��ġ(����) ����
        Vector3[] tileVector = new Vector3[tileNum];

        //���� Ÿ�ϵ� ��ǥ ����(ù��° ������ �� �ε���(0~3), �ι�° ������ x�Ǵ� y(0~1)
        //([0,0]�� ���� ù��° Ÿ���� x��ǥ, [0,1]�� ���� ù��° Ÿ���� y��ǥ)
        int[,] tilePos = new int[4, 2];

        for(int i = 0; i < tileNum; i++)
        {
            //Ÿ�ϵ��� ���� ��ġ ����
            Transform tileTransform = block.transform.GetChild(i);
            //Ÿ�� ��ġ(����) ����
            tileVector[i] = tileTransform.position;
            //Ÿ�� ��ġ(�迭) ����
            tilePos[i, 0] = (int)tileVector[i].x;
            tilePos[i, 1] = (int)tileVector[i].y;

            //�� ������Ʈ�� Ÿ�� ��ġ ����
            blockData.tilePos[i, 0] = tilePos[i, 0];
            blockData.tilePos[i, 1] = tilePos[i, 1];
            //�� �̵� ���� Ȯ�� �޼��� ȣ��
            //controller.checkCanMove();
            //Ÿ�� ���� ���� ǥ��(�����ϰ� ��)
            tileExist[tilePos[i, 1], tilePos[i, 0]] = true;
        }
    }
    // �� �̵� �� ��(�迭)�� ���� ��ġ ����
    public void TilePosUpdate(GameObject block)
    {
        //�̵��ϴ� ���� �� Ŭ���� ����
        Block blockData = block.GetComponent<Block>();
        //���� Ÿ�� ���� ����
        int tileNum = block.transform.childCount;
        //Ÿ�� ��ġ(����) ����
        Vector3[] tileVector = new Vector3[tileNum];

        //���� Ÿ�ϵ� ��ǥ ����(ù��° ������ �� �ε���(0~3), �ι�° ������ x�Ǵ� y(0~1)
        //([0,0]�� ���� ù��° Ÿ���� x��ǥ, [0,1]�� ���� ù��° Ÿ���� y��ǥ)
        int[,] tilePos = new int[4, 2];

        for (int i = 0; i < tileNum; i++)
        {
            //Ÿ�� �̵� �� ��ġ(�迭)�� ���� tileExist(Ÿ�� ���� ����) �ε��� ���
            int xIndex = 0, yIndex = 0;
            xIndex = blockData.tilePos[i, 0];
            yIndex = blockData.tilePos[i, 1];

            //Ÿ�� ���� ���� ǥ��(�������� �ʰԵ�)
            //���� �̵��ϱ⿡ �ش� ��ġ�� Ÿ�� �����
            tileExist[yIndex, xIndex] = false;

            //Ÿ�ϵ��� ���� ��ġ ����
            Transform tileTransform = block.transform.GetChild(i);
            //Ÿ�� ��ġ(����) ����
            tileVector[i] = tileTransform.position;
            //Ÿ�� ��ġ(�迭) ����
            tilePos[i, 0] = (int)tileVector[i].x;
            tilePos[i, 1] = (int)tileVector[i].y;

            //�� ������Ʈ�� Ÿ�� ��ġ ����
            blockData.tilePos[i, 0] = tilePos[i, 0];
            blockData.tilePos[i, 1] = tilePos[i, 1];
        }
        //�� �̵� ���� Ȯ�� �޼��� ȣ��
        controller.CheckCanMove();
        for (int i = 0; i < tileNum; i++)
        {
            //Ÿ�� ���� ���� ǥ��(�����ϰ� ��)
            tileExist[tilePos[i, 1], tilePos[i, 0]] = true;
        }
        //���� ���� �Ǿ��� �� ���� �ϼ� ���� üũ
        if (!blockData.falling)
        {
            //���� �ϼ� ���� üũ �޼��� ȣ��
            CheckLineFull();
        }
    }
    //���� �ϼ� ���� üũ �޼���
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
            //���� ���� 10���̱� ������ 10��
            if(rowCount == 10)
            {
                Debug.Log("���� Ŭ����");
                ClearLine(y);
            }
        }
        //�� ���� ���� ���� üũ
        controller.CheckCanSpawn();
    }
    //���� �ϼ��� Ŭ�����ϴ� �޼���
    private void ClearLine(int rowNum)
    {
        //������ �����ϴ� �� ��ȸ
        foreach (GameObject block in blockList)
        {
            //���� �ڽ� ������Ʈ�� Ÿ�� ��� ����
            List<GameObject> tileList = GetTileList(block);
            //�ش� ���� Ÿ�� ��ȸ
            foreach (GameObject tile in tileList)
            {
                //�ش� ���� Ÿ�� ����
                if (tile.transform.position.y == rowNum) 
                {
                    Destroy(tile);
                    Debug.Log($"{tile.transform.position.y}�� ����");
                }
            }
        }

        for (int x = 0; x < tileExist.GetLength(1); x++)
        {
            //���� �ϼ� �� �ش� �� false
            tileExist[rowNum, x] = false;
        }
        //���� �ϼ� �Ʊ⿡ �ϼ��� ���ΰ� �� �� ���� �ϰ�
        LineFall(rowNum);
    }

    //���� �ϼ��� �ϼ��� ���ΰ� �� ���� ����� ������ �ϰ��ϴ� �޼���
    private void LineFall(int rowNum)
    {
        //������ �����ϴ� �� ��ȸ
        foreach (GameObject block in blockList)
        {
            //���� �ڽ� ������Ʈ�� Ÿ�� ��� ����
            List<GameObject> tileList = GetTileList(block);
            //�ش� ���� Ÿ�� ��ȸ
            foreach (GameObject tile in tileList)
            {
                //Ŭ������ �� ���������� ��ȸ
                for(int y = rowNum + 1; y < tileExist.GetLength(0); y++)
                {
                    for(int x = 0; x < tileExist.GetLength(1); x++)
                    {
                        tileExist[y, x] = false;
                        //LowestYPos ȣ��
                    }
                }
            }
        }
    }

    //Ư�� ���� �ڽ� ������Ʈ�� Ÿ�� ����� ��ȯ�ϴ� �޼���
    private List<GameObject> GetTileList(GameObject block)
    {
        List<GameObject> tileList = new List<GameObject>();
        for (int i = 0; i < block.transform.childCount; i++) { 
            Transform tileTransform = block.transform.GetChild(i);
            tileList.Add(tileTransform.gameObject);
        }
        return tileList;
    }


}
