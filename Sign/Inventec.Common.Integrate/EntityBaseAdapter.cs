using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Inventec.Common.Logging;

namespace Inventec.Common.Integrate
{
	public abstract class EntityBaseAdapter
	{
		protected enum LogType
		{
			Debug,
			Info,
			Warn,
			Error,
			Fatal
		}

		protected string ClassName { get; set; }

		protected string MethodName { get; set; }

		public static string UserName { get; set; }

		protected string ErrorFormat { get; set; }

		protected string Input { get; set; }

		protected string Output { get; set; }

		protected int FrameIndex { get; set; }

		protected EntityBaseAdapter()
		{
			try
			{
				ClassName = GetType().Name;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		protected void LogInOut()
		{
			try
			{
				Logging(new StringBuilder().Append("InputData: ").Append(Input).Append(Environment.NewLine)
					.Append("____OutputData: ")
					.Append(Output)
					.ToString(), LogType.Info);
			}
			catch (Exception ex)
			{
				try
				{
					LogSystem.Error("EntityBase.LogInOut.Exception.", ex);
				}
				catch (Exception)
				{
				}
			}
		}

		protected void LogInOut(string output)
		{
			FrameIndex++;
			LogInOut(output, LogType.Info);
		}

		protected void LogInOut(string output, LogType logType)
		{
			try
			{
				FrameIndex++;
				Logging(new StringBuilder().Append("InputData: ").Append(Input).Append(Environment.NewLine)
					.Append("____OutputData: ")
					.Append(output)
					.ToString(), logType);
			}
			catch (Exception ex)
			{
				try
				{
					LogSystem.Error("EntityBase.LogInOut.Exception.", ex);
				}
				catch (Exception)
				{
				}
			}
		}

		protected void Logging(string message, LogType en)
		{
			try
			{
				FrameIndex++;
				try
				{
					MethodName = string.Format("{0}", new StackTrace().GetFrame(FrameIndex + 2).GetMethod().Name);
					ClassName = string.Format("{0}", new StackTrace().GetFrame(FrameIndex + 2).GetMethod().ReflectedType.FullName);
				}
				catch (Exception ex)
				{
					LogSystem.Error(ex);
				}
				string threadId = GetThreadId();
				message = new StringBuilder().Append(ErrorFormat).Append(string.IsNullOrEmpty(ErrorFormat) ? "" : Environment.NewLine).Append("____")
					.Append(GetInfoProcess())
					.Append(Environment.NewLine)
					.Append("____")
					.Append("UserName: [")
					.Append(UserName)
					.Append("]")
					.Append(Environment.NewLine)
					.Append("____")
					.Append(threadId)
					.Append(Environment.NewLine)
					.Append("____")
					.Append(message)
					.ToString();
				switch (en)
				{
				case LogType.Debug:
					LogSystem.Debug(message);
					break;
				case LogType.Info:
					LogSystem.Info(message);
					break;
				case LogType.Warn:
					LogSystem.Warn(message);
					break;
				case LogType.Error:
					LogSystem.Error(message);
					break;
				case LogType.Fatal:
					LogSystem.Fatal(message);
					break;
				}
			}
			catch (Exception ex2)
			{
				try
				{
					LogSystem.Error("EntityBase.Logging.Exception.", ex2);
				}
				catch (Exception)
				{
				}
			}
		}

		protected string GetInfoProcess()
		{
			try
			{
				FrameIndex++;
				return new StringBuilder().Append("TraceInfo: [").Append("Class: ").Append(string.IsNullOrWhiteSpace(GetClassName()) ? "" : (GetClassName() + "; "))
					.Append("MethodName: ")
					.Append(string.IsNullOrWhiteSpace(GetMethodName()) ? "" : (GetMethodName() + "; "))
					.Append("LineNumber: ")
					.Append(string.IsNullOrWhiteSpace(GetLineNumber().ToString()) ? "" : GetLineNumber().ToString())
					.Append("]")
					.ToString();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return "";
			}
		}

		protected string GetClassName()
		{
			StackTrace stackTrace = new StackTrace();
			return stackTrace.GetFrame(FrameIndex + 2).GetMethod().ReflectedType.FullName;
		}

		protected string GetMethodName()
		{
			StackTrace stackTrace = new StackTrace();
			return stackTrace.GetFrame(FrameIndex + 2).GetMethod().Name;
		}

		protected int GetLineNumber()
		{
			StackTrace stackTrace = new StackTrace();
			return stackTrace.GetFrame(FrameIndex + 2).GetFileLineNumber();
		}

		protected static string GetThreadId()
		{
			try
			{
				return "ThreadId: " + Thread.CurrentThread.ManagedThreadId;
			}
			catch (Exception ex)
			{
				LogSystem.Error("EntityBase.GetThreadId.Exception.", ex);
				return "";
			}
		}

		protected bool IsNotNull(object data)
		{
			bool flag = false;
			try
			{
				return data != null;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return false;
			}
		}

		protected bool IsNotNullOrEmpty(string data)
		{
			bool flag = false;
			try
			{
				return !string.IsNullOrEmpty(data);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return false;
			}
		}

		protected bool IsNotNullOrEmpty(ICollection listData)
		{
			bool flag = false;
			try
			{
				return listData != null && listData.Count > 0;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return false;
			}
		}

		protected bool IsGreaterThanZero(long id)
		{
			bool flag = false;
			try
			{
				return id > 0;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return false;
			}
		}
	}
}
