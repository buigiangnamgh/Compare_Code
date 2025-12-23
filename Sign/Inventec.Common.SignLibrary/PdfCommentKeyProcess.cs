using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;

namespace Inventec.Common.SignLibrary
{
	internal class PdfCommentKeyProcess
	{
		internal List<SignPositionADO> Run(string src, ref string outFile)
		{
			List<SignPositionADO> signPositionAutos = new List<SignPositionADO>();
			try
			{
				File.Copy(src, outFile, true);
				signPositionAutos = PdfDocumentProcess.GetPositionWithAutoAddAnnotationBySearchKey(src, outFile, "<SINGLE_KEY__COMMENT_SIGN__");
				if (signPositionAutos != null && signPositionAutos.Count > 0)
				{
					LogSystem.Info("Toa do vi tri key tu dong key trong file template theo key <SINGLE_KEY__COMMENT_SIGN__: " + LogUtil.TraceData(LogUtil.GetMemberName<List<SignPositionADO>>((Expression<Func<List<SignPositionADO>>>)(() => signPositionAutos)), (object)signPositionAutos) + "____outFile:" + outFile);
					PdfDocumentProcess.ReplaceText(signPositionAutos.Select((SignPositionADO o) => o.Text).ToList(), outFile);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return signPositionAutos;
		}

		internal List<SignPositionADO> RunWithUserKey(string src, ref string outFile)
		{
			List<SignPositionADO> signPositionAutos = new List<SignPositionADO>();
			try
			{
				File.Copy(src, outFile, true);
				signPositionAutos = PdfDocumentProcess.GetPositionWithAutoAddAnnotationBySearchKey(src, outFile, "{USER_SIGN_KEY__");
				if (signPositionAutos != null && signPositionAutos.Count > 0)
				{
					LogSystem.Info("Toa do vi tri key tu dong key trong file template theo key {USER_SIGN_KEY__: " + LogUtil.TraceData(LogUtil.GetMemberName<List<SignPositionADO>>((Expression<Func<List<SignPositionADO>>>)(() => signPositionAutos)), (object)signPositionAutos) + "____outFile:" + outFile);
					PdfDocumentProcess.ReplaceText(signPositionAutos.Select((SignPositionADO o) => o.Text).ToList(), outFile);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return signPositionAutos;
		}
	}
}
