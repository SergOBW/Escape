namespace DefaultNamespace
{
    using DefaultNamespace.Touchable;
    using DG.Tweening;
    using UnityEngine;

    public class AnimatedInterior : MonoBehaviour,ITouchable
    {
        private DOTweenAnimation _doTweenAnimation;

        private void Awake()
        {
            _doTweenAnimation = GetComponentInChildren<DOTweenAnimation>();
        }

        public void Interact()
        {
            Debug.Log("Interact");
            _doTweenAnimation.DOPlayById("rotate");
            Debug.Log(_doTweenAnimation.id);
        }
    }

}