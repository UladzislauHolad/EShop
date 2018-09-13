using EShop.Services.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Infrastructure
{
    class StatusState
    {
        Dictionary<StateTransition, StatusStates> transitions;
        public StatusStates CurrentState { get; private set; }
        class StateTransition
        {
            readonly StatusStates CurrentState;
            readonly Commands Command;

            public StateTransition(StatusStates currentState, Commands command)
            {
                CurrentState = currentState;
                Command = command;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                StateTransition other = obj as StateTransition;
                return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
            }
        }

        public StatusState(StatusStates state)
        {
            CurrentState = state;
            transitions = new Dictionary<StateTransition, StatusStates>
            {
                { new StateTransition(StatusStates.New, Commands.Confirm), StatusStates.Confirmed },
                { new StateTransition(StatusStates.New, Commands.Pay), StatusStates.Paid },
                { new StateTransition(StatusStates.New, Commands.Delete), StatusStates.Deleted },
                { new StateTransition(StatusStates.Confirmed, Commands.Pay), StatusStates.Paid },
                { new StateTransition(StatusStates.Paid, Commands.Pack), StatusStates.Packed },
                { new StateTransition(StatusStates.Packed, Commands.Complete), StatusStates.Completed },
                { new StateTransition(StatusStates.Packed, Commands.Deliver), StatusStates.OnDelivering },
                { new StateTransition(StatusStates.OnDelivering, Commands.Complete), StatusStates.Completed }
            };
        }

        public StatusStates GetNext(Commands command)
        {
            StateTransition transition = new StateTransition(CurrentState, command);
            StatusStates nextState;
            if (!transitions.TryGetValue(transition, out nextState))
                throw new InvalidOperationException("Invalid transition: " + CurrentState + " -> " + command);
            return nextState;
        }

        public StatusStates MoveNext(Commands command)
        {
            CurrentState = GetNext(command);
            return CurrentState;
        }
    }
}
