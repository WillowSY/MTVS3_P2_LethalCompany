using System.ComponentModel;
using UnityEngine;

public static class MonsterEnums
{
    /* 몬스터 상태에 대한 열거형 변수 */
    public enum State
    {
        Idle,       // 대기 : 0
        Patrol,     // 순찰 : 1
        Combat,     // 전투 : 2
        Attack,     // 공격 : 3
        Escape      // 도망 : 4
    }

    //public static string ToName(this State state) => $"{state}State";
    //public static string ToName(this string s) => $"{s}+++";

    public enum MonsterType
    {
        Spider,
        Flea,
        Dog
    }
}
