using UnityEngine;

public class PointOfView : MonoBehaviour
{
    private Collider col;
    public Transform playerPos;
    public MonsterPattern monster;
    // FIXME : 컴포넌트 분리 및 MonoBehaviour 제거한 클래스로 변경 필요.
    // public void SetPlayerPos(Transform pos)
    // {
    //     playerPos = pos;
    // }
    //
    // public void SetCollider(Collider other)
    // {
    //     col = other;
    // }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Spider : POV Trigger");
            monster.curPattern.ExitPattern();
            monster.curPattern = monster.patterns[(int)PatternNumber.Pattern.Combat];
            monster.curPattern.EnterPattern(Vector3.zero);
        }
    }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.transform == playerPos)
    //     {
    //         activation = false;
    //     }
    // }
    
    // private void Update(){
    //     if (activation)
    //     {
    //         Vector3 dir = playerPos.position - transform.position + Vector3.up;
    //         Ray ray = new Ray(transform.position, dir);
    //         RaycastHit hitInfo;
    //         if (Physics.Raycast(ray, out hitInfo))
    //         {
    //             if (hitInfo.collider.transform == playerPos)
    //             {
    //                 Debug.Log("Spider : 플레이어 시각 감지");
    //             }
    //         }
    //     }
    // }
}
