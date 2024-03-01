using MadeYellow.Character2D.Basic.StateMachine.States;
using MadeYellow.Character2D.Basic.StateMachine.States.Abstractions;
using MadeYellow.FSM;
using UnityEngine;

namespace MadeYellow.Character2D.Basic.StateMachine
{
    [System.Serializable]
    public class BasicCharacter2DStateMachine : FiniteStateMachineBase<BasicCharacter2DStateBase>
    {
        [SerializeField]
        private BasicCharacter2DStateMachineConfig _machineConfig;
        public BasicCharacter2DStateMachineConfig MachineConfig => _machineConfig;

        public readonly BasicCharacter2DLocomotionState Locomotion;

        public Vector2 MotionSmoothVelocity { get; set; }
        public Vector2 PassVelocity { get; set; }
        public Vector2 MotionInput { get; set; }

        public BasicCharacter2DStateMachine()
        {
            Locomotion = new BasicCharacter2DLocomotionState(this);
        }
    }
}
