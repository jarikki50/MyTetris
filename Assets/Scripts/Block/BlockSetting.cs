using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSetting : MonoBehaviour
{
    //���� ������ ��ġ ���� ����
    private Vector2 lastPosition;
    //���� �̵� ������ �������� ����
    public bool canMove = true;
    //�� ��Ʈ�ѷ� ���� ����
    public BlockController controller;
    void Start()
    {
        //���� ��ġ�� ����
        lastPosition = transform.position;
    }

    void Update()
    {
        //���� ��ġ�� ��� ����
        if (canMove) {
            lastPosition = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹 �� ���� ��ġ�� �ǵ����� ������ ��Ȱ��ȭ
        canMove = false;
        //������Ʈ ��Ȱ��ȭ

        this.transform.position = lastPosition;
        
    }
}
