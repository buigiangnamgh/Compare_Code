using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;
using EMR.WCF.DCO;

namespace Inventec.Common.SignLibrary.ServiceSign
{
	[ServiceContract(ConfigurationName = "ServiceSign.ISignProcessor", SessionMode = SessionMode.Required)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public interface ISignProcessor
	{
		[OperationContract(Action = "http://tempuri.org/ISignProcessor/SignExecute", ReplyAction = "http://tempuri.org/ISignProcessor/SignExecuteResponse")]
		WcfSignResultDCO SignExecute(string data);

		[OperationContract(Action = "http://tempuri.org/ISignProcessor/SignExecute", ReplyAction = "http://tempuri.org/ISignProcessor/SignExecuteResponse")]
		Task<WcfSignResultDCO> SignExecuteAsync(string data);

		[OperationContract(Action = "http://tempuri.org/ISignProcessor/GetSerialNumber", ReplyAction = "http://tempuri.org/ISignProcessor/GetSerialNumberResponse")]
		WcfSignResultDCO GetSerialNumber(string data);

		[OperationContract(Action = "http://tempuri.org/ISignProcessor/GetSerialNumber", ReplyAction = "http://tempuri.org/ISignProcessor/GetSerialNumberResponse")]
		Task<WcfSignResultDCO> GetSerialNumberAsync(string data);

		[OperationContract(Action = "http://tempuri.org/ISignProcessor/StringBase64SignXml", ReplyAction = "http://tempuri.org/ISignProcessor/StringBase64SignXmlResponse")]
		string StringBase64SignXml(string xmlData, string elementName, string serialNumber);

		[OperationContract(Action = "http://tempuri.org/ISignProcessor/StringBase64SignXml", ReplyAction = "http://tempuri.org/ISignProcessor/StringBase64SignXmlResponse")]
		Task<string> StringBase64SignXmlAsync(string xmlData, string elementName, string serialNumber);

		[OperationContract(Action = "http://tempuri.org/ISignProcessor/SignXml130", ReplyAction = "http://tempuri.org/ISignProcessor/SignXml130Response")]
		WcfSignResultDCO SignXml130(string data);

		[OperationContract(Action = "http://tempuri.org/ISignProcessor/SignXml130", ReplyAction = "http://tempuri.org/ISignProcessor/SignXml130Response")]
		Task<WcfSignResultDCO> SignXml130Async(string data);
	}
}
