

using Domain.Core;

namespace Domain.Entities
{
    public class Amistades : BaseEntity
    {
        public int UsuarioID1 { get; set; }
        public int UsuarioID2 { get; set; }

        public virtual User Usuario1 { get; set; }
        public virtual User Usuario2 { get; set; }
    }
}
