using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadeYellow.Character2D.Basic.Animators.Abstractions
{
    [RequireComponent(typeof(BasicCharacter2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class BasicCharacter2DAnimatorBase : MonoBehaviour
    {
        public Animator AnimationController { get; private set; }
        public BasicCharacter2D CharacterController { get; private set; }

        public static int IsMovingHash = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            AnimationController = GetComponent<Animator>();
            CharacterController = GetComponent<BasicCharacter2D>();
        }

        private void Update()
        {
            AnimationController.SetBool(IsMovingHash, CharacterController.IsMoving);
        }
    }
}