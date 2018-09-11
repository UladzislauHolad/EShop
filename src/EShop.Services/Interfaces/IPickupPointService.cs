using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Interfaces
{
    public interface IPickupPointService
    {
        IEnumerable<PickupPointDTO> GetPickupPoints();
    }
}
