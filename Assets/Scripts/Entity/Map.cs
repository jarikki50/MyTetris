using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<<��ƼƼ Ŭ����>>
    �� ��ü*/
public class Map : MonoBehaviour
{
    //���� ĭ(20x10)�� Ÿ�� ���� ���� ���� [����,����]
    //���� ���� ���� ���� tileExist[19,0]
    //���� ���� ������ �Ʒ��� tileExist[0,9]
    public bool[,] tileExist = new bool[22,10];
    //�� ���۱� ���� ����
    private BlockController controller;
    void Start()
    {
        controller = GameObject.Find("GameManager").gameObject.GetComponent<BlockController>();
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
        controller.checkCanMove();
        for (int i = 0; i < tileNum; i++)
        {
            //Ÿ�� ���� ���� ǥ��(�����ϰ� ��)
            tileExist[tilePos[i, 1], tilePos[i, 0]] = true;
        }

    }
}
