using Precision.API.Model.Common;
using Precision.API.Model.Enums;

namespace Precision.API.BAL.CommonServices.Interfaces
{
    public interface ICommonMethods
    {        
        Task CreateOrAppendFile(string path, string content);
        Stream GenerateStreamFromString(string s);
    }
}
