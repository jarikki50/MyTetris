using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<<���� Ŭ����>>
    �� ����*/
public class BlockSpawner : MonoBehaviour
{
    //�� ������ ���� ����
    public GameObject[] blockPrefab;
    //�� �ν��Ͻ� ���� ����
    public GameObject activatedBlock;
    //�� �ε���
    private int blockIdx = 0;
    //�� ���� x ��ǥ
    private float positionX = 0.0f;
    //�� ���� ���� ����
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

    //�� ���� �޼���
    void SpawnBlock () 
    {
        //���� �ϰ� �� �ε���(0 ~ 6) �� �ϳ� ����
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
        //�� ���� �� ����
        activatedBlock = Instantiate(blockPrefab[blockIdx]);
        Debug.Log("�� ����");
        //�� �ʱ� ��ġ ����
        activatedBlock.transform.position = new Vector3(positionX, blockPrefab[blockIdx].transform.position.y);
    }
}
