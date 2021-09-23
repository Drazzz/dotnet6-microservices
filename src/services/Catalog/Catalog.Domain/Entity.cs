using BuildingBlocks.Common.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Catalog.Domain
{
    public abstract class Entity
    {
        [Key] public Guid Id { get; internal set; }

        public static bool operator ==(Entity left, Entity right) => left?.CompareObjects(right) ?? right is null;

        public static bool operator !=(Entity left, Entity right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
                return false;

            return this == (Entity)obj;
        }

        public override int GetHashCode()
        {
            var xType = GetType();
            string stringToHashCode = string.Empty;
            foreach (var prop in xType.GetProperties())
                stringToHashCode.Concat(prop.GetValue(this, null)?.ToString()?? string.Empty);

            return stringToHashCode.GetHashCode();
        }
    }
}
