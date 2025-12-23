using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using EMR.WCF.DCO;

namespace Inventec.Common.SignLibrary.ServiceSign
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class SignProcessorClient : ClientBase<ISignProcessor>, ISignProcessor
	{
		public SignProcessorClient()
		{
		}

		public SignProcessorClient(string endpointConfigurationName)
			: base(endpointConfigurationName)
		{
		}

		public SignProcessorClient(string endpointConfigurationName, string remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		public SignProcessorClient(string endpointConfigurationName, EndpointAddress remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		public SignProcessorClient(Binding binding, EndpointAddress remoteAddress)
			: base(binding, remoteAddress)
		{
		}

		public WcfSignResultDCO SignExecute(string data)
		{
			return base.Channel.SignExecute(data);
		}

		public Task<WcfSignResultDCO> SignExecuteAsync(string data)
		{
			return base.Channel.SignExecuteAsync(data);
		}

		public WcfSignResultDCO GetSerialNumber(string data)
		{
			return base.Channel.GetSerialNumber(data);
		}

		public Task<WcfSignResultDCO> GetSerialNumberAsync(string data)
		{
			return base.Channel.GetSerialNumberAsync(data);
		}

		public string StringBase64SignXml(string xmlData, string elementName, string serialNumber)
		{
			return base.Channel.StringBase64SignXml(xmlData, elementName, serialNumber);
		}

		public Task<string> StringBase64SignXmlAsync(string xmlData, string elementName, string serialNumber)
		{
			return base.Channel.StringBase64SignXmlAsync(xmlData, elementName, serialNumber);
		}

		public WcfSignResultDCO SignXml130(string data)
		{
			return base.Channel.SignXml130(data);
		}

		public Task<WcfSignResultDCO> SignXml130Async(string data)
		{
			return base.Channel.SignXml130Async(data);
		}
	}
}
