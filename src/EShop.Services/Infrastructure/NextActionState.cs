using EShop.Services.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Infrastructure
{
    class NextActionState
    {
        Dictionary<StateTransition, Commands> transitions;
        private class StateTransition
        {
            StatusStates StatusState;
            PaymentMethods PaymentMethod;
            DeliveryMethods DeliveryMethod;

            public StateTransition(StatusStates statusStates, PaymentMethods paymentMethod, DeliveryMethods deliveryMethod)
            {
                StatusState = statusStates;
                PaymentMethod = paymentMethod;
                DeliveryMethod = deliveryMethod;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * StatusState.GetHashCode()
                    + PaymentMethod.GetHashCode()
                    + DeliveryMethod.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                StateTransition other = obj as StateTransition;
                return other != null
                    && this.StatusState == other.StatusState
                    && this.PaymentMethod == other.PaymentMethod
                    && this.DeliveryMethod == other.DeliveryMethod;
            }
        }

        public NextActionState()
        {
            transitions = new Dictionary<StateTransition, Commands>
            {
                { new StateTransition(StatusStates.New, PaymentMethods.Cash, DeliveryMethods.Courier), Commands.Pay },
                { new StateTransition(StatusStates.New, PaymentMethods.Cash, DeliveryMethods.Pickup), Commands.Pay },
                { new StateTransition(StatusStates.New, PaymentMethods.Online, DeliveryMethods.Courier), Commands.Confirm },
                { new StateTransition(StatusStates.New, PaymentMethods.Online, DeliveryMethods.Pickup), Commands.Confirm },

                { new StateTransition(StatusStates.Confirmed, PaymentMethods.Online, DeliveryMethods.Courier), Commands.Pay },
                { new StateTransition(StatusStates.Confirmed, PaymentMethods.Online, DeliveryMethods.Pickup), Commands.Pay },

                { new StateTransition(StatusStates.Paid, PaymentMethods.Cash, DeliveryMethods.Courier), Commands.Pack },
                { new StateTransition(StatusStates.Paid, PaymentMethods.Cash, DeliveryMethods.Pickup), Commands.Pack },
                { new StateTransition(StatusStates.Paid, PaymentMethods.Online, DeliveryMethods.Courier), Commands.Pack },
                { new StateTransition(StatusStates.Paid, PaymentMethods.Online, DeliveryMethods.Pickup), Commands.Pack },

                { new StateTransition(StatusStates.Packed, PaymentMethods.Cash, DeliveryMethods.Pickup), Commands.Complete },
                { new StateTransition(StatusStates.Packed, PaymentMethods.Online, DeliveryMethods.Pickup), Commands.Complete },

                { new StateTransition(StatusStates.Packed, PaymentMethods.Online, DeliveryMethods.Courier), Commands.Deliver },
                { new StateTransition(StatusStates.Packed, PaymentMethods.Cash, DeliveryMethods.Courier), Commands.Deliver },

                { new StateTransition(StatusStates.OnDelivering, PaymentMethods.Cash, DeliveryMethods.Courier), Commands.Complete },
                { new StateTransition(StatusStates.OnDelivering, PaymentMethods.Online, DeliveryMethods.Courier), Commands.Complete },

                { new StateTransition(StatusStates.Completed, PaymentMethods.Cash, DeliveryMethods.Courier), Commands.Nothing },
                { new StateTransition(StatusStates.Completed, PaymentMethods.Cash, DeliveryMethods.Pickup), Commands.Nothing },
                { new StateTransition(StatusStates.Completed, PaymentMethods.Online, DeliveryMethods.Courier), Commands.Nothing },
                { new StateTransition(StatusStates.Completed, PaymentMethods.Online, DeliveryMethods.Pickup), Commands.Nothing }
            };
        }

        public Commands GetNext(StatusStates statusStates, PaymentMethods paymentMethod, DeliveryMethods deliveryMethod)
        {
            StateTransition transition = new StateTransition(statusStates, paymentMethod, deliveryMethod);
            Commands nextState;
            if (!transitions.TryGetValue(transition, out nextState))
                throw new Exception($"Invalid transition: {statusStates} {paymentMethod} {deliveryMethod}");
            return nextState;
        }
    }
}
