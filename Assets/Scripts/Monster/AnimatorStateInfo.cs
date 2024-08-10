using UnityEngine;

public class AnimatorStateInfo
{
    public Animator animator;

    public string GetStateName(int stateHash, string LayerName)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (Animator.StringToHash(LayerName + "." + clip.name) == stateHash)
            {
                Debug.Log("clip.name : " + clip.name);
                return clip.name;
            }
        }
        return "Unknown";
    }
}
