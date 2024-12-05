using Raika.Common.SharedKernel.Interfaces;
using Raika.Common.SharedKernel;
using Raika.HomeAlarmPanel.Domain.ParameterObjects.Audit;

namespace Raika.HomeAlarmPanel.Domain.Entities
{
    public class Store : AuditableEntity<Guid>, IAggregateRoot
    {
        //
        // Constructors
        //
        private Store()
        {

        }

        private Store(CreateStoreParameterObject parameterObject)
        {
            
        }

        //
        // Factory Method
        //
        public static Store Create(CreateStoreParameterObject parameterObject) => new Store(parameterObject);

        //
        // Entity Methods
        //

        //
        // Fields
        // 

        //
        // Properties
        //

        //
        // Navigation Properties
        //
    }
}
