using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*<<��ƼƼ Ŭ����>>
    �� ��ü*/
public class Block : MonoBehaviour
{
    //�� �ε��� ����
    private int blockIdx;
    //�̵� ���� ���� ����
    private bool canMove = true;
    //�������� �ӵ� ����
    private float fallingSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Falling", 0.5f, fallingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Stop();
    }

    //�� �ϰ� �޼���
    void Falling()
    {
        gameObject.transform.Translate(Vector3.down);
        
    }
    // �� �ϰ� ���� �޼���
    void Stop()
    {
        if (!canMove)
        {
            CancelInvoke("Falling");
        }
    }
}
