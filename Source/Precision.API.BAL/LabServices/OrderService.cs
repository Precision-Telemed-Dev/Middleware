using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.BAL.LabServices.Interfaces;
using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;
using System.Text;

namespace Precision.API.BAL.LabServices
{
    public class OrderService : IOrderService
    {
        private readonly ICommonMethods _common;
        public OrderService(ICommonMethods commonMethods)
        {
            _common = commonMethods;
        }
        public async Task<string> GenerateCSV(Order order, string processedFilePath, string id, LabResource fhirResource)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Join(",", "Patient Chart", "Patient First Name", "Patient Middle Initial", "Patient Last Name", "Patient Gender", 
                "Patient DOB", "Care Of", "Patient Address", "Patient Address 2", "Patient City", "Patient State", "Patient Zip", "Patient Email", 
                "Patient Phone", "Patient Race", "Client Number", "Physician Number", "Test Code", "Diagnosis code", "Collection date", "Collection time", 
                "Insurance Number", "Insurance ID", "Source", "Order Comment", "Patient Docchart", "Order Number"));

            sb.AppendLine(string.Join("\",\"", "\""+order.PatientChart, order.PatientFirstName, order.PatientMiddleInitial, order.PatientLastName, order.PatientGender,
                order.PatientDOB, order.CareOf, order.PatientAddress1, order.PatientAddress2, order.PatientCity, order.PatientState, order.PatientZip, order.PatientEmail,
                order.PatientPhone, order.PatientRace, order.ClientNumber, order.PhysicianNumber, order.TestCode, order.DiagnosisCode, order.CollectionDate, order.CollectionTime,
                order.InsuranceNumber, order.InsuranceID, order.Source, order.OrderComment, order.PatientDocChart, order.OrderNumber+"\""));

            return sb.ToString();
        }
    }
}