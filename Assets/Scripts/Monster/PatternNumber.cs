using UnityEngine;

public static class PatternNumber
{
    // 몬스터 패턴에 대한 열거형 변수 정보
    public enum Pattern
    {
        Idle,       // 대기 : 0
        Patrol,     // 순찰 : 1
        Combat,     // 전투 : 2
        Attack      // 공격 : 3
    }
}
