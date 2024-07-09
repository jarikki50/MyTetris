using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/*<<��ƼƼ Ŭ����>>
    �� ��ü*/
public class Block : MonoBehaviour
{
    //�� �ε��� ����
    private int blockIdx;
    //�̵� ���� ���� ����
    public bool falling = true;
    //���´� ��ġ ���� ����
    public bool isAtLeftEdge = true;
    //�ֿ�� ��ġ ���� ����
    public bool isAtRightEdge = true;
    //�������� �ӵ� ����
    private float fallingSpeed = 0.3f;
    //�� Ŭ���� ���� ����
    private Map map;
    //��������Ʈ������ ���� ����
    private SpriteRenderer spriteRenderer;

    //���� ���� Ÿ�ϵ� ��ǥ ����(ù��° ������ �� �ε���(0~3), �ι�° ������ x�Ǵ� y(0~1)
    //([0,0]�� ���� ù��° Ÿ���� x��ǥ, [0,1]�� ���� ù��° Ÿ���� y��ǥ)
    public int[,] tilePos = new int[4,2];

    void Start()
    {
        //map������ ������Ʈ GameManager�� �ִ� Map Ŭ���� �Ҵ�
        map = GameObject.Find("GameManager").gameObject.GetComponent<Map>();
        //�� ���� ����
        ColorSet();
        //���� Ÿ�� ���� ��ġ ����
        map.TilePosSave(this.gameObject);
        //�� �ϰ� �޼��� ȣ��
        InvokeRepeating("Falling", 0.5f, fallingSpeed);
    }

    void Update()
    {
        Stop();
    }

    //�� �ϰ� �޼���
    void Falling()
    {
        if (falling)
        {
            //�Ʒ��� 1��ŭ �ϰ�
            gameObject.transform.Translate(Vector3.down, Space.World);
        }
        //�� ��ġ ������Ʈ �޼��� ȣ��
        map.TilePosUpdate(this.gameObject);
    }

    // �� �ϰ� ���� �޼���
    void Stop()
    {
        if (!falling)
        {
            CancelInvoke("Falling");
        }
    }

    void ColorSet()
    {
        int ranNum = Random.Range(0, 7);
        Color32[] color = new Color32[] {
            new Color32(253, 74, 56, 255),//������
            new Color32(255, 172, 28, 255),//��Ȳ��
            new Color32(253, 255, 0, 255),//�����
            new Color32(0, 202, 87, 255),//�ʷϻ�
            new Color32(0, 102, 255, 255),//�Ķ���
            new Color32(4, 67, 180, 255),//����
            new Color32(123, 0, 233, 255)//�����
        };

        for (int i = 0; i < 4; i++)
        {
            //���� �ڽ� ������Ʈ�� Ÿ�ϵ��� ���� ����
            spriteRenderer = gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
            spriteRenderer.color = color[ranNum];
        }
    }
}
