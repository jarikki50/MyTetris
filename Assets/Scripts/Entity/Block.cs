using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/*<<엔티티 클래스>>
    블럭 개체*/
public class Block : MonoBehaviour
{
    //블럭 인덱스 변수
    private int blockIdx;
    //이동 가능 여부 변수
    public bool falling = true;
    //최좌단 위치 여부 변수
    public bool isAtLeftEdge = true;
    //최우단 위치 여부 변수
    public bool isAtRightEdge = true;
    //떨어지는 속도 변수
    private float fallingSpeed = 0.3f;
    //맵 클래스 참조 변수
    private Map map;
    //스프라이트렌더러 참조 변수
    private SpriteRenderer spriteRenderer;

    //현재 블럭의 타일들 좌표 변수(첫번째 차원은 블럭 인덱스(0~3), 두번째 차원은 x또는 y(0~1)
    //([0,0]은 블럭의 첫번째 타일의 x좌표, [0,1]은 블럭은 첫번째 타일의 y좌표)
    public int[,] tilePos = new int[4,2];

    void Start()
    {
        //map변수에 오브젝트 GameManager에 있는 Map 클래스 할당
        map = GameObject.Find("GameManager").gameObject.GetComponent<Map>();
        //블럭 색깔 설정
        ColorSet();
        //블럭의 타일 현재 위치 저장
        map.TilePosSave(this.gameObject);
        //블럭 하강 메서드 호출
        InvokeRepeating("Falling", 0.5f, fallingSpeed);
    }

    void Update()
    {
        Stop();
    }

    //블럭 하강 메서드
    void Falling()
    {
        if (falling)
        {
            //아래로 1만큼 하강
            gameObject.transform.Translate(Vector3.down, Space.World);
        }
        //블럭 위치 업데이트 메서드 호출
        map.TilePosUpdate(this.gameObject);
    }

    // 블럭 하강 중지 메서드
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
            new Color32(253, 74, 56, 255),//빨간색
            new Color32(255, 172, 28, 255),//주황색
            new Color32(253, 255, 0, 255),//노란색
            new Color32(0, 202, 87, 255),//초록색
            new Color32(0, 102, 255, 255),//파란색
            new Color32(4, 67, 180, 255),//남색
            new Color32(123, 0, 233, 255)//보라색
        };

        for (int i = 0; i < 4; i++)
        {
            //블럭의 자식 오브젝트인 타일들의 색깔 설정
            spriteRenderer = gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
            spriteRenderer.color = color[ranNum];
        }
    }
}
