using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using BTCh11_Bai3.Models;

namespace BTCh11_Bai3
{
    public partial class FormChuyenDi : Form
    {
        private QL_DaNgoaiContext db;
        private int selectedMaChuyen = 0;
        private string currentLang = "vi-VN"; // Mặc định tiếng Việt
        private readonly string settingsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "QL_DaNgoai",
            "usersettings.json"
        );

        // HttpClient dùng chung theo Task 7 (Timeout 8 giây)
        private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(8) };

        public FormChuyenDi()
        {
            InitializeComponent();

            // Cấu hình User-Agent để API Open-Meteo không chặn kết nối mạng
            if (!httpClient.DefaultRequestHeaders.Contains("User-Agent"))
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
            }

            // Gắn sẵn toàn bộ sự kiện để tránh lỗi click không nhận
            dgvChuyenDi.CellClick += dgvChuyenDi_CellClick;
            btnXemThoiTiet.Click += btnXemThoiTiet_Click;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnTim.Click += btnTim_Click;
            cboNgonNgu.SelectedIndexChanged += cboNgonNgu_SelectedIndexChanged;

            // Đọc chuỗi kết nối trực tiếp từ appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            string connStr = config.GetConnectionString("DefaultConnection")
                             ?? "Server=LAPTOP-APPVC4J1\\SQLEXPRESS;Database=QL_DaNgoai;Trusted_Connection=True;TrustServerCertificate=True;";

            var optionsBuilder = new DbContextOptionsBuilder<QL_DaNgoaiContext>();
            optionsBuilder.UseSqlServer(connStr);
            db = new QL_DaNgoaiContext(optionsBuilder.Options);
        }

        private void FormChuyenDi_Load(object sender, EventArgs e)
        {
            KhoiTaoCacDieuKhien();
            DocCauHinhNgonNgu();
            LoadDanhSachChuyenDi();
        }

        private void KhoiTaoCacDieuKhien()
        {
            cboNgonNgu.Items.Clear();
            cboNgonNgu.Items.AddRange(new object[] { "Tiếng Việt", "English" });

            cboPhuongTien.Items.Clear();
            cboPhuongTien.Items.AddRange(new object[] { "Xe máy", "Ô tô", "Xe khách", "Tàu hỏa" });
            cboPhuongTien.SelectedIndex = 1;

            // Sửa giá trị nhỏ nhất của ngân sách về 0 để không bị lỗi
            nudNganSach.Minimum = 0;
            nudNganSach.Value = 0;

            lblLoiKhuyen.Visible = false;
            prgLoading.Visible = false;
        }

        // ====================== ĐA NGÔN NGỮ (TASK 8) ======================

        private class UserSettings
        {
            public string Language { get; set; } = "vi-VN";
        }

        private void DocCauHinhNgonNgu()
        {
            try
            {
                if (File.Exists(settingsFilePath))
                {
                    string json = File.ReadAllText(settingsFilePath);
                    var settings = JsonSerializer.Deserialize<UserSettings>(json);
                    currentLang = settings?.Language ?? "vi-VN";
                }
            }
            catch { currentLang = "vi-VN"; }

            cboNgonNgu.SelectedIndex = (currentLang == "en-US") ? 1 : 0;
            ApDungNgonNgu();
        }

        private void LuuCauHinhNgonNgu(string lang)
        {
            try
            {
                string dir = Path.GetDirectoryName(settingsFilePath);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                string json = JsonSerializer.Serialize(new UserSettings { Language = lang });
                File.WriteAllText(settingsFilePath, json);
            }
            catch { }
        }

        private void cboNgonNgu_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentLang = (cboNgonNgu.SelectedIndex == 1) ? "en-US" : "vi-VN";
            LuuCauHinhNgonNgu(currentLang);
            ApDungNgonNgu();
            LoadDanhSachChuyenDi();
        }

        private void ApDungNgonNgu()
        {
            bool isEn = (currentLang == "en-US");

            this.Text = isEn ? "Weekend Picnic Planning" : "Kế hoạch dã ngoại cuối tuần";
            label1.Text = isEn ? "Weekend Picnic Planning" : "Kế hoạch dã ngoại cuối tuần";
            lblNgonNgu.Text = isEn ? "Language:" : "Ngôn ngữ:";

            // Khớp đúng với tên biến groupBox1 và label2 -> label7 trong Designer
            groupBox1.Text = isEn ? "TRIP INFORMATION" : "THÔNG TIN CHUYẾN ĐI";
            label2.Text = isEn ? "Trip name" : "Tên chuyến";
            label3.Text = isEn ? "Location" : "Địa điểm";
            label4.Text = isEn ? "Date" : "Ngày đi";
            label5.Text = isEn ? "People" : "Số người";
            label6.Text = isEn ? "Budget" : "Ngân sách";
            label7.Text = isEn ? "Vehicle" : "Phương tiện";
            chkDaHoanThanh.Text = isEn ? "Completed" : "Đã hoàn thành";

            btnThem.Text = isEn ? "Add" : "Thêm";
            btnSua.Text = isEn ? "Edit" : "Sửa";
            btnXoa.Text = isEn ? "Delete" : "Xóa";
            btnLamMoi.Text = isEn ? "Clear" : "Làm mới";
            btnXemThoiTiet.Text = isEn ? "Check Weather" : "Xem thời tiết";
            btnTim.Text = isEn ? "Search" : "Tìm";
            chkLocChuaHoanThanh.Text = isEn ? "Only uncompleted trips" : "Chỉ hiện chuyến chưa hoàn thành";
        }

        // ====================== CRUD & HIỂN THỊ (TASK 2, 3, 4) ======================

        private void LoadDanhSachChuyenDi(List<ChuyenDi> list = null)
        {
            if (list == null) list = db.ChuyenDi.ToList();

            CultureInfo culture = new CultureInfo(currentLang);
            bool isEn = (currentLang == "en-US");

            dgvChuyenDi.Rows.Clear();
            dgvChuyenDi.Columns.Clear();

            dgvChuyenDi.Columns.Add("MaChuyen", isEn ? "ID" : "Mã");
            dgvChuyenDi.Columns.Add("TenChuyen", isEn ? "Trip Name" : "Tên chuyến");
            dgvChuyenDi.Columns.Add("DiaDiem", isEn ? "Location" : "Địa điểm");

            var colNgay = new DataGridViewTextBoxColumn { Name = "NgayDi", HeaderText = isEn ? "Date" : "Ngày đi" };
            dgvChuyenDi.Columns.Add(colNgay);

            dgvChuyenDi.Columns.Add("SoNguoi", isEn ? "People" : "Số người");

            var colNganSach = new DataGridViewTextBoxColumn { Name = "NganSach", HeaderText = isEn ? "Budget" : "Ngân sách" };
            colNganSach.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChuyenDi.Columns.Add(colNganSach);

            dgvChuyenDi.Columns.Add("PhuongTien", isEn ? "Vehicle" : "Phương tiện");

            var colHoanThanh = new DataGridViewCheckBoxColumn { Name = "DaHoanThanh", HeaderText = isEn ? "Completed" : "Hoàn thành" };
            dgvChuyenDi.Columns.Add(colHoanThanh);

            foreach (var c in list)
            {
                // Xử lý kiểu dữ liệu DateOnly hoặc DateTime tùy bản EF Core scaffold
                DateTime ngayHienThi = c.NgayDi is DateOnly d ? d.ToDateTime(TimeOnly.MinValue) : Convert.ToDateTime(c.NgayDi);

                dgvChuyenDi.Rows.Add(
                    c.MaChuyen,
                    c.TenChuyen,
                    c.DiaDiem,
                    ngayHienThi.ToString("d", culture),
                    c.SoNguoi,
                    c.NganSach.ToString("N0", culture),
                    c.PhuongTien,
                    c.DaHoanThanh
                );
            }
        }

        private void dgvChuyenDi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvChuyenDi.Rows.Count) return;

            DataGridViewRow row = dgvChuyenDi.Rows[e.RowIndex];
            if (row.Cells["MaChuyen"].Value == null) return;
            selectedMaChuyen = Convert.ToInt32(row.Cells["MaChuyen"].Value);

            var c = db.ChuyenDi.Find(selectedMaChuyen);
            if (c != null)
            {
                txtTenChuyen.Text = c.TenChuyen;
                txtDiaDiem.Text = c.DiaDiem;

                DateTime ngayDiDate = c.NgayDi is DateOnly d ? d.ToDateTime(TimeOnly.MinValue) : Convert.ToDateTime(c.NgayDi);
                dtpNgayDi.Value = ngayDiDate;

                nudSoNguoi.Value = c.SoNguoi;
                nudNganSach.Value = c.NganSach;
                cboPhuongTien.SelectedItem = c.PhuongTien;
                chkDaHoanThanh.Checked = c.DaHoanThanh;

                // Tự động kiểm tra thời tiết khi chọn dòng
                _ = LayDuBaoThoiTietAsync(c.DiaDiem, ngayDiDate);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            bool isEn = (currentLang == "en-US");

            if (string.IsNullOrWhiteSpace(txtTenChuyen.Text) || string.IsNullOrWhiteSpace(txtDiaDiem.Text))
            {
                MessageBox.Show(isEn ? "Trip name and location cannot be empty!" : "Tên chuyến và địa điểm không được để trống!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpNgayDi.Value.Date < DateTime.Today)
            {
                MessageBox.Show(isEn ? "Departure date cannot be earlier than today!" : "Ngày đi không được nhỏ hơn ngày hôm nay khi thêm mới!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudSoNguoi.Value < 1 || nudNganSach.Value < 0)
            {
                MessageBox.Show(isEn ? "Invalid people count or budget!" : "Số người phải >= 1 và ngân sách >= 0!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChuyenDi c = new ChuyenDi
            {
                TenChuyen = txtTenChuyen.Text.Trim(),
                DiaDiem = txtDiaDiem.Text.Trim(),
                SoNguoi = (int)nudSoNguoi.Value,
                NganSach = nudNganSach.Value,
                PhuongTien = cboPhuongTien.SelectedItem?.ToString() ?? "Ô tô",
                DaHoanThanh = chkDaHoanThanh.Checked
            };

            // Gán NgayDi theo đúng kiểu scaffold của model (DateOnly hoặc DateTime)
            var propNgayDi = typeof(ChuyenDi).GetProperty("NgayDi");
            if (propNgayDi != null && propNgayDi.PropertyType == typeof(DateOnly))
            {
                propNgayDi.SetValue(c, DateOnly.FromDateTime(dtpNgayDi.Value.Date));
            }
            else
            {
                propNgayDi?.SetValue(c, dtpNgayDi.Value.Date);
            }

            db.ChuyenDi.Add(c);
            db.SaveChanges();
            MessageBox.Show(isEn ? "Trip added successfully!" : "Thêm chuyến đi thành công!");
            LoadDanhSachChuyenDi();
            LamMoiForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            bool isEn = (currentLang == "en-US");
            var c = db.ChuyenDi.Find(selectedMaChuyen);
            if (c == null)
            {
                MessageBox.Show(isEn ? "Please select a trip to edit!" : "Vui lòng chọn chuyến đi cần sửa!");
                return;
            }

            c.TenChuyen = txtTenChuyen.Text.Trim();
            c.DiaDiem = txtDiaDiem.Text.Trim();

            var propNgayDi = typeof(ChuyenDi).GetProperty("NgayDi");
            if (propNgayDi != null && propNgayDi.PropertyType == typeof(DateOnly))
            {
                propNgayDi.SetValue(c, DateOnly.FromDateTime(dtpNgayDi.Value.Date));
            }
            else
            {
                propNgayDi?.SetValue(c, dtpNgayDi.Value.Date);
            }

            c.SoNguoi = (int)nudSoNguoi.Value;
            c.NganSach = nudNganSach.Value;
            c.PhuongTien = cboPhuongTien.SelectedItem?.ToString();
            c.DaHoanThanh = chkDaHoanThanh.Checked;

            db.SaveChanges();
            MessageBox.Show(isEn ? "Updated successfully!" : "Cập nhật thành công!");
            LoadDanhSachChuyenDi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            bool isEn = (currentLang == "en-US");
            var c = db.ChuyenDi.Find(selectedMaChuyen);
            if (c == null)
            {
                MessageBox.Show(isEn ? "Please select a trip to delete!" : "Vui lòng chọn chuyến đi cần xóa!");
                return;
            }

            var confirm = MessageBox.Show(isEn ? $"Delete trip '{c.TenChuyen}'?" : $"Xóa chuyến đi '{c.TenChuyen}'?", isEn ? "Confirm" : "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                db.ChuyenDi.Remove(c);
                db.SaveChanges();
                MessageBox.Show(isEn ? "Deleted successfully!" : "Đã xóa chuyến đi!");
                LoadDanhSachChuyenDi();
                LamMoiForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
            LoadDanhSachChuyenDi();
        }

        private void LamMoiForm()
        {
            selectedMaChuyen = 0;
            txtTenChuyen.Clear();
            txtDiaDiem.Clear();
            dtpNgayDi.Value = DateTime.Today;
            nudSoNguoi.Value = 1;
            nudNganSach.Value = 0;
            cboPhuongTien.SelectedIndex = 1;
            chkDaHoanThanh.Checked = false;
            pnlThoiTiet.Visible = false;
        }

        // Lọc theo tên chuyến/địa điểm và trạng thái hoàn thành (Đã bỏ lọc khoảng ngày)
        private void btnTim_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();
            if (tuKhoa == "tìm địa điểm...") tuKhoa = "";

            bool chiChuaHoanThanh = chkLocChuaHoanThanh.Checked;

            var q = db.ChuyenDi.AsQueryable();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                q = q.Where(x => x.TenChuyen.ToLower().Contains(tuKhoa) || x.DiaDiem.ToLower().Contains(tuKhoa));
            }

            if (chiChuaHoanThanh)
            {
                q = q.Where(x => !x.DaHoanThanh);
            }

            LoadDanhSachChuyenDi(q.ToList());
        }

        // ====================== TÍCH HỢP API THỜI TIẾT OPEN-METEO (TASK 5, 6, 7) ======================

        private async void btnXemThoiTiet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDiaDiem.Text))
            {
                MessageBox.Show((currentLang == "en-US") ? "Please enter a location!" : "Vui lòng nhập địa điểm!", "Info");
                return;
            }
            await LayDuBaoThoiTietAsync(txtDiaDiem.Text.Trim(), dtpNgayDi.Value.Date);
        }

        private async Task LayDuBaoThoiTietAsync(string diaDiem, DateTime ngayDi)
        {
            bool isEn = (currentLang == "en-US");

            btnXemThoiTiet.Enabled = false;
            prgLoading.Visible = true;
            pnlThoiTiet.Visible = true;
            lblLoiKhuyen.Visible = false;

            lblTieuDeThoiTiet.Text = isEn
                ? $"Forecast for {ngayDi:dd/MM/yyyy} - {diaDiem}"
                : $"Dự báo thời tiết ngày {ngayDi:dd/MM/yyyy} - {diaDiem}";

            int soNgayChenhLech = (ngayDi.Date - DateTime.Today).Days;
            if (soNgayChenhLech < 0 || soNgayChenhLech > 15)
            {
                lblMoTaThoiTiet.Text = isEn ? "No forecast data (Exceeds 16 days)" : "Chưa có dữ liệu dự báo (Quá 16 ngày)";
                lblWeatherIcon.Text = "❓";
                lblNhietDo.Text = "";
                lblXacSuatMua.Text = "";
                prgLoading.Visible = false;
                btnXemThoiTiet.Enabled = true;
                return;
            }

            try
            {
                // Bước 1: Lấy tọa độ (Geocoding API) - Tìm 10 kết quả và ưu tiên lọc kết quả tại Việt Nam (VN)
                string diaDiemKhongDau = BoDauTiengViet(diaDiem).Trim();
                string geoUrl = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(diaDiemKhongDau)}&count=10&language=vi&format=json";
                string geoJson = await httpClient.GetStringAsync(geoUrl);

                using var docGeo = JsonDocument.Parse(geoJson);
                if (!docGeo.RootElement.TryGetProperty("results", out var results) || results.GetArrayLength() == 0)
                {
                    lblMoTaThoiTiet.Text = isEn
                        ? "Location not found! Try without accents (e.g., Da Lat)"
                        : "Không tìm thấy địa danh! Gợi ý: thử tên không dấu như 'Da Lat'";
                    lblWeatherIcon.Text = "📍";
                    lblNhietDo.Text = "";
                    lblXacSuatMua.Text = "";
                    return;
                }

                // Duyệt tìm bản ghi thuộc Việt Nam (country_code = "VN")
                JsonElement targetResult = results[0];
                foreach (var item in results.EnumerateArray())
                {
                    if (item.TryGetProperty("country_code", out var codeProp) &&
                        string.Equals(codeProp.GetString(), "VN", StringComparison.OrdinalIgnoreCase))
                    {
                        targetResult = item;
                        break;
                    }
                }

                double lat = targetResult.GetProperty("latitude").GetDouble();
                double lon = targetResult.GetProperty("longitude").GetDouble();

                // Bước 2: Lấy dữ liệu dự báo theo tọa độ
                string forecastUrl = string.Format(
                    CultureInfo.InvariantCulture,
                    "https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_probability_max&timezone=auto&forecast_days=16",
                    lat, lon
                );
                string forecastJson = await httpClient.GetStringAsync(forecastUrl);

                using var docForecast = JsonDocument.Parse(forecastJson);
                var daily = docForecast.RootElement.GetProperty("daily");
                var times = daily.GetProperty("time").EnumerateArray().Select(x => x.GetString()).ToList();

                string ngayDiStr = ngayDi.ToString("yyyy-MM-dd");
                int index = times.IndexOf(ngayDiStr);

                if (index == -1)
                {
                    lblMoTaThoiTiet.Text = isEn ? "No forecast for this day" : "Không có dữ liệu cho ngày này";
                    return;
                }

                int code = daily.GetProperty("weather_code")[index].GetInt32();
                double minTemp = daily.GetProperty("temperature_2m_min")[index].GetDouble();
                double maxTemp = daily.GetProperty("temperature_2m_max")[index].GetDouble();
                int rainProb = daily.GetProperty("precipitation_probability_max")[index].GetInt32();

                CapNhatGiaoDienThoiTiet(code, minTemp, maxTemp, rainProb, isEn);
            }
            catch (TaskCanceledException ex)
            {
                Log.Warning(ex, "Timeout khi tra cứu thời tiết cho {DiaDiem}", diaDiem);
                MessageBox.Show(isEn ? "Request timeout! Please check your network." : "Hết thời gian chờ kết nối! Vui lòng kiểm tra lại mạng.", "Network Timeout");
            }
            catch (HttpRequestException ex)
            {
                Log.Error(ex, "Lỗi mạng khi kết nối Open-Meteo cho {DiaDiem}", diaDiem);
                MessageBox.Show(isEn ? "Network error: Unable to connect to Open-Meteo." : "Lỗi mạng: Không thể kết nối tới dịch vụ thời tiết.", "Connection Error");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi không xác định khi tra thời tiết");
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                prgLoading.Visible = false;
                btnXemThoiTiet.Enabled = true;
            }
        }

        private void CapNhatGiaoDienThoiTiet(int code, double minTemp, double maxTemp, int rainProb, bool isEn)
        {
            string icon = "☀️";
            string moTa = isEn ? "Clear sky" : "Trời quang";

            if (code >= 1 && code <= 3)
            {
                icon = "⛅";
                moTa = isEn ? "Partly cloudy" : "Có mây";
            }
            else if (code == 45 || code == 48)
            {
                icon = "🌫️";
                moTa = isEn ? "Fog" : "Sương mù";
            }
            else if ((code >= 51 && code <= 67) || (code >= 80 && code <= 82))
            {
                icon = "🌧️";
                moTa = isEn ? "Rainy" : "Mưa";
            }
            else if (code >= 95 && code <= 99)
            {
                icon = "⛈️";
                moTa = isEn ? "Thunderstorm" : "Giông bão";
            }

            lblWeatherIcon.Text = icon;
            lblMoTaThoiTiet.Text = moTa;
            lblNhietDo.Text = isEn
                ? $"Temperature: {minTemp:0.#}°C - {maxTemp:0.#}°C"
                : $"Nhiệt độ: {minTemp:0.#}°C - {maxTemp:0.#}°C";
            lblXacSuatMua.Text = isEn
                ? $"Rain probability: {rainProb}%"
                : $"Khả năng mưa: {rainProb}%";

            if (rainProb >= 60)
            {
                lblLoiKhuyen.Visible = true;
                lblLoiKhuyen.BackColor = Color.Orange;
                lblLoiKhuyen.Text = isEn
                    ? "⚠️ Bring a raincoat or consider rescheduling!"
                    : "⚠️ Nên mang áo mưa hoặc cân nhắc dời lịch";
            }
            else
            {
                lblLoiKhuyen.Visible = false;
            }
        }

        // Hàm chuyển tiếng Việt có dấu thành không dấu chuẩn Unicode
        private string BoDauTiengViet(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;

            var normalizedString = text.Normalize(System.Text.NormalizationForm.FormD);
            var stringBuilder = new System.Text.StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(System.Text.NormalizationForm.FormC).Replace('đ', 'd').Replace('Đ', 'D');
        }
    }
}