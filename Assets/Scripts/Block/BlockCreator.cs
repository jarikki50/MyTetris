using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
[System.Serializable]
public class BlockColor
{
    //���� �� �� �迭
    public GameObject[] color;
     
}
public class BlockCreator : MonoBehaviour
{
    //�� �迭
    public BlockColor[] blocks;


    //�� �ε���, ���� ���� �ε���
    int blockIdx = 0, colorIdx = 0;

    //�� ���� ���� ����
    public bool ActivatedBlockExist = false;

    //���� Ȱ��ȭ�� �� ���� ����
    public GameObject activatedBlock;

    //���� Ȱ��ȭ�� �� ���� ���� ����
    public BlockSetting setting;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!ActivatedBlockExist)
        {
            Debug.Log("������Ʈ");
            //�� ���� �޼��� ȣ��, ������ �� ����
            activatedBlock = SpawnBlock();

            ActivatedBlockExist = true;
        }
    }

    //�� ���� �޼���
    GameObject SpawnBlock()
    {
        Debug.Log("������");
        //���� �� �ε���, ���� �ε���
        blockIdx = Random.Range(0, blocks.Length);
        colorIdx = Random.Range(0, blocks[blockIdx].color.Length);
        //�� ����
        Instantiate(blocks[blockIdx].color[colorIdx]);
        //�� ���� �ϱ�
        setting = blocks[blockIdx].color[colorIdx].AddComponent<BlockSetting>();
        Debug.Log("������");
        //������ �� ��ȯ
        return blocks[blockIdx].color[colorIdx];
    }
}
