using System;
using HIS.Desktop.LocalStorage.ConfigApplication;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.RegisterV2.Config
{
	internal class AppConfigs
	{
		private const string CONFIG_KEY__TIEP_DON_HIEN_THI_THONG_TIN_THEM = "CONFIG_KEY__TIEP_DON_HIEN_THI_THONG_TIN_THEM";

		private const string CONFIG_KEY__HIEN_THI_NOI_LAM_VIEC_THEO_DINH_DANG_MAN_HINH_DANG_KY = "CONFIG_KEY__HIEN_THI_NOI_LAM_VIEC_THEO_DINH_DANG_MAN_HINH_DANG_KY";

		private const string CONFIG_KEY__CHE_DO_IN_PHIEU_DANG_KY_DICH_VU_KHAM_BENH = "CONFIG_KEY__CHE_DO_IN_PHIEU_DANG_KY_DICH_VU_KHAM_BENH";

		public const string CONFIG_KEY__DEFAULT_CONFIG_PATIENT_TYPE_CODE = "CONFIG_KEY__DEFAULT_CONFIG_PATIENT_TYPE_CODE";

		private const string CONFIG_KEY__FILL_DU_LIEU_TU_DONG_VAO_O_DIA_CHI_BENH_NHAN_CHIP_THE_MAN_HINH_DANG_KY = "CONFIG_KEY__FILL_DU_LIEU_TU_DONG_VAO_O_DIA_CHI_BENH_NHAN_CHIP_THE_MAN_HINH_DANG_KY";

		private const string CONFIG_KEY__ALERT_EXPRIED_TIME_HEIN_CARD_BHYT = "CONFIG_KEY__ALERT_EXPRIED_TIME_HEIN_CARD_BHYT";

		private const string CONFIG_KEY__DANG_KY_TIEP_DON__THU_TIEN_SAU = "CONFIG_KEY__DEFAULT_CONFIG_IS_NOT_REQUIRE_FEE";

		private const string CONFIG_KEY__DANG_KY_TIEP_DON__THOI_GIAN_LOAD_DANH_SACH_PHONG_KHAM = "CONFIG_KEY__DANG_KY_TIEP_DON__THOI_GIAN_LOAD_DANH_SACH_PHONG_KHAM";

		private const string CONFIG_KEY__DANG_KY_TIEP_DON__GOI_BENH_NHAN_BANG_PHAN_MEM_CPA = "CONFIG_KEY__DANG_KY_TIEP_DON__GOI_BENH_NHAN_BANG_CPA";

		private const string CONFIG_KEY__DANG_KY_TIEP_DON__HIEN_THI_THONG_BAO_TIM_THAY_BN_THEO_THONG_TIN_NHAP = "CONFIG_KEY__DANG_KY_TIEP_DON__HIEN_THI_THONG_BAO_TIM_THAY_BN_THEO_THONG_TIN_NHAP";

		private const string CONFIG_KEY__HIS_DESKTOP__REGISTER__OWE_TYPE_DEFAULT = "CONFIG_KEY__HIS_DESKTOP__REGISTER__OWE_TYPE_DEFAULT";

		private const string CONFIG_KEY__HIS_DESKTOP__PLUGINS_AUTO_CHECK_HEIN_DATE_TO = "CONFIG_KEY__HIS_DESKTOP__PLUGINS_AUTO_CHECK_HEIN_DATE_TO";

		private const string CONFIG_KEY__IS_AUTO_FILL_DATA_RECENT_SERVICE_ROOM = "HIS.IS_AUTO_FILL_DATA_RECENT_SERVICE_ROOM";

		private const string CONFIG_KEY__IS_DANG_KY_QUA_TONG_DAI = "HIS.IS_DANG_KY_QUA_TONG_DAI";

		private const string CONFIG_KEY__INSURANCE_EXPERTISE__CHECK_HEIN_CONFIG = "CONFIG_KEY__INSURANCEEXPERTISE_CHECKHEINCONFIG";

		private const string CONFIG_KEY__IS_USE_HID_SYNC = "CONFIG_KEY__IS_USE_HID_SYNC";

		public static string PatientTypeCodeDefault { get; set; }

		public static long AlertExpriedTimeHeinCardBhyt { get; set; }

		public static long CheDoHienThiNoiLamViecManHinhDangKyTiepDon { get; set; }

		public static long TiepDon_HienThiMotSoThongTinThemBenhNhan { get; set; }

		public static string DangKyTiepDonThuTienSau { get; set; }

		public static long DangKyTiepDonThoiGianLoadDanhSachPhongKham { get; set; }

		public static long DangKyTiepDonHienThiThongBaoTimDuocBenhNhan { get; set; }

		public static string DangKyTiepDonGoiBenhNhanBangCPA { get; set; }

		public static long CheDoTuDongFillDuLieuDiaChiGhiTrenTheVaoODiaChiBenhNhanHayKhong { get; set; }

		public static string InsuranceExpertiseCheckHeinConfig { get; set; }

		public static long CheDoInPhieuDangKyDichVuKhamBenh { get; set; }

		public static string OweTypeDefault { get; set; }

		public static long CheDoTuDongCheckThongTinTheBHYT { get; set; }

		public static string IsAutoFillDataRecentServiceRoom { get; set; }

		public static string IsDangKyQuaTongDai { get; set; }

		internal static bool IsVisibleSomeControl { get; set; }

		public static void LoadConfig()
		{
			try
			{
				IsVisibleSomeControl = ConfigApplicationWorker.Get<string>("CONFIG_KEY__TIEP_DON_HIEN_THI_THONG_TIN_THEM") == "1";
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			try
			{
				IsDangKyQuaTongDai = ConfigApplicationWorker.Get<string>("HIS.IS_DANG_KY_QUA_TONG_DAI");
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
			}
			try
			{
				InsuranceExpertiseCheckHeinConfig = ConfigApplicationWorker.Get<string>("CONFIG_KEY__INSURANCEEXPERTISE_CHECKHEINCONFIG");
			}
			catch (Exception ex3)
			{
				LogSystem.Error(ex3);
			}
			try
			{
				IsAutoFillDataRecentServiceRoom = ConfigApplicationWorker.Get<string>("HIS.IS_AUTO_FILL_DATA_RECENT_SERVICE_ROOM");
			}
			catch (Exception ex4)
			{
				LogSystem.Error(ex4);
			}
			try
			{
				PatientTypeCodeDefault = ConfigApplicationWorker.Get<string>("CONFIG_KEY__DEFAULT_CONFIG_PATIENT_TYPE_CODE");
			}
			catch (Exception ex5)
			{
				LogSystem.Error(ex5);
			}
			try
			{
				OweTypeDefault = ConfigApplicationWorker.Get<string>("CONFIG_KEY__HIS_DESKTOP__REGISTER__OWE_TYPE_DEFAULT");
			}
			catch (Exception ex6)
			{
				LogSystem.Error(ex6);
			}
			try
			{
				AlertExpriedTimeHeinCardBhyt = ConfigApplicationWorker.Get<long>("CONFIG_KEY__ALERT_EXPRIED_TIME_HEIN_CARD_BHYT");
			}
			catch (Exception ex7)
			{
				LogSystem.Error(ex7);
			}
			try
			{
				CheDoHienThiNoiLamViecManHinhDangKyTiepDon = ConfigApplicationWorker.Get<long>("CONFIG_KEY__HIEN_THI_NOI_LAM_VIEC_THEO_DINH_DANG_MAN_HINH_DANG_KY");
			}
			catch (Exception ex8)
			{
				LogSystem.Error(ex8);
			}
			try
			{
				TiepDon_HienThiMotSoThongTinThemBenhNhan = ConfigApplicationWorker.Get<long>("CONFIG_KEY__TIEP_DON_HIEN_THI_THONG_TIN_THEM");
			}
			catch (Exception ex9)
			{
				LogSystem.Error(ex9);
			}
			try
			{
				DangKyTiepDonThuTienSau = ConfigApplicationWorker.Get<string>("CONFIG_KEY__DEFAULT_CONFIG_IS_NOT_REQUIRE_FEE");
			}
			catch (Exception ex10)
			{
				LogSystem.Error(ex10);
			}
			try
			{
				DangKyTiepDonThoiGianLoadDanhSachPhongKham = ConfigApplicationWorker.Get<long>("CONFIG_KEY__DANG_KY_TIEP_DON__THOI_GIAN_LOAD_DANH_SACH_PHONG_KHAM");
			}
			catch (Exception ex11)
			{
				LogSystem.Error(ex11);
			}
			try
			{
				DangKyTiepDonHienThiThongBaoTimDuocBenhNhan = ConfigApplicationWorker.Get<long>("CONFIG_KEY__DANG_KY_TIEP_DON__HIEN_THI_THONG_BAO_TIM_THAY_BN_THEO_THONG_TIN_NHAP");
			}
			catch (Exception ex12)
			{
				LogSystem.Error(ex12);
			}
			try
			{
				DangKyTiepDonGoiBenhNhanBangCPA = ConfigApplicationWorker.Get<string>("CONFIG_KEY__DANG_KY_TIEP_DON__GOI_BENH_NHAN_BANG_CPA");
			}
			catch (Exception ex13)
			{
				LogSystem.Error(ex13);
			}
			try
			{
				CheDoTuDongFillDuLieuDiaChiGhiTrenTheVaoODiaChiBenhNhanHayKhong = ConfigApplicationWorker.Get<long>("CONFIG_KEY__FILL_DU_LIEU_TU_DONG_VAO_O_DIA_CHI_BENH_NHAN_CHIP_THE_MAN_HINH_DANG_KY");
			}
			catch (Exception ex14)
			{
				LogSystem.Error(ex14);
			}
			try
			{
				CheDoInPhieuDangKyDichVuKhamBenh = ConfigApplicationWorker.Get<long>("CONFIG_KEY__CHE_DO_IN_PHIEU_DANG_KY_DICH_VU_KHAM_BENH");
			}
			catch (Exception ex15)
			{
				LogSystem.Error(ex15);
			}
			try
			{
				CheDoTuDongCheckThongTinTheBHYT = ConfigApplicationWorker.Get<long>("CONFIG_KEY__HIS_DESKTOP__PLUGINS_AUTO_CHECK_HEIN_DATE_TO");
			}
			catch (Exception ex16)
			{
				LogSystem.Error(ex16);
			}
		}
	}
}
