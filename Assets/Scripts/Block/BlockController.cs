using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    //ó�� �� ���� �ð�, ���� �� ���� �ݺ� ����
    private float startTime = 1.0f, fallingRate = 1.0f;

    public BlockCreator blockCreator;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("��ŸƮ");
        InvokeRepeating("Falling", startTime, fallingRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Falling()
    {
        //Ȱ��ȭ�� ���� ������ �ӵ��� �Ʒ��� ����
        if (blockCreator.ActivatedBlockExist && blockCreator.setting.canMove)
        {
            Debug.Log("������");
            blockCreator.activatedBlock.transform.Translate(new Vector2(0.0f,-1.0f));
            Debug.Log("������");
        }
    }
}
