using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IHaveId
    {
        public int Id { get; set; }
    }
}
