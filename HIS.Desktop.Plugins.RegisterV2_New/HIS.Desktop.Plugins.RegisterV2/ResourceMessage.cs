using System;
using System.Reflection;
using System.Resources;
using Inventec.Common.Logging;
using Inventec.Common.Resource;
using Inventec.Desktop.Common.LanguageManager;

namespace HIS.Desktop.Plugins.RegisterV2
{
	internal class ResourceMessage
	{
		internal static ResourceManager languageMessage = new ResourceManager("HIS.Desktop.Plugins.RegisterV2.Resources.Message.Lang", Assembly.GetExecutingAssembly());

		internal static string BenhNhanCoHenKhamVaoNgayTaiPhongKhamY
		{
			get
			{
				try
				{
					return Get.Value("BenhNhanCoHenKhamVaoNgayTaiPhongKhamY", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string NoVienPhi
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_NoVienPhi", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string GoiSangCongBHXHTraVeMaLoi
		{
			get
			{
				try
				{
					return Get.Value("GoiSangCongBHXHTraVeMaLoi", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DichVuDinhKemDichVuChuaCoChinhSachGia
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_DichVuDinhKemDichVuChuaCoChinhSachGia", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string Title_InDichVuKham
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_Title_InDichVuKham", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string Title_InTheBenhNhan
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_Title_InTheBenhNhan", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string Title_InBangKiemTruocTiemChung
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_Title_InBangKiemTruocTiemChung", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string Title_InPhieuYeuCauKham
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_Title_InPhieuYeuCauKham", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TenBNVuotQuaMaxLength
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_TenBNVuotQuaMaxLength", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string HoDemBNVuotQuaMaxLength
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_HoDemBNVuotQuaMaxLength", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string SoTheBHYTKhongHopLe
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_SoTheBHYTKhongHopLe", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string SoTheDaDuocSuDung
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_SoTheDaDuocSuDung", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DotDieuTriGanNhatCuaBenhNhanCoNgayRaLaHomNay
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_DotDieuTriGanNhatCuaBenhNhanCoNgayRaLaHomNay", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DotKhamTruocCuaBenhNhanConNoTienVienPhi
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_DotKhamTruocCuaBenhNhanConNoTienVienPhi", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DotKhamTruocCuaBenhNhanConNoTienVienPhi3
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_DotKhamTruocCuaBenhNhanConNoTienVienPhi3", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThuocCoThoiSuDungDen
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_ThuocCoThoiSuDungDen", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DotKhamTruocCuaBenhNhanCoThuocChuaUongHet
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_DotKhamTruocCuaBenhNhanCoThuocChuaUongHet", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThongBaoKetQuaTimKiemBenhNhanKhiQuetTheDuLieuTraVeNull
		{
			get
			{
				try
				{
					return Get.Value("ThongBaoKetQuaTimKiemBenhNhanKhiQuetTheDuLieuTraVeNull", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string CanhBaoTheBhytSapHatHan
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_CanhBaoTheBhytSapHatHan", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DoiTuongBenhNhanLaBHYTBatBuocPhaiChonLaDungTuyen
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_DoiTuongBenhNhanLaBHYTBatBuocPhaiChonLaDungTuyen", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string MaHoNgheoKhongHopLe
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_MaHoNgheoKhongHopLe", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string NgaySinhKhongDuocNhoHon7
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_NgaySinhKhongDuocNhoHon7", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string NhapGioSinhKhongDung
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_NhapGioSinhKhongDung", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string NhapNgaySinhKhongDungDinhDang
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_NhapNgaySinhKhongDungDinhDang", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThongTinNgaySinhPhaiNhoHonNgayHienTai
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_ThongTinNgaySinhPhaiNhoHonNgayHienTai", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string YeuCauNhapDayDuNgayThangNamSinhVoiBNDuoi6Tuoi
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_YeuCauNhapDayDuNgayThangNamSinhVoiBNDuoi6Tuoi", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ChonPhongThuNganTruocKhiMoTinhNangNay
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_ChonPhongThuNganTruocKhiMoTinhNangNay", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TheBhytChuaDenHanSuDung
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_TheBhytChuaDenHanSuDung", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TheBhytDaHetHanSuDung
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_TheBhytDaHetHanSuDung", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string QuaGioiHanKiemTraTheQuaCongBHXH
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_QuaGioiHanKiemTraTheQuaCongBHXH", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TheDaDuocSuDungTrongNgay
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_TheDaDuocSuDungTrongNgay", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TimDuocMotBenhNhanTheoThongTinNguoiDungNhapNeuKhongPhaiBNCuVuiLongNhanNutBNMoi
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_TimDuocMotBenhNhanTheoThongTinNguoiDungNhapNeuKhongPhaiBNCuVuiLongNhanNutBNMoi", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TimDuocMotBNTheoThongTinNhap
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_TimDuocMotBNTheoThongTinNhap", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TieuDeCuaSoThongBaoLaThongBao
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_TieuDeCuaSoThongBaoLaThongBao", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TieuDeCuaSoThongBaoLaCanhBao
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_TieuDeCuaSoThongBaoLaCanhBao", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ChuaCoThongTinBacSiKham
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_ChuaCoThongTinBacSiKham", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThoiGianYLenhLonHonThoiGianHanDenCuaTheBHYT
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_ThoiGianYLenhLonHonThoiGianHanDenCuaTheBHYT", languageMessage, LanguageManager.GetCulture());
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
					return Get.Value("Plugin_Register_TruongDuLieuBatBuoc", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ThieuTruongDuLieuBatBuoc
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_ThieuTruongDuLieuBatBuoc", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string XuLyThatBai
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_XuLyThatBai", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string TreEmCoGiayKhaiSinhPhaiNhapThongTinHanhChinh
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_TreEmCoGiayKhaiSinhPhaiNhapThongTinHanhChinh", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string CapMaMSThatBaiBanCoMuonTiepTuc
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_CapMaMSThatBaiBanCoMuonTiepTuc", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ChuaNhapCMNDNumberKhiDaCheckCapMaMS
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_ChuaNhapCMNDNumberKhiDaCheckCapMaMS", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ChuaNhapAnhChupCMNDMatTruocMatSauKhiDaCheckCapMaMS
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_ChuaNhapAnhChupCMNDMatTruocMatSauKhiDaCheckCapMaMS", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string ChucNangNayChuaDuocHoTroTrongPhienBanNay
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_ChucNangNayChuaDuocHoTroTrongPhienBanNay", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string NguoiDungInPhieuYeCauKhamKhongCoDuLieuDangKyKham
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_NguoiDungInPhieuYeCauKhamKhongCoDuLieuDangKyKham", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string HeThongTBKetQuaTraVeCuaServerKhongHopLe
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_HeThongTBKetQuaTraVeCuaServerKhongHopLe", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanChuaChonDichVuKham
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_BanChuaChonDichVuKham", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string MotSoThongTinVungThongTinKhacKhongDungDinhDang
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_MotSoThongTinVungThongTinKhacNhapKhongDungDinhDang", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DuLieuRong
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_DuLieuRong", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string MaBenhNhanKhongTontai
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_MaBenhNhanKhongTontai", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string MaHenKhamKhongTontai
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_MaHenKhamKhongTontai", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string MaChuongTrinhKhongTontai
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_MaChuongTrinhKhongTontai", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string SoTheKhongTontai
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_SoTheKhongTontai", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string CanhBaoDichVuDaDuocChiDinhTrongKhoangThoiGianCauHinh
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_CanhBaoDichVuDaDuocChiDinhTrongKhoangThoiGianCauHinh", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanCoMuonNhapThongTinKhuVuc
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_BanCoMuonNhapThongTinKhuVuc", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string VuotQuaLuotKhamBHYTTrongNgay
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_VuotQuaLuotKhamBHYTTrongNgay", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BenhNhanChuaChonCongKham
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_BenhNhanChuaChonCongKham", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string Title_InBienLaiHoaDon
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_Title_InBienLaiHoaDon", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanChuaDuocCapSoHoaDon
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_BanChuaDuocCapSoHoaDon", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanCoMuonThayDoiThongTinBenhNhanTheoCmndCccdHayKhong
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_BanCoMuonThayDoiThongTinBenhNhanTheoCmndCccdHayKhong", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanCoMuonThayDoiThongTinNgayCapNoiCapTheoCmndCccdHayKhong
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_BanCoMuonThayDoiThongTinNgayCapNoiCapTheoCmndCccdHayKhong", languageMessage, LanguageManager.GetCulture());
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
					return Get.Value("Plugin_Register_ThongBao", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanChuaChonPhongThuNgan
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_BanChuaChonPhongThuNgan", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanChuaChonSoTamUng
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_BanChuaChonSoTamUng", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string BanChuaChonHinhThucGiaoDich
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_BanChuaChonHinhThucGiaoDich", languageMessage, LanguageManager.GetCulture());
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				return "";
			}
		}

		internal static string DoiDichVuHenKhamSeDoiSttDaCap
		{
			get
			{
				try
				{
					return Get.Value("Plugin_Register_DoiDichVuHenKhamSeDoiSttDaCap", languageMessage, LanguageManager.GetCulture());
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
