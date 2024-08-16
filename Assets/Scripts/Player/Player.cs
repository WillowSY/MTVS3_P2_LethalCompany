using UnityEngine;
using Object = UnityEngine.Object;


public class Player : MonoBehaviour
{
    public Transform cameraTransform;
    public CharacterController controller;
    public Animator animator;
    public Inventory inventory;
    public PlayerRaycast playerRaycast;

    public float speed = 3f; // 기본 이동 속도
    public float runSpeed = 6f; // 달리기 속도
    public float crouchSpeed = 1.5f; // 앉기 속도
    private float _currentSpeed;

    public float jumpSpeed = 7f; // 점프 높이
    public float gravity = -20f; // 중력

    public float crouchHeight = 1f; // 앉기 높이
    public float standHeight = 1.65f; // 서기 높이
    public float crouchCameraHeight = 1f;
    public float standCameraHeight = 1.495f;
    public float yVelocity;

    private bool _isCrouching; // 앉기 상태 확인 변수
    private bool _isRunning; // 달리기 상태 확인 변수
    private bool _isMoving = false;
    private bool _hasItem;

    private StatusController _theStatusController;
    
    public SoundEmitter soundEmitter;
    

    private void Start()
    {
        _theStatusController = Object.FindFirstObjectByType<StatusController>();
    }

    private void Update()
    {
        CameraPosition();
        PlayerHand();
        Jump();
        TryRun();
        TryCrouch();
        Move();
        //Debug.Log("현재속도: " + _currentSpeed);
    }

    private void Weight()
    {
        
    }
    
    private void CameraPosition()
    {
        Vector3 cameraPosition = cameraTransform.localPosition;
        cameraPosition.y = _isCrouching ? crouchCameraHeight : standCameraHeight;
        if (_isCrouching && _isMoving)
        {
            cameraPosition.z = 0.7f;
        }
        else if (_isCrouching && !_isMoving)
        {
            cameraPosition.z = 0.39f;
        }
        else
        {
            cameraPosition.z = 0.17f;
        }
        cameraTransform.localPosition = cameraPosition;
    }
    
    private void Jump()
    {
        if (controller.collisionFlags == CollisionFlags.Below)
        {
            yVelocity = 0f;
            if (Input.GetKeyDown(KeyCode.Space)&& _theStatusController.GetCurrentSP() > 19.2f)
            {
                yVelocity = jumpSpeed;
                _theStatusController.DecreaseStamina(10f);
                animator.SetBool("isJumping",true);
            }
            else
            {
                animator.SetBool("isJumping", false); 
            }
        }
        yVelocity += gravity * Time.deltaTime;
    }
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _theStatusController.GetCurrentSP() > 19.2f)
        {
            if (!_isCrouching)
            {
                Crouch();
            }
            Running();
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift) || _theStatusController.GetCurrentSP() <= 19.2f)
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        if (_isMoving)
        {
            _isRunning = true;
            _theStatusController.DecreaseStamina(10 * Time.deltaTime); // 초당 스태미너 감소
            animator.SetBool("isRunning",true);
        }
        else
        {
            RunningCancel();
        }
    }

    private void RunningCancel()
    {
        _isRunning = false;
        animator.SetBool("isRunning",false);
    }

    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isRunning)
        {
            _isCrouching = !_isCrouching;
            Crouch();
            
        }
    }

    private void Crouch()
    {
        controller.height = _isCrouching ? crouchHeight : standHeight;
        Vector3 center = controller.center;
        center.y = controller.height / 2f;
        controller.center = center;

        if (_isCrouching)
        {
            animator.SetBool("isCrouching",true);
        }
        if(!_isCrouching)
        {
            animator.SetBool("isCrouching",false);
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        Vector3 dir = new Vector3(h, 0f, v).normalized;
        _currentSpeed = _isCrouching ? crouchSpeed : (_isRunning ? runSpeed : speed);
        dir = cameraTransform.TransformDirection(dir) * _currentSpeed;
        dir.y = yVelocity;
        controller.Move(dir * Time.deltaTime);
        
        if (Mathf.Approximately(h, 0f) && Mathf.Approximately(v, 0f))
        {
            animator.SetBool("isWalking",false);
            animator.SetBool("isCrouchWalking",false);
            _isMoving = false;
        }
        else if (_isCrouching)
        {
            animator.SetBool("isWalking",false);
            animator.SetBool("isCrouchWalking",true);
            _isMoving = true;
        }
        else
        {
            animator.SetBool("isCrouchWalking",false);
            animator.SetBool("isWalking",true);
            _isMoving = true;
        }
    }
    
    // FIXME : 추후 거미&개 공통 클래스 상속으로 정리 후 몬스터별 데미지 참조로 변경 필요.
     public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _theStatusController.playerHp -= 30f;
            Debug.Log("player HP : " + _theStatusController.playerHp);
        }
    }

    /*
     * playFootStepSound : 플레이어 발자국 소리 출력.
     * FIXME : 추후 출력 소리 다양해질 시 개별 클래스로 분리 예정.
     */
    private void FootStepSound()
    {
        if (_isMoving && controller.isGrounded && soundEmitter != null)
        {
            soundEmitter.PlayFootStepSound();
        }
    }

    private void PlayerHand()
    {
        if (inventory.currentHeldItem != null && 
            inventory.scraps[playerRaycast.currentQuickSlot].IsTwoHanded)
        {
            animator.SetBool("twoHand", true);
            animator.SetBool("oneHand", false);
        }

        else if (inventory.currentHeldItem != null && 
                 inventory.scraps[playerRaycast.currentQuickSlot].IsShovel == false)
        {
            animator.SetBool("twoHand",false);
            animator.SetBool("oneHand",true);
        }
        else if (inventory.currentHeldItem == null || 
                 inventory.scraps[playerRaycast.currentQuickSlot].IsShovel)
        {
            animator.SetBool("twoHand",false);
            animator.SetBool("oneHand",false);
        }
    }
}