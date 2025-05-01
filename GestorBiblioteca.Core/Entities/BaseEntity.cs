using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBiblioteca.Core.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }
    }
}
