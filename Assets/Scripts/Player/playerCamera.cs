using UnityEngine;

public class playerCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f; // 마우스 감도 설정
    public Transform playerBody; // 플레이어의 몸체 Transform을 저장할 변수

    private float _xRotation = 0f; // 카메라의 x축 회전 값 저장

    void Start()
    {
        // 마우스 커서를 화면 중앙에 고정하고 숨김
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 입력 값을 받아옴
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // x축 회전 값 업데이트 (위, 아래 움직임)
        _xRotation -= mouseY;
        // x축 회전 값 제한 (카메라가 뒤집히지 않도록)
        _xRotation = Mathf.Clamp(_xRotation, -35f, 55f);

        // 카메라의 로컬 회전 적용
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        // 플레이어 몸체의 y축 회전 적용 (좌우 움직임)
        playerBody.Rotate(Vector3.up * mouseX);
    }   
}
