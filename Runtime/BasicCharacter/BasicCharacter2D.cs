using MadeYellow.Character2D.Abstractions;
using MadeYellow.Character2D.Basic.StateMachine;
using MadeYellow.Character2D.Basic.StateMachine.States.Abstractions;
using MadeYellow.FSM;
using UnityEngine;

namespace MadeYellow.Character2D.Basic
{
    /// <summary>
    /// Redefinable 2D character
    /// </summary>
    public class BasicCharacter2D : Character2DCore<BasicCharacter2DStateBase>
    {
        #region FSM
        /// <summary>
        /// Editor customizable finite state machine
        /// </summary>
        [SerializeField]
        private BasicCharacter2DStateMachine _fsm;

        public BasicCharacter2DStateMachine FiniteStateMachine => _fsm;
        protected override FiniteStateMachineBase<BasicCharacter2DStateBase> FSM => _fsm;
        #endregion

        #region Inputs
        /// <summary>
        /// Motion direction input
        /// </summary>
        public Vector2 MotionInput => _fsm.MotionInput;

        /// <summary>
        /// Direction action input (e.g. attack, dash, etc.)
        /// </summary>
        public Vector2 DirectionInput { get; private set; }
        #endregion

        protected override void AfterAwake()
        {
            _fsm.ChangeState(_fsm.Locomotion);
        }

        protected override void AfterExecute(in float deltaTime)
        {
            PassVelocity = _fsm.PassVelocity;
        }

        /// <summary>
        /// Tells character to move in certain direction
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Vector2 direction)
        {
            _fsm.MotionInput = direction;
        }
    }
}