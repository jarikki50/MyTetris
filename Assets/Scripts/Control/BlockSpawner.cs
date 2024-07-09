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
    private GameObject block;
    //�� �ε���
    private int blockIdx = 0;
    //�� ���� x ��ǥ
    private float positionX = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //�� ���� �޼��� ȣ�� !!!���� ����!!!
        InvokeRepeating("SpawnBlock", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        block = Instantiate(blockPrefab[blockIdx]);
        //�� �ʱ� ��ġ ����
        block.transform.position = new Vector3(positionX, blockPrefab[blockIdx].transform.position.y);
    }
}
