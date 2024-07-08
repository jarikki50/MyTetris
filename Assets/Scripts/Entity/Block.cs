using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*<<엔티티 클래스>>
    블럭 개체*/
public class Block : MonoBehaviour
{
    //블럭 인덱스 변수
    private int blockIdx;
    //이동 가능 여부 변수
    private bool canMove = true;
    //떨어지는 속도 변수
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

    //블럭 하강 메서드
    void Falling()
    {
        gameObject.transform.Translate(Vector3.down);
        
    }
    // 블럭 하강 중지 메서드
    void Stop()
    {
        if (!canMove)
        {
            CancelInvoke("Falling");
        }
    }
}
