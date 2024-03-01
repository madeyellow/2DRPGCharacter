using UnityEngine;

namespace MadeYellow.Character2D.Basic
{
    [CreateAssetMenu(menuName = "MadeYellow/RPG Character 2D/FSM Config", fileName = "New RPG Character 2D FSM Config")]
    public class BasicCharacter2DStateMachineConfig : ScriptableObject
    {
        [Header("Locomotion")]

        [Tooltip("This mode will be used when FSM enters Locomotion state")]
        public LocomotionMotionMode LocomotionDefaultMode = LocomotionMotionMode.Regular;

        [Tooltip("Slow, stealth type of motion")]
        public LocomotionMotionConfig LocomotionStealthConfig = new LocomotionMotionConfig();

        [Tooltip("Regular type of motion, that will be used most time of the game")]
        public LocomotionMotionConfig LocomotionRegularConfig = new LocomotionMotionConfig();

        [Tooltip("Rapid, sprint motion")]
        public LocomotionMotionConfig LocomotionSprintConfig = new LocomotionMotionConfig();
    }

    public enum LocomotionMotionMode
    {
        Stealth,
        Regular,
        Sprint
    }

    /// <summary>
    /// Locomotion state motion config
    /// </summary>
    [System.Serializable]
    public class LocomotionMotionConfig
    {
        /// <summary>
        /// Indicates if you can or can't use that type of motion
        /// </summary>
        [Tooltip("Indicates if theLocomotion state may use that mode")]
        public bool IsAllowed = true;

        [Space]

        /// <summary>
        /// Motion speed
        /// </summary>
        [Tooltip("Target value of speed in this mode")]
        [Range(0f, 100f)]
        public float Speed = 10f;

        [Space]

        [Header("Smoothing")]

        [Tooltip("How long (in seconds) it should take for speed to match target value")]
        [Range(0f, 1f)]
        public float SmoothTime = 0.075f;

        [Tooltip("Max value of how much speed may move towards target value")]
        [Range(0.01f, float.MaxValue)]
        public float SmoothSpeedLimit = float.MaxValue;
    }
}