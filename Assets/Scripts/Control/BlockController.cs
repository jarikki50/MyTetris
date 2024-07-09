using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<<���� Ŭ����>>
    �� �����¿� �̵�*/
public class BlockController : MonoBehaviour
{
    //�ϰ� ���� ���� ����
    bool needToStop = false;
    //���� Ȱ��ȭ�� �� ���� ����
    private GameObject activatedBlock;
    //���� Ȱ��ȭ�� ���� �� Ŭ���� ���� ����
    private Block blockData;
    //�� Ŭ���� ���� ����
    private Map map;
    //�� ������ ���� ����
    private BlockSpawner spawner;
    void Start()
    {
        //map������ ������Ʈ GameManager�� �ִ� Map Ŭ���� �Ҵ�
        map = GameObject.Find("GameManager").gameObject.GetComponent<Map>();
        spawner = GameObject.Find("GameManager").gameObject.GetComponent<BlockSpawner>();
    }

    void Update()
    {
        //���� Ȱ��ȭ�� �� ������Ʈ �Ҵ�
        activatedBlock = spawner.activatedBlock;
        if (activatedBlock != null)
        {
            blockData = activatedBlock.GetComponent<Block>();
            BlockMove();
        }
        
    }
    //�� �����¿� ���� �޼���
    void BlockMove()
    {
        if (!blockData.isAtLeftEdge && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
        {                
            //�·� 1��ŭ �ϰ�
            activatedBlock.transform.Translate(Vector3.left, Space.World);

            //�� ��ġ ������Ʈ �޼��� ȣ��
            map.TilePosUpdate(activatedBlock);
        }
        else if (!blockData.isAtRightEdge && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
        {
            //��� 1��ŭ �̵�
            activatedBlock.transform.Translate(Vector3.right, Space.World);

            //�� ��ġ ������Ʈ �޼��� ȣ��
            map.TilePosUpdate(activatedBlock);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            //�Ʒ��� 1��ŭ �ϰ�
            activatedBlock.transform.Translate(Vector3.down, Space.World);

            //�� ��ġ ������Ʈ �޼��� ȣ��
            map.TilePosUpdate(activatedBlock);
        }
    }
    //�� �̵� ���� Ȯ�� �޼���
    public void CheckCanMove()
    {
        //needToStop�� true�� �� ���� �ٴڿ� �ִ� ���̰� �̵��� �����Ѵ�.
        CheckCanFall();
        CheckCanMoveSide();
    }

    private void CheckCanFall()
    {
        needToStop = false;

        for (int i = 0; i < blockData.tilePos.GetLength(0); i++)
        {
            //���� �ٴڿ� ��ġ�� �� �� �̻� �������� ���ϰ� needToStop�� true
            if (blockData.tilePos[i, 1] == 0)
            {
                needToStop = true;
                break;
            }
            //���� Ÿ ��� ���� ��ġ�� �� �� �̻� �������� ���ϰ� needToStop�� true
            if (map.tileExist[blockData.tilePos[i, 1] - 1, blockData.tilePos[i, 0]])
            {
                needToStop = true;
                break;
            }
        }
        if (needToStop)
        {
            //needToStop�� ���� �Ǹ� �� �ϰ� ����
            blockData.falling = false;
        }
    }
    public void CheckCanSpawn()
    {
        if (needToStop) {
            //needToStop�� ���� �Ǹ� �� ����
            spawner.canSpawn = true;
        }
    }
    private void CheckCanMoveSide()
    {
        for (int i = 0; i < blockData.tilePos.GetLength(0); i++)
        {
            //���� ���� ���� ��ġ�� �� �������� �������� ���ϰ� isAtLeftEdge�� true
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
            //���� ���� ���� ��ġ�� �� �������� �������� ���ϰ� isAtLeftEdge�� true
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
