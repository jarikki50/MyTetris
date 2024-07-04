using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSetting : MonoBehaviour
{
    //블럭의 마지막 위치 저장 변수
    private Vector2 lastPosition;
    //블럭이 이동 가능한 상태인지 여부
    public bool canMove = true;
    //블럭 컨트롤러 참조 변수
    public BlockController controller;
    void Start()
    {
        //이전 위치를 저장
        lastPosition = transform.position;
    }

    void Update()
    {
        //이전 위치를 계속 저장
        if (canMove) {
            lastPosition = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌 시 이전 위치로 되돌리고 움직임 비활성화
        canMove = false;
        //오브젝트 비활성화

        this.transform.position = lastPosition;
        
    }
}
