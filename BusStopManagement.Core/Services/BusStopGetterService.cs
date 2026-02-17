using BusStopManagement.Core.DTO;
using BusStopManagement.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusStopManagement.Core.Services
{
    public class BusStopGetterService : IBusStopGetterService
    {
        public Task<BusStopResponse?> GetBusStopByBusStopID(Guid? busStopID)
        {
            throw new NotImplementedException();
        }

        public Task<BusStopResponse?> GetBusStopByBusStopName(string busStopName)
        {
            throw new NotImplementedException();
        }

        public Task<List<BusStopResponse>> GetBusStops()
        {
            throw new NotImplementedException();
        }
    }
}
