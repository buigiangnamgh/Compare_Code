using System.Collections.Generic;
using EMR_MAIN;
using EMR_MAIN.DATABASE.BenhAn;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute
{
	public class ERMADO
	{
		public string KyDienTu_DiaChiACS = "";

		public string KyDienTu_DiaChiEMR = "";

		public string KyDienTu_DiaChiThuVienKy = "";

		public string KyDienTu_ApplicationCode = "HIS";

		public string KyDienTu_TREATMENT_CODE = "";

		public LoaiBenhAnEMR _LoaiBenhAnEMR_s { get; set; }

		public HanhChinhBenhNhan _HanhChinhBenhNhan_s { get; set; }

		public ThongTinDieuTri _ThongTinDieuTri_s { get; set; }

		public BenhAnBong _BenhAnBong_s { get; set; }

		public BenhAnDaLieu _BenhAnDaLieu_s { get; set; }

		public BenhAnHuyetHocTruyenMau _BenhAnHuyetHocTruyenMau_s { get; set; }

		public BenhAnMatBanPhanTruoc _BenhAnMatBanPhanTruoc_s { get; set; }

		public BenhAnMatChanThuong _BenhAnMatChanThuong_s { get; set; }

		public BenhAnMatDayMat _BenhAnMatDayMat_s { get; set; }

		public BenhAnMatGlocom _BenhAnMatGlocom_s { get; set; }

		public BenhAnMatLac _BenhAnMatLac_s { get; set; }

		public BenhAnMatTreEm _BenhAnMatTreEm_s { get; set; }

		public BenhAnDieuTriBanNgay _BenhAnDieuTriBanNgay_s { get; set; }

		public BenhAnNgoaiKhoa _BenhAnNgoaiKhoa_s { get; set; }

		public BenhAnNgoaiTru _BenhAnNgoaiTru_s { get; set; }

		public BenhAnNgoaiTruRangHamMat _BenhAnNgoaiTruRangHamMat_s { get; set; }

		public BenhAnNgoaiTruTaiMuiHong _BenhAnNgoaiTruTaiMuiHong_s { get; set; }

		public BenhAnNgoaiTruYHCT _BenhAnNgoaiTruYHCT_s { get; set; }

		public BenhAnNhiKhoa _BenhAnNhiKhoa_s { get; set; }

		public BenhAnNoiKhoa _BenhAnNoiKhoa_s { get; set; }

		public BenhAnNoiTruYHCT _BenhAnNoiTruYHCT_s { get; set; }

		public BenhAnPhuKhoa _BenhAnPhuKhoa_s { get; set; }

		public BenhAnPhucHoiChucNang _BenhAnPhucHoiChucNang_s { get; set; }

		public BenhAnRangHamMat _BenhAnRangHamMat_s { get; set; }

		public BenhAnSanKhoa _BenhAnSanKhoa_s { get; set; }

		public BenhAnSoSinh _BenhAnSoSinh_s { get; set; }

		public BenhAnTaiMuiHong _BenhAnTaiMuiHong_s { get; set; }

		public BenhAnTamThan _BenhAnTamThan_s { get; set; }

		public BenhAnTruyenNhiem _BenhAnTruyenNhiem_s { get; set; }

		public BenhAnUngBuou _BenhAnUngBuou_s { get; set; }

		public BenhAnXaPhuong _BenhAnXaPhuong_s { get; set; }

		public List<PhauThuatThuThuat_HIS> PhauThuatThuThuat_HIS_s { get; set; }

		public BenhAnTim _BenhAnTim_s { get; set; }
	}
}
