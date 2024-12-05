using System.ComponentModel.DataAnnotations.Schema;

namespace Raika.Common.SharedKernel
{
    public abstract class EntityBase<EntityKeyType> : IEquatable<EntityBase<EntityKeyType>>
    {
        public EntityKeyType Id { get; set; }
        //
        // Overrides and Operators
        //
        public bool Equals(EntityBase<EntityKeyType> otherEntity)
        {
            if (otherEntity is null)
                return false;
            if (otherEntity.GetType() != typeof(EntityBase<EntityKeyType>))
                return false;
            return true;
        }
        public static bool operator ==(EntityBase<EntityKeyType> left, EntityBase<EntityKeyType> right)
        {
            return EqualityComparer<EntityBase<EntityKeyType>>.Default.Equals(left, right);
        }
        public static bool operator !=(EntityBase<EntityKeyType> left, EntityBase<EntityKeyType> right)
        {
            return !(left == right);
        }
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() != typeof(EntityBase<EntityKeyType>))
                return false;
            if (obj is not EntityBase<EntityKeyType> entity)
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        //
        // Domain Event Functionality
        //
        private List<DomainEventBase> _domainEvents = new();
        [NotMapped]
        public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();
        public void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
        internal void ClearDomainEvents() => _domainEvents.Clear();
        //
        //
        protected abstract void Validate();
    }
}
