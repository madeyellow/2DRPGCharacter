using UnityEngine;
using MadeYellow.Character2D.Basic.StateMachine.States.Abstractions;

namespace MadeYellow.Character2D.Basic.StateMachine.States
{
    /// <summary>
    /// State of regular ground motion
    /// </summary>
    public class BasicCharacter2DLocomotionState : BasicCharacter2DStateBase
    {
        private LocomotionMotionMode _curentMode;
        private LocomotionMotionConfig _currentModeConfig;

        private Vector2 _stateVelocity;

        /// <summary>
        /// Vector using to smoothly translate velocity to target value
        /// </summary>
        private Vector2 _smoothingVelocity;

        private float _smoothTime => _currentModeConfig.SmoothTime;
        private float _smoothSpeed => _currentModeConfig.SmoothSpeedLimit;

        public BasicCharacter2DLocomotionState(BasicCharacter2DStateMachine fsm) : base(fsm)
        {
        }

        protected override void ExecuteHandler(in float deltaTime)
        {
            var targetVelocity = CalculateTargetVelocity();

            _stateVelocity = Vector2.SmoothDamp(_stateVelocity, targetVelocity, ref _smoothingVelocity, _smoothTime, _smoothSpeed, deltaTime); // TODO Механизм плавного КОНТРОЛИРУЕМОГО смещения вектора к целевому значению

            FiniteStateMachine.PassVelocity = _stateVelocity;
            FiniteStateMachine.MotionSmoothVelocity = _smoothingVelocity;
        }

        public override void CheckTransitions()
        {
            // TODO Implement transitions to other states here
        }

        protected override void StateEnteringHook()
        {
            _stateVelocity = FiniteStateMachine.PassVelocity;
            _smoothingVelocity = FiniteStateMachine.MotionSmoothVelocity;

            _currentModeConfig = PickDefaultMotionMode();
        }

        public void UseWalkMode()
        {
            UpdateMode(LocomotionMotionMode.Stealth, FiniteStateMachine.MachineConfig.LocomotionStealthConfig);
        }

        public void UseRegularMode()
        {
            UpdateMode(LocomotionMotionMode.Regular, FiniteStateMachine.MachineConfig.LocomotionRegularConfig);
        }

        public void UseSprintMode()
        {
            UpdateMode(LocomotionMotionMode.Sprint, FiniteStateMachine.MachineConfig.LocomotionSprintConfig);
        }

        public void UseDefaultMode()
        {
            UpdateMode(FiniteStateMachine.MachineConfig.LocomotionDefaultMode, PickDefaultMotionMode());
        }

        private void UpdateMode(LocomotionMotionMode mode, LocomotionMotionConfig config)
        {
            if (config == null || !config.IsAllowed)
            {
                return;
            }

            _curentMode = mode;
            _currentModeConfig = config;
        }

        private LocomotionMotionConfig PickDefaultMotionMode()
        {
            _curentMode = FiniteStateMachine.MachineConfig.LocomotionDefaultMode;

            switch (_curentMode)
            {
                case LocomotionMotionMode.Stealth:
                    return FiniteStateMachine.MachineConfig.LocomotionStealthConfig;

                case LocomotionMotionMode.Regular:
                    return FiniteStateMachine.MachineConfig.LocomotionRegularConfig;

                case LocomotionMotionMode.Sprint:
                    return FiniteStateMachine.MachineConfig.LocomotionSprintConfig;

                default: throw new UnityException("Locomotion mode doesn't exists!");
            }
        }

        private Vector2 CalculateTargetVelocity()
        {
            return (_currentModeConfig != null) ? FiniteStateMachine.MotionInput * _currentModeConfig.Speed : Vector2.zero;
        }
    }
}
