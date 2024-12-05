namespace Raika.Common.SharedKernel
{
    public class AuditableEntity<EntityKeyType> : EntityBase<EntityKeyType>
    {
        public Guid CreatedBy { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public Guid? ModifiedBy { get; protected set; }
        public DateTime? ModifiedAt { get; protected set; }
        public bool IsDeleted { get; protected set; } = false;

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
