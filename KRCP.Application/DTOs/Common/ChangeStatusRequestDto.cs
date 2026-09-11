using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Common
{
    // este dto generico solo sera utilizado por los controllers, no service ni interface
    public class ChangeStatusRequestDto
    {
        public required bool Activo { get; set; }
    }
}
