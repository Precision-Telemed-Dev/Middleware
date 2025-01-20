using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.BAL.LabServices.Interfaces;
using Precision.API.BAL.PharmacyServices.Interfaces;
using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;
using System.Text;

namespace Precision.API.BAL.PharmacyServices
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly ICommonMethods _common;
        public PrescriptionService(ICommonMethods commonMethods)
        {
            _common = commonMethods;
        }
    }
}