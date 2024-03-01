using MadeYellow.FSM;

namespace MadeYellow.Character2D.Basic.StateMachine.States.Abstractions
{
    public abstract class BasicCharacter2DStateBase : StateBase
    {
        protected readonly BasicCharacter2DStateMachine FiniteStateMachine;

        protected BasicCharacter2DStateBase(BasicCharacter2DStateMachine fsm)
        {
            FiniteStateMachine = fsm;
        }
    }
}