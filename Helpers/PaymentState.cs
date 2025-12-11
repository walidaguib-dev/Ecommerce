using Ecommerce.Models;
using Stateless;

namespace Ecommerce.Helpers
{
    public class PaymentState
    {
        private StateMachine<PaymentStatus, string> _machine;
        public PaymentState(Payments payments)
        {
            _machine = new StateMachine<PaymentStatus, string>(payments.Status);

            _machine.Configure(PaymentStatus.Pending)
               .Permit("PaymentCompleted", PaymentStatus.Completed)
               .Permit("PaymentFailed", PaymentStatus.Failed);
            _machine.Configure(PaymentStatus.Completed)
               .Permit("PaymentCancelled", PaymentStatus.Cancelled)
               .Permit("PaymentRefunded", PaymentStatus.Refunded);
        }

        public void TriggerEvent(string trigger)
        {
            _machine.Fire(trigger);

        }
    }
}