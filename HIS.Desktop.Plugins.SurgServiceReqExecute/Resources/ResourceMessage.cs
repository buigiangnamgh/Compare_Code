using System;
using System.Reflection;
using System.Resources;
using Inventec.Common.Logging;
using Inventec.Common.Resource;
using Inventec.Desktop.Common.LanguageManager;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Resources
{
	internal class ResourceMessage
	{
		private static ResourceManager languageMessage = new ResourceManager("HIS.Desktop.Plugins.SurgServiceReqExecute.Resources.Message.Lang", Assembly.GetExecutingAssembly());

		internal static string ThoiGianYLenhKhongThuocKhoangThoiGianTrongKhoa
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("Plugin_AssignService__ThoiGianYLenhKhongThuocKhoangThoiGianTrongKhoa", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Error(ex);
				}
				return "";
			}
		}

		internal static string ChuaChonNgayChiDinh
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("Plugin_AssignService__ChuaChonNgayChiDinh", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanCoMuonSuaThoiGianYLenhBangThoiGianBatDauPTTT
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("BanCoMuonSuaThoiGianYLenhBangThoiGianBatDauPTTT", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThoiGianKetThucThoiGianRaVien
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("ThoiGianKetThucThoiGianRaVien", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThoiGianBatDauThoiGianRaVien
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("ThoiGianBatDauThoiGianRaVien", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThoiGianKetThucThoiGianVaoVien
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("ThoiGianKetThucThoiGianVaoVien", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThoiGianBatDauThoiGianKetThuc
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("Plugin_SurgServiceReqExecute__ThoiGianBatDauThoiGianKetThuc", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThoiGianBatDauThoiGianVaoVien
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("Plugin_SurgServiceReqExecute__ThoiGianBatDauThoiGianVaoVien", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string KhongTimThayICDTuongUng
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("Plugin_SurgServiceReqExecute__KhongTimThayICDTuongUng", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThoiGianBatDauKhongDuocLonHonThoiGianKetThuc
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("Plugin_SurgServiceReqExecute__ThoiGianBatDauKhongDuocLonHonThoiGianKetThuc", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThoiGianBatDauPhaiLonHonThoiGianYLenh
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("Plugin_SurgServiceReqExecute__ThoiGianBatDauPhaiLonHonThoiGianYLenh", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThoiGianKetThucKhongDuocNhoHonThoiGianBatDau
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("Plugin_SurgServiceReqExecute__ThoiGianKetThucKhongDuocNhoHonThoiGianBatDau", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThoiGianKetThucKhongDuocLonHonThoiGianHeThong
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("Plugin_SurgServiceReqExecute__ThoiGianKetThucKhongDuocLonHonThoiGianHeThong", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TruongDuLieuVuotQuaKyTu
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("TruongDuLieuVuotQuaKyTu", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DichVuThieuThongTinKhongChoKetThucXuLy
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("DichVuThieuThongTinKhongChoKetThucXuLy", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThoiGianKetThucKhongDuocNhoHonThoiGianYLenh
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("ThoiGianKetThucKhongDuocNhoHonThoiGianYLenh", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanChuaChonLuocDo
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("BanChuaChonLuocDo", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string KhongCoNoiDungLuuMau
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("KhongCoNoiDungLuuMau", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThongBao
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("ThongBao", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanChuaNhapThongTinTuongUngVoiCacVaiTRo
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("BanChuaNhapThongTinTuongUngVoiCacVaiTRo", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TaiKhoanDuocThietLapVoiCacVaiTro
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("TaiKhoanDuocThietLapVoiCacVaiTro", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanChuaChonPhuongPhapNao
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("BanChuaChonPhuongPhapNao", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string KhongCoDuLieuMau
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("KhongCoDuLieuMau", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string VuiLongNhapThongTinkipThucHien
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("VuiLongNhapThongTinkipThucHien", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DuLieuEkipTrung
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("DuLieuEkipTrung", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanKhongPhaiLaBacSyKhongDuocKetThuc
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("BanKhongPhaiLaBacSyKhongDuocKetThuc", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DichVuKhongCoThoiGianKetThucKhongChoKetThucXuLy
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("DichVuKhongCoThoiGianKetThucKhongChoKetThucXuLy", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DichVuChuaThucHienKhongChoKetThucXuLy
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("DichVuChuaThucHienKhongChoKetThucXuLy", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string UploadFileThatBaiVuiLongLienHeQuanTriheThongDeDuocHoTro
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("UploadFileThatBaiVuiLongLienHeQuanTriheThongDeDuocHoTro", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string MaBenhChinhVuotQuaKyTuChoPhep
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("MaBenhChinhVuotQuaKyTuChoPhep", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TruongDuLieuBatBuoc
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("TruongDuLieuBatBuoc", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string MaICDKhongDungVuiLongKiemTraLai
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("MaICDKhongDungVuiLongKiemTraLai", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TenBenhChinhVuotQuaKyTuChoPhep
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("TenBenhChinhVuotQuaKyTuChoPhep", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string KhongChoPhepTraKetQuaDichVu_Sau_PhutTinhTuThoiDiemRaYLenh
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("KhongChoPhepTraKetQuaDichVu_Sau_PhutTinhTuThoiDiemRaYLenh", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TraKetQuaDichVu_VuotQua_PhutTinhTuThoiDiemRaYLenh
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("TraKetQuaDichVu_VuotQua_PhutTinhTuThoiDiemRaYLenh", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanCoMuonTiepTucKhong
		{
			get
			{
				try
				{
					return Inventec.Common.Resource.Get.Value("BanCoMuonTiepTucKhong", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}
	}
}
