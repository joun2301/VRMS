using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private List<Animator> _animators;

    private bool isPlayingSequence = false;

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 콜라이더에서 식별자 정보 가져오기
        ColliderIdentifier identifier = other.GetComponent<ColliderIdentifier>();
        if (identifier != null && !isPlayingSequence)
        {
            StartCoroutine(PlayAnimationSequence(identifier.animationID, 3));
        }
    }

    private System.Collections.IEnumerator PlayAnimationSequence(int animationID, int repeatCount)
    {
        isPlayingSequence = true;

        float clipLength = 1f;
        if (_animators.Count > 0)
        {
            string stateName = animationID.ToString();
            foreach (var clip in _animators[0].runtimeAnimatorController.animationClips)
            {
                if (clip.name == stateName)
                {
                    clipLength = clip.length;
                    break;
                }
            }
        }

        for (int i = 0; i < repeatCount; i++)
        {
            foreach (var animator in _animators)
            {
                animator.SetInteger("AnimationID", animationID);
            }
            yield return new WaitForSeconds(clipLength);
        }

        // 반복 후 기본 애니메이션 상태로 복귀
        foreach (var animator in _animators)
        {
            animator.SetInteger("AnimationID", 0);
        }
        isPlayingSequence = false;
    }
}
