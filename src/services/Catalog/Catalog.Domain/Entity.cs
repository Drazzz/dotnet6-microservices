using BuildingBlocks.Common.Extensions;

namespace Catalog.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; internal set; }


        public static bool operator ==(Entity left, Entity right) => left?.Equals(right) ?? Equals(right, null);
        public static bool operator !=(Entity left, Entity right) => !(left == right);
        

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Entity)obj;
            return item.Id == Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
