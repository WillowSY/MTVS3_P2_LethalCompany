using Mono.Cecil;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    // 플레이어의 이동 속도
    public float moveSpeed = 10f;

    // 점프에 필요한 힘의 세기
    public float jumpPower = 4f;

    // 플레이어의 회전 속도
    public float rotationSpeed = 5f;
    // 플레이어의 메인카메라
    public Camera mainCamera;
    
    private Animator anim;
    private Rigidbody rb; // Rigidbody 컴포넌트
    
    private bool isGrounded; // 점프 중인지 여부

    
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        isGrounded = true;

        // Rigidbody의 회전 제한 설정
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // X와 Z 축 회전을 고정시킴
    }

    private void Update()
    {
        MovePlayer(); // 플레이어 이동 처리
        Jump(); // 점프 처리
        RotatePlayer(); // 플레이어 회전 처리
    }
    

    // 플레이어 이동 처리 함수
    public void MovePlayer()
    {
        float h = Input.GetAxis("Horizontal"); // 수평 입력
        float v = Input.GetAxis("Vertical"); // 수직 입력

        // 카메라의 전방 벡터를 기준으로 이동 방향 계산
        
        Vector3 cameraForward = mainCamera.transform.forward; // 카메라의 전방 벡터
        Vector3 cameraRight = mainCamera.transform.right; // 카메라의 오른쪽 벡터
        cameraForward.y = 0f; // y 방향은 무시
        cameraRight.y = 0f; // y 방향은 무시

        Vector3 moveDirection = cameraForward * v + cameraRight * h; // 이동 방향 계산
        moveDirection.Normalize();

        // 이동 방향이 존재할 경우에만 플레이어를 이동시킴
        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("IsWalking", true);
            Vector3 movement = moveSpeed * Time.deltaTime * moveDirection; // 이동 거리 계산
            transform.Translate(movement, Space.World); // 플레이어 이동
        }
        else
        {
                anim.SetBool("IsWalking", false);
                anim.SetBool("Idle", true);
                
        }
    }

    // 점프 처리 함수
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) // 점프 키를 눌렀고, 지면에 맞닿아 있을 때
        {
            isGrounded = false; // 공중에 있음
            anim.SetBool("IsJumping", true);
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // 위쪽으로 힘을 가하여 점프
        }
    }

    // 땅과 충돌했을 때 점프 상태 변경
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject) // 어떤 물체와 충돌했을 때
        {
            isGrounded = true; // 점프 중 아님
            anim.SetBool("IsJumping", false);
        }
    }

    // 플레이어 회전 처리 함수
    private void RotatePlayer()
    {
        float h = Input.GetAxis("Horizontal"); // 수평 입력
        float v = Input.GetAxis("Vertical"); // 수직 입력

        // 플레이어의 회전 방향 설정
        Vector3 cameraForward = mainCamera.transform.forward; // 카메라의 전방 벡터
        Vector3 cameraRight = mainCamera.transform.right; // 카메라의 오른쪽 벡터
        cameraForward.y = 0f; // x 방향을 무시하여 수직 회전 방향 제거
        cameraRight.y = 0f; // z 방향을 무시하여 수직 회전 방향 제거

        Vector3 moveDirection = cameraForward.normalized * v + cameraRight.normalized * h; // 이동 방향 계산

        // 이동 방향이 존재할 경우에만 회전 적용
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection); // 목표 회전 방향 계산
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // 부드럽게 회전
        }
    }
}
