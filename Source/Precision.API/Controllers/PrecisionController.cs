using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.Model.Common;
using Precision.API.Model.Enums;
using Precision.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Precision.API.Model.LabInfo;
using Precision.API.BAL.LabServices.Interfaces;
using Precision.Authorization;
using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;

namespace Precision.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrecisionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICommonMethods _common;
        private readonly IBaseService _baseService;

        string? _path = String.Empty;
        string exceptionFilePath = String.Empty;
        string processedFilePath = String.Empty;

        LabCredential credential = new LabCredential();

        const Module module = Module.Lab;

        public PrecisionController(IConfiguration configuration, ICommonMethods commonMethods, IBaseService baseService, IOrderService orderService)
        {
            _configuration = configuration;
            _common = commonMethods;
            _baseService = baseService;

            _path = _configuration.GetValue<string>("LogPath");

            credential.Username = _configuration.GetValue<string>("LabUsername");
            credential.Password = _configuration.GetValue<string>("LabPassword");
            credential.Mode = _configuration.GetValue<string>("LabMode");
            credential.Url = _configuration.GetValue<string>("LabUrl");

            exceptionFilePath = string.Concat(_path, module.ToString(), "\\Exceptions\\", "Exception_", DateTime.Now.ToString("yyyy-MM-dd"), ".txt");
            processedFilePath = string.Concat(_path, module.ToString(), "\\Processed\\", "Processed_", DateTime.Now.ToString("yyyy-MM-dd"), ".txt");
        }

        [TypeFilter(typeof(AuthorizationFilterAttribute))]
        [HttpPost]
        public async Task<ActionResult> Post([FromHeader] string username, [FromHeader] string password, [FromBody] Order order)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                await _common.CreateOrAppendFile(processedFilePath, String.Concat("------------- "
                    + module.ToString() + " - " + " Started (", DateTime.Now.ToString("yyyy-MM-ddTHHmmss"), ") -------------"));
                await _common.CreateOrAppendFile(processedFilePath, System.Text.Json.JsonSerializer.Serialize(order));

                if (string.IsNullOrEmpty(AuthorizeSession.accessToken))
                    response = await AuthorizeSession.Authorize(credential);

                credential.SessionKey = AuthorizeSession.accessToken;

                //if (response.IsSuccessStatusCode)
                // Save Order

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response = await AuthorizeSession.Authorize(credential);

                    //if (response.IsSuccessStatusCode)
                    // Save Order
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.InnerException == null ? ex.Message.RemoveUselessChars() : ex.InnerException.Message.RemoveUselessChars();
            }

            await _common.CreateOrAppendFile(processedFilePath, string.Concat(DateTime.Now.ToString("yyyy-MM-ddTHHmmss"), " -> ",
                    " StatusCode = ", response.StatusCode, " (", (int)response.StatusCode, "), Content = "
                    , (response.StatusCode != HttpStatusCode.InternalServerError) ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase));

            //if (response.StatusCode == HttpStatusCode.BadRequest)
            //    response.Content = new StringContent(String.Empty);

            return StatusCode(Convert.ToInt32(response.StatusCode)
                , (response.StatusCode != HttpStatusCode.InternalServerError) ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase);
        }

        [TypeFilter(typeof(AuthorizationFilterAttribute))]
        [HttpGet]
        [Route("Pharmacy/ReadStatus/{rxnumber}")]
        public async Task<ActionResult> ReadStatus([FromHeader] string username, [FromHeader] string password, string rxnumber)
        {
            credential.Username = _configuration.GetValue<string>("PharUsername");
            credential.Password = _configuration.GetValue<string>("PharPassword");
            credential.Url = _configuration.GetValue<string>("PharUrl");
            credential.Url = string.Concat(credential.Url, "status/");

            exceptionFilePath = string.Concat(_path, Module.Pharmacy.ToString(), "\\Exceptions\\", "Exception_", DateTime.Now.ToString("yyyy-MM-dd"), ".txt");
            processedFilePath = string.Concat(_path, Module.Pharmacy.ToString(), "\\Processed\\", "Processed_", DateTime.Now.ToString("yyyy-MM-dd"), ".txt");

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                await _common.CreateOrAppendFile(processedFilePath, String.Concat("------------- "
                    + Actions.PharmacyReadStatus.ToString() + " Started (", DateTime.Now.ToString("yyyy-MM-ddTHHmmss"), ") -------------"));
                await _common.CreateOrAppendFile(processedFilePath, String.Concat("RxNumber: ", rxnumber));

                response = await _baseService.Get(processedFilePath, credential, rxnumber, Actions.PharmacyReadStatus);
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.InnerException == null ? ex.Message.RemoveUselessChars() : ex.InnerException.Message.RemoveUselessChars();
            }

            await _common.CreateOrAppendFile(processedFilePath, string.Concat(DateTime.Now.ToString("yyyy-MM-ddTHHmmss"), " -> ",
                    " StatusCode = ", response.StatusCode, " (", (int)response.StatusCode, "), ReasonPhrase = ", response.ReasonPhrase,
                    ", Content = ", response.Content != null ? response.Content.ReadAsStringAsync().Result: string.Empty));

            return StatusCode(Convert.ToInt32(response.StatusCode)
                , (response.IsSuccessStatusCode) ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase);
        }

        [TypeFilter(typeof(AuthorizationFilterAttribute))]
        [HttpPost]
        [Route("Pharmacy/CreateRequest")]
        public async Task<ActionResult> CreateRequest([FromHeader] string username, [FromHeader] string password, [FromBody] Precision.API.Model.PharmacyInfo.PrescriptionOrder order)
        {
            credential.Username = _configuration.GetValue<string>("PharUsername");
            credential.Password = _configuration.GetValue<string>("PharPassword");
            credential.Url = _configuration.GetValue<string>("PharUrl");
            credential.Url = string.Concat(credential.Url, "create/");

            exceptionFilePath = string.Concat(_path, Module.Pharmacy.ToString(), "\\Exceptions\\", "Exception_", DateTime.Now.ToString("yyyy-MM-dd"), ".txt");
            processedFilePath = string.Concat(_path, Module.Pharmacy.ToString(), "\\Processed\\", "Processed_", DateTime.Now.ToString("yyyy-MM-dd"), ".txt");

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                await _common.CreateOrAppendFile(processedFilePath, String.Concat("------------- "
                    + Actions.PharmacyCreateRequest.ToString() + " Started (", DateTime.Now.ToString("yyyy-MM-ddTHHmmss"), ") -------------"));
                await _common.CreateOrAppendFile(processedFilePath, System.Text.Json.JsonSerializer.Serialize(order));

                response = await _baseService.SavePharmacy(processedFilePath, credential, Actions.PharmacyCreateRequest, order);
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.InnerException == null ? ex.Message.RemoveUselessChars() : ex.InnerException.Message.RemoveUselessChars();
            }

            await _common.CreateOrAppendFile(processedFilePath, string.Concat(DateTime.Now.ToString("yyyy-MM-ddTHHmmss"), " -> ",
                    " StatusCode = ", response.StatusCode, " (", (int)response.StatusCode, "), Content = "
                    , (response.StatusCode != HttpStatusCode.InternalServerError) ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase));

            //if (response.StatusCode == HttpStatusCode.BadRequest)
            //    response.Content = new StringContent(String.Empty);

            return StatusCode(Convert.ToInt32(response.StatusCode)
                , (response.StatusCode != HttpStatusCode.InternalServerError) ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase);
        }

        [TypeFilter(typeof(AuthorizationFilterAttribute))]
        [HttpPost]
        [Route("Pharmacy/CreateRefillRequest")]
        public async Task<ActionResult> CreateRefillRequest([FromHeader] string username, [FromHeader] string password, [FromBody] Precision.API.Model.PharmacyInfo.RefillOrder order)
        {
            credential.Username = _configuration.GetValue<string>("PharUsername");
            credential.Password = _configuration.GetValue<string>("PharPassword");
            credential.Url = _configuration.GetValue<string>("PharUrl");
            credential.Url = string.Concat(credential.Url, "refill/");

            exceptionFilePath = string.Concat(_path, Module.Pharmacy.ToString(), "\\Exceptions\\", "Exception_", DateTime.Now.ToString("yyyy-MM-dd"), ".txt");
            processedFilePath = string.Concat(_path, Module.Pharmacy.ToString(), "\\Processed\\", "Processed_", DateTime.Now.ToString("yyyy-MM-dd"), ".txt");

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                await _common.CreateOrAppendFile(processedFilePath, String.Concat("------------- "
                    + Actions.PharmacyRefillRequest.ToString() + " Started (", DateTime.Now.ToString("yyyy-MM-ddTHHmmss"), ") -------------"));
                await _common.CreateOrAppendFile(processedFilePath, System.Text.Json.JsonSerializer.Serialize(order));

                response = await _baseService.SavePharmacy(processedFilePath, credential, Actions.PharmacyRefillRequest, order);
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.InnerException == null ? ex.Message.RemoveUselessChars() : ex.InnerException.Message.RemoveUselessChars();
            }

            await _common.CreateOrAppendFile(processedFilePath, string.Concat(DateTime.Now.ToString("yyyy-MM-ddTHHmmss"), " -> ",
                    " StatusCode = ", response.StatusCode, " (", (int)response.StatusCode, "), Content = "
                    , (response.StatusCode != HttpStatusCode.InternalServerError) ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase));

            //if (response.StatusCode == HttpStatusCode.BadRequest)
            //    response.Content = new StringContent(String.Empty);

            return StatusCode(Convert.ToInt32(response.StatusCode)
                , (response.StatusCode != HttpStatusCode.InternalServerError) ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase);
        }

        [TypeFilter(typeof(AuthorizationFilterAttribute))]
        [HttpDelete]
        [Route("Pharmacy/CancelRequest")]
        public async Task<ActionResult> CancelRequest([FromHeader] string username, [FromHeader] string password, [Required] string rxnumber)
        {
            credential.Username = _configuration.GetValue<string>("PharUsername");
            credential.Password = _configuration.GetValue<string>("PharPassword");
            credential.Url = _configuration.GetValue<string>("PharUrl");
            credential.Url = string.Concat(credential.Url, "transfer-back/");

            exceptionFilePath = string.Concat(_path, Module.Pharmacy.ToString(), "\\Exceptions\\", "Exception_", DateTime.Now.ToString("yyyy-MM-dd"), ".txt");
            processedFilePath = string.Concat(_path, Module.Pharmacy.ToString(), "\\Processed\\", "Processed_", DateTime.Now.ToString("yyyy-MM-dd"), ".txt");

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                await _common.CreateOrAppendFile(processedFilePath, String.Concat("------------- "
                    + Actions.PharmacyCancelRequest.ToString() + " Started (", DateTime.Now.ToString("yyyy-MM-ddTHHmmss"), ") -------------"));
                await _common.CreateOrAppendFile(processedFilePath, String.Concat("RxNumber: ", rxnumber));

                response = await _baseService.SavePharmacy(processedFilePath, credential, Actions.PharmacyCancelRequest, null, rxnumber);
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.InnerException == null ? ex.Message.RemoveUselessChars() : ex.InnerException.Message.RemoveUselessChars();
            }

            await _common.CreateOrAppendFile(processedFilePath, string.Concat(DateTime.Now.ToString("yyyy-MM-ddTHHmmss"), " -> ",
                    " StatusCode = ", response.StatusCode, " (", (int)response.StatusCode, "), Content = "
                    , (response.StatusCode != HttpStatusCode.InternalServerError) ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase));

            response.Content = new StringContent(String.Empty);

            return StatusCode(Convert.ToInt32(response.StatusCode)
                , (response.StatusCode != HttpStatusCode.InternalServerError) ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase);
        }
    }
}