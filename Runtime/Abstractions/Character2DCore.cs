using MadeYellow.FSM;
using UnityEngine;

namespace MadeYellow.Character2D.Abstractions
{
    /// <summary>
    /// Ядро контроллера персонажа
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Character2DCore<TState> : MonoBehaviour where TState : StateBase
    {
        /// <summary>
        /// 2D физический агент
        /// </summary>
        protected Rigidbody2D rBody { get; private set; }

        /// <summary>
        /// Конечный автомат
        /// </summary>
        protected abstract FiniteStateMachineBase<TState> FSM { get; }

        /// <summary>
        /// Текущая реальная скорость персонажа
        /// </summary>
        public Vector2 Velocity => rBody.velocity;

        /// <summary>
        /// Индикатор того движется ли персонаж или нет
        /// </summary>
        public bool IsMoving => !Mathf.Approximately(Velocity.magnitude, 0f);

        /// <summary>
        /// Скорость, которую нужно передать в <see cref="rBody"/>
        /// </summary>
        public Vector2 PassVelocity { get; protected set; }

        private void Awake()
        {
            rBody = GetComponent<Rigidbody2D>();
            rBody.gravityScale = 0f;

            AfterAwake();
        }

        /// <summary>
        /// Хук, вызываемый сразу после <see cref="Awake"/>
        /// </summary>
        protected virtual void AfterAwake()
        {
            
        }

        /// <summary>
        /// Логика ядра будет выполняться в этой функции ТОЛЬКО если физика обситывается в Update
        /// </summary>
        private void Update()
        {
            if (Physics2D.simulationMode != SimulationMode2D.Update)
            {
                return;
            }

            ExecuteUpdate(Time.deltaTime);
        }

        /// <summary>
        /// Логика ядра будет выполняться в этой функции ТОЛЬКО если физика обситывается в FixedUpdate
        /// </summary>
        private void FixedUpdate()
        {
            if (Physics2D.simulationMode != SimulationMode2D.FixedUpdate)
            {
                return;
            }

            ExecuteUpdate(Time.fixedDeltaTime);
        }

        /// <summary>
        /// Вызвать исполнение текущего состояния <see cref="FSM"/>
        /// </summary>
        /// <param name="deltaTime"></param>
        public void ExecuteUpdate(in float deltaTime)
        {
            // Выполнить текущее состояние автомата
            FSM.Execute(deltaTime);

            AfterExecute(deltaTime);

            // Передать вычисленную скорость перемещения
            rBody.velocity = PassVelocity;
        }

        protected virtual void AfterExecute(in float deltaTime) {}
    }
}