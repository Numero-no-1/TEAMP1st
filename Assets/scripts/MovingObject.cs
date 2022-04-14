using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public static MovingObject instance;

    public float speed;//  p움직임. 플레이어 스피드
    public int walkCount;
    private int currentWalkCount;

    private Vector3 vector;//  p움직임. x, y, z

    private BoxCollider2D boxCollider;
    private LayerMask layerMask;
    private Animator animator;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;
    private bool canMove = true;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            // p움직임. update의 방향키 입력에 대한 벡터값 적용
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;


            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                // p실제 움직임.
                transform.Translate(vector.x * (speed + applyRunSpeed), vector.y * (speed + applyRunSpeed), 0);
                if (applyRunFlag)
                    currentWalkCount++;
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;

        }
        animator.SetBool("Walking", false);
        canMove = true;

    }



    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {// (Input.GetAxisRaw("Horizontal") 좌 -1 우 1 || Input.GetAxisRaw("Vertical") != 0) 상 1 하 -1. p움직임 방향
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

    }
}

//void Update()
//{

//    // (Input.GetAxisRaw("Horizontal") 좌 -1 우 1 || Input.GetAxisRaw("Vertical") != 0) 상 1 하 -1
//    if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)// p움직임. 방향
//    {
//        // p움직임. update의 방향키 입력에 대한 벡터값 적용
//        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

//          Translate 현재 있는 값에서 vector.x * speed 이 숫자만큼 더해 준다.
//          상하좌우 움직임 구현
//        if (vector.x != 0)
//        {
//            transform.Translate(vector.x * speed, 0, 0);
//        }
//        else if (vector.y != 0)
//        {
//            transform.Translate(0, vector.y * speed, 0);
//        }
//    }
//}
