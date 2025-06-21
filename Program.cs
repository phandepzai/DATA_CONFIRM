// Program.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Diagnostics;

namespace DATA_CONFIRM
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles(); // Kích hoạt kiểu giao diện hiện đại
            Application.SetCompatibleTextRenderingDefault(false); // Cấu hình chế độ vẽ văn bản
            Application.Run(new MainForm()); // Chạy form chính
        }
    }

    public class MainForm : Form
    {
        // Danh sách các lỗi có thể chọn
        private static readonly string[] ErrorNames = new string[]
        {
            "Lỗi 01",
            "Lỗi 02",
            "Lỗi 03",
            "Lỗi 04",
            "Lỗi 05",
            "Lỗi 06",
            "Lỗi 07",
            "Lỗi 08",
            "Lỗi 09",
            "Lỗi 10",
            "Lỗi 11",
            "Lỗi 12",
            "Lỗi 13",
            "Lỗi 14",
            "Lỗi 15",
            "Lỗi 16",
            "Lỗi 17",
            "Lỗi 18",
            "Lỗi 19",
            "Lỗi 20",
            "Lỗi 21",
            "Lỗi 22",
            "Lỗi 23",
            "Lỗi 24",
            "Lỗi 25",
            "Lỗi 26",
            "Lỗi 27",
            "Lỗi 28",
            "Lỗi 29",
            "Lỗi 30",
            "Lỗi 31",
            "Lỗi 32",
            "Lỗi 33",
            "Lỗi 34",
            "Lỗi 35",
            "Lỗi 36",
            "Lỗi 37",
            "Lỗi 38",
            "Lỗi 39",
            "Lỗi 40",
            "Lỗi 41",
            "Lỗi 42",
            "Lỗi 43",
            "Lỗi 44",
            "Lỗi 45",
            "Lỗi 46",
            "Lỗi 47",
            "Lỗi 48",
            "Lỗi 49",
            "Lỗi 50",
            "Lỗi 51",
            "Lỗi 52",
            "Lỗi 53",
            "Lỗi 54",
            "Lỗi 55",
            "Lỗi 56",
            "Lỗi 57",
            "Lỗi 58",
            "Lỗi 59",
            "Lỗi 60"
        };

        // Danh sách các mẫu (Pattern) có thể chọn
        private static readonly string[] PatternNames = new string[]
        {
            "Pattern 1", "Pattern 2", "Pattern 3", "Pattern 4", "Pattern 5",
            "Pattern 6", "Pattern 7", "Pattern 8", "Pattern 9", "Pattern 10",
            "Pattern 11", "Pattern 12", "Pattern 13", "Pattern 14", "Pattern 15",
            "Pattern 16", "Pattern 17", "Pattern 18", "Pattern 19", "Pattern 20",
            "Pattern 21", "Pattern 22", "Pattern 23", "Pattern 24", "Pattern 25",
            "Pattern 26", "Pattern 27", "Pattern 28", "Pattern 29", "Pattern 30",
            "Pattern 31", "Pattern 32", "Pattern 33", "Pattern 34", "Pattern 35",
            "Pattern 36", "Pattern 37", "Pattern 38", "Pattern 39", "Pattern 40",
            "Pattern 41", "Pattern 42", "Pattern 43", "Pattern 44", "Pattern 45",
            "Pattern 46", "Pattern 47", "Pattern 48", "Pattern 49", "Pattern 50"
        };

        // Danh sách các mức độ (Level) có thể chọn
        private static readonly string[] LevelNames = new string[]
        {
            "Rất yếu", "Yếu", "Vừa", "Mạnh", "Rất Mạnh","Rất nhẹ", "Nhẹ", "Vừa", "Nặng", "Rất nặng",
            "Lv0.1", "Lv0.2", "Lv0.3", "Lv0.4", "Lv0.5", "Lv0.6", "Lv0.7", "Lv0.8", "Lv0.9", "Lv1.0",
            "Lv1.1", "Lv1.2", "Lv1.3", "Lv1.4", "Lv1.5", "Lv1.6", "Lv1.7", "Lv1.8", "Lv1.9", "Lv2.0",
            "Lv2.1", "Lv2.2", "Lv2.3", "Lv2.4", "Lv2.5", "Lv2.6", "Lv2.7", "Lv2.8", "Lv2.9", "Lv3.0",
            "Lv3.1", "Lv3.2", "Lv3.3", "Lv3.4", "Lv3.5", "Lv3.6", "Lv3.7", "Lv3.8", "Lv3.9", "Lv4.0",
            "Lv4.1", "Lv4.2", "Lv4.3", "Lv4.4", "Lv4.5", "Lv4.6", "Lv4.7", "Lv4.8", "Lv4.9", "Lv5.0",
        };

        private TextBox txtAPN; // TextBox để nhập mã APN
        private TextBox[] txtCoordinatesX; // Mảng TextBox để nhập tọa độ X
        private TextBox[] txtCoordinatesY; // Mảng TextBox để nhập tọa độ Y
        private TextBox txtSelectedError; // TextBox hiển thị lỗi đã chọn
        private TextBox txtSelectedPattern; // TextBox hiển thị mẫu (Pattern) đã chọn
        private TextBox txtSelectedLevel; // TextBox hiển thị mức độ (Level) đã chọn
        private TextBox txtSelectedPosition; // TextBox hiển thị vị trí lỗi đã chọn
        private Button btnSelectError; // Nút để mở form chọn lỗi
        private Button btnSelectPattern; // Nút để mở form chọn mẫu
        private Button btnSelectLevel; // Nút để mở form chọn mức độ
        private Button btnSelectPosition; // Nút để mở form chọn vị trí lỗi
        private Button btnSave; // Nút để lưu dữ liệu vào file CSV
        private Button btnReset; // Nút để xóa tất cả dữ liệu nhập
        private Panel numericKeyboard; // Bàn phím số ảo
        private LinkLabel lblStatus; // Nhãn hiển thị trạng thái (thành công/lỗi)
        private Label lblDateTime; // Nhãn hiển thị thời gian lưu gần nhất
        private Label lblAPNCount; // Nhãn hiển thị số lượng cell đã lưu

        // Thêm các biến cho nút DK và BR
        private Button btnDK;
        private Button btnBR;

        public MainForm()
        {
            InitializeComponent(); // Khởi tạo các thành phần giao diện
            InitializeNumericKeyboard(); // Khởi tạo bàn phím số
            UpdateAPNCount(); // Cập nhật số lượng cell đã lưu
            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FORM_NHAP_DATA.icon.ico"))
                {
                    if (stream != null)
                    {
                        this.Icon = new Icon(stream); // Đặt biểu tượng cho form
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài nguyên icon.ico"); // Thông báo nếu không tìm thấy icon
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải biểu tượng: " + ex.Message); // Thông báo lỗi nếu xảy ra
            }
        }

        // Khởi tạo các thành phần giao diện của form chính
        private void InitializeComponent()
        {
            this.Text = "DATA CONFIRM"; // Tiêu đề form
            this.Size = new Size(450, 580); // Kích thước form
            this.StartPosition = FormStartPosition.CenterScreen; // Căn giữa form
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Không cho phép thay đổi kích thước
            this.MaximizeBox = false; // Ẩn nút phóng to

            // Nhãn "APN"
            Label lblAPN = new Label();
            lblAPN.Text = "APN:";
            lblAPN.Location = new Point(20, 18);
            lblAPN.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblAPN.AutoSize = true;
            this.Controls.Add(lblAPN);

            // TextBox nhập mã APN
            txtAPN = new TextBox();
            txtAPN.Location = new Point(60, 10);
            txtAPN.Size = new Size(360, 40);
            txtAPN.Font = new Font("Arial", 15F);
            txtAPN.MaxLength = 300;
            txtAPN.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtCoordinatesX[0].Focus(); // Chuyển focus sang ô X1 khi nhấn Enter
                    e.SuppressKeyPress = true; // Ngăn âm thanh mặc định khi nhấn Enter
                }
            };
            this.Controls.Add(txtAPN);

            // Khởi tạo mảng TextBox cho tọa độ X và Y (4 cặp)
            txtCoordinatesX = new TextBox[4];
            txtCoordinatesY = new TextBox[4];

            for (int i = 0; i < 4; i++)
            {
                // Nhãn cho tọa độ X
                Label lblX = new Label();
                lblX.Text = $"X{i + 1}:";
                lblX.Location = new Point(25, 67 + i * 40);
                lblX.AutoSize = true;
                lblX.Font = new Font("Arial", 9F, FontStyle.Bold);
                this.Controls.Add(lblX);

                // TextBox cho tọa độ X
                txtCoordinatesX[i] = new TextBox();
                txtCoordinatesX[i].Location = new Point(50, 60 + i * 40);
                txtCoordinatesX[i].Size = new Size(60, 40);
                txtCoordinatesX[i].Font = new Font("Arial", 12F, FontStyle.Bold);
                txtCoordinatesX[i].TextAlign = HorizontalAlignment.Center;
                txtCoordinatesX[i].Click += TextBox_Click; // Gán sự kiện click để kích hoạt bàn phím số
                txtCoordinatesX[i].KeyPress += ValidateNumericInput; // Gán sự kiện kiểm tra đầu vào số
                this.Controls.Add(txtCoordinatesX[i]);

                // Nhãn cho tọa độ Y
                Label lblY = new Label();
                lblY.Text = $"Y{i + 1}:";
                lblY.Location = new Point(125, 67 + i * 40);
                lblY.AutoSize = true;
                lblY.Font = new Font("Arial", 9F, FontStyle.Bold);
                this.Controls.Add(lblY);

                // TextBox cho tọa độ Y
                txtCoordinatesY[i] = new TextBox();
                txtCoordinatesY[i].Location = new Point(150, 60 + i * 40);
                txtCoordinatesY[i].Size = new Size(60, 40);
                txtCoordinatesY[i].Font = new Font("Arial", 12F, FontStyle.Bold);
                txtCoordinatesY[i].TextAlign = HorizontalAlignment.Center;
                txtCoordinatesY[i].Click += TextBox_Click; // Gán sự kiện click để kích hoạt bàn phím số
                txtCoordinatesY[i].KeyPress += ValidateNumericInput; // Gán sự kiện kiểm tra đầu vào số
                this.Controls.Add(txtCoordinatesY[i]);
            }

            // Nút để mở form chọn vị trí lỗi
            btnSelectPosition = new Button();
            btnSelectPosition.Text = "VỊ TRÍ LỖI";
            btnSelectPosition.Location = new Point(40, 220);
            btnSelectPosition.Size = new Size(80, 40);
            btnSelectPosition.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnSelectPosition.Click += BtnSelectPosition_Click; // Gán sự kiện click
            this.Controls.Add(btnSelectPosition);

            // TextBox hiển thị vị trí lỗi đã chọn
            txtSelectedPosition = new TextBox();
            txtSelectedPosition.Location = new Point(130, 225);
            txtSelectedPosition.Size = new Size(80, 30);
            txtSelectedPosition.Font = new Font("Arial", 11F, FontStyle.Bold);
            txtSelectedPosition.ReadOnly = true; // Chỉ đọc, giá trị được lấy từ form chọn vị trí
            this.Controls.Add(txtSelectedPosition);

            // Tạo các nút và TextBox cho Tên lỗi, Mức độ, Pattern
            CreateSelectionControls("TÊN LỖI", 260, ref txtSelectedError, ref btnSelectError);
            CreateSelectionControls("LEVEL", 300, ref txtSelectedLevel, ref btnSelectLevel);
            txtSelectedLevel.Width = 165; // Thay đổi 165 thành độ dài mong muốn của bạn
            CreateSelectionControls("PATTERN", 340, ref txtSelectedPattern, ref btnSelectPattern);

            // Nút DK
            btnDK = new Button();
            btnDK.Text = "DK";
            btnDK.Location = new Point(txtSelectedLevel.Location.X + txtSelectedLevel.Width + 10, txtSelectedLevel.Location.Y);
            btnDK.Size = new Size(50, txtSelectedLevel.Height);
            btnDK.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnDK.Click += BtnDK_BR_Click;
            btnDK.BackColor = Color.DimGray; // Đặt màu nền tối cho nút DK (ví dụ: DimGray)
            btnDK.ForeColor = Color.White; // Đặt màu chữ trắng để dễ nhìn trên nền tối
            this.Controls.Add(btnDK);

            // Nút BR
            btnBR = new Button();
            btnBR.Text = "BR";
            btnBR.Location = new Point(btnDK.Location.X + btnDK.Width + 5, txtSelectedLevel.Location.Y);
            btnBR.Size = new Size(50, txtSelectedLevel.Height);
            btnBR.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnBR.Click += BtnDK_BR_Click;
            btnBR.BackColor = Color.LightGray; // Đặt màu nền sáng cho nút BR (ví dụ: LightGray)
            btnBR.ForeColor = Color.Black; // Đặt màu chữ đen để dễ nhìn trên nền sáng
            this.Controls.Add(btnBR);

            // Nút Xác nhận để lưu dữ liệu
            btnSave = new Button();
            btnSave.Text = "XÁC NHẬN";
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.Location = new Point(110, 400);
            btnSave.Size = new Size(100, 40);
            btnSave.Click += BtnSave_Click; // Gán sự kiện click để lưu dữ liệu
            this.Controls.Add(btnSave);

            // Nút Reset để xóa dữ liệu nhập
            btnReset = new Button();
            btnReset.Text = "RESET";
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReset.Location = new Point(230, 400);
            btnReset.Size = new Size(100, 40);
            btnReset.Click += BtnReset_Click; // Gán sự kiện click để xóa dữ liệu
            this.Controls.Add(btnReset);

            // Nhãn hiển thị thời gian lưu gần nhất
            lblDateTime = new Label();
            lblDateTime.Location = new Point(20, 480);
            lblDateTime.Size = new Size(320, 20);
            lblDateTime.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(lblDateTime);

            // Nhãn hiển thị trạng thái (thành công/lỗi)
            lblStatus = new LinkLabel();
            lblStatus.Location = new Point(20, 500);
            lblStatus.Size = new Size(320, 40);
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.AutoSize = false;
            lblStatus.LinkColor = Color.Blue;
            lblStatus.ActiveLinkColor = Color.Red;
            lblStatus.LinkBehavior = LinkBehavior.HoverUnderline;
            lblStatus.LinkClicked += LblStatus_LinkClicked; // Gán sự kiện click để mở thư mục
            this.Controls.Add(lblStatus);

            // Nhãn hiển thị số lượng cell đã lưu
            lblAPNCount = new Label();
            lblAPNCount.Location = new Point(50, 450);
            lblAPNCount.Size = new Size(320, 20);
            lblAPNCount.TextAlign = ContentAlignment.MiddleCenter;
            lblAPNCount.Text = "Số cell đã lưu: 0";
            this.Controls.Add(lblAPNCount);

            // Gán các sự kiện click cho các nút chọn
            btnSelectError.Click += BtnSelectError_Click;
            btnSelectLevel.Click += BtnSelectLevel_Click;
            btnSelectPattern.Click += BtnSelectPattern_Click;

            // Nhãn hiển thị tên tác giả (xoay dọc)
            VerticalLabel lblAuthor = new VerticalLabel("Tác giả: Nông Văn Phấn");
            lblAuthor.Font = new Font("Tahoma", 7F, FontStyle.Regular);
            lblAuthor.ForeColor = Color.Gray;
            lblAuthor.BackColor = Color.Transparent;
            lblAuthor.AutoSize = false;
            lblAuthor.Size = new Size(20, 150);
            lblAuthor.Location = new Point(this.ClientSize.Width - 20, this.ClientSize.Height - 130);
            this.Controls.Add(lblAuthor);
            lblAuthor.BringToFront();
            // Cập nhật vị trí nhãn tác giả khi form thay đổi kích thước
            this.Resize += (s, e) =>
            {
                lblAuthor.Location = new Point(this.ClientSize.Width - 15, this.ClientSize.Height - 140);
            };
        }

        // Tạo nút và TextBox cho các mục chọn (Tên lỗi, Mức độ, Pattern)
        private void CreateSelectionControls(string labelText, int top, ref TextBox textBox, ref Button button)
        {
            button = new Button();
            button.Text = labelText;
            button.Location = new Point(40, top + 5);
            button.Size = new Size(80, 37);
            button.Font = new Font("Arial", 10F);
            this.Controls.Add(button);

            textBox = new TextBox();
            textBox.Location = new Point(130, top + 10);
            textBox.Size = new Size(280, 55);
            textBox.Font = new Font("Arial", 12F);
            textBox.TextAlign = HorizontalAlignment.Left;
            textBox.ReadOnly = true; // Chỉ đọc
            this.Controls.Add(textBox);
        }

        // Khởi tạo bàn phím số ảo
        private void InitializeNumericKeyboard()
        {
            numericKeyboard = new Panel();
            numericKeyboard.Size = new Size(195, 195);
            numericKeyboard.Location = new Point(230, 55);
            numericKeyboard.Visible = true;
            numericKeyboard.BorderStyle = BorderStyle.None;
            numericKeyboard.BackColor = Color.FromArgb(245, 245, 245);
            this.Controls.Add(numericKeyboard);

            string[] numbers = { "7", "8", "9", "4", "5", "6", "1", "2", "3", "0", ".", "Xóa" };
            int buttonWidth = 55;
            int buttonHeight = 40;
            int spacing = 9;

            // Tạo các nút số và nút Xóa
            for (int i = 0; i < numbers.Length; i++)
            {
                Button btn = new Button();
                btn.Text = numbers[i];
                btn.Size = new Size(buttonWidth, buttonHeight);
                btn.Location = new Point(
                    (i % 3) * (buttonWidth + spacing) + 5,
                    (i / 3) * (buttonHeight + spacing) + 5
                );
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(64, 64, 64);
                btn.Font = new Font("Arial", 12F, FontStyle.Bold);
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(240, 240, 240);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
                btn.Cursor = Cursors.Hand;
                btn.Click += NumericButton_Click; // Gán sự kiện click cho nút
                numericKeyboard.Controls.Add(btn);
            }
        }

        // Xử lý sự kiện click vào các nút trên bàn phím số
        private void NumericButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "Xóa")
            {
                if (activeTextBox != null && !string.IsNullOrEmpty(activeTextBox.Text))
                {
                    activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1); // Xóa ký tự cuối
                }
            }
            else
            {
                if (activeTextBox != null)
                {
                    activeTextBox.Text += btn.Text; // Thêm số hoặc dấu chấm vào TextBox
                }
            }
        }

        private TextBox activeTextBox = null; // Lưu TextBox đang được focus

        // Hiển thị form chọn (Tên lỗi, Mức độ, hoặc Pattern)
        private void ShowSelectionForm(string type, TextBox targetTextBox)
        {
            using (Form selectionForm = new Form())
            {
                selectionForm.Text = "CHỌN " + type; // Tiêu đề form
                selectionForm.StartPosition = FormStartPosition.CenterParent;
                selectionForm.ShowInTaskbar = false;
                selectionForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                selectionForm.MaximizeBox = false;
                selectionForm.MinimizeBox = false;
                selectionForm.TopMost = true;

                if (type == "MỨC ĐỘ")
                {
                    // Tạo bảng để hiển thị các mức độ
                    TableLayoutPanel tablePanel = new TableLayoutPanel();
                    tablePanel.Dock = DockStyle.Fill;
                    tablePanel.ColumnCount = 5;
                    tablePanel.RowCount = (int)Math.Ceiling((double)LevelNames.Length / tablePanel.ColumnCount);
                    tablePanel.Padding = new Padding(5);

                    for (int i = 0; i < tablePanel.ColumnCount; i++)
                    {
                        tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / tablePanel.ColumnCount));
                    }
                    for (int i = 0; i < tablePanel.RowCount; i++)
                    {
                        tablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / tablePanel.RowCount));
                    }

                    selectionForm.Controls.Add(tablePanel);
                    selectionForm.Size = new Size(500, tablePanel.RowCount * 50 + 50);

                    int buttonIndex = 0;
                    foreach (string level in LevelNames)
                    {
                        Button levelButton = new Button();
                        levelButton.Text = level;
                        levelButton.Dock = DockStyle.Fill;
                        levelButton.Margin = new Padding(2);
                        levelButton.Font = new Font("Arial", 11F);
                        levelButton.BackColor = Color.White;
                        levelButton.FlatStyle = FlatStyle.Flat;
                        levelButton.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
                        levelButton.Click += (s, e) =>
                        {
                            targetTextBox.Text = levelButton.Text; // Cập nhật TextBox với mức độ được chọn
                            selectionForm.Close();
                        };

                        int row = buttonIndex / tablePanel.ColumnCount;
                        int col = buttonIndex % tablePanel.ColumnCount;
                        tablePanel.Controls.Add(levelButton, col, row);
                        buttonIndex++;
                    }
                }
                else if (type == "PATTERN")
                {
                    int columns = 5;
                    int rows = 10;

                    // Tạo bảng để hiển thị các mẫu (Pattern)
                    TableLayoutPanel tablePanel = new TableLayoutPanel();
                    tablePanel.Dock = DockStyle.Top;
                    tablePanel.Height = 510;
                    tablePanel.ColumnCount = columns;
                    tablePanel.RowCount = rows;
                    tablePanel.Padding = new Padding(5);

                    for (int i = 0; i < columns; i++)
                    {
                        tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                    }
                    for (int i = 0; i < rows; i++)
                    {
                        tablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
                    }

                    selectionForm.Controls.Add(tablePanel);
                    selectionForm.Size = new Size(600, 600);

                    List<string> selectedPatterns = new List<string>();
                    if (!string.IsNullOrEmpty(targetTextBox.Text))
                    {
                        selectedPatterns.AddRange(targetTextBox.Text.Split(',').Select(p => p.Trim()));
                    }

                    for (int i = 0; i < PatternNames.Length; i++)
                    {
                        Button patternButton = new Button();
                        string patternText = PatternNames[i];
                        patternButton.Text = patternText;
                        patternButton.Dock = DockStyle.Fill;
                        patternButton.Margin = new Padding(2);
                        patternButton.Font = new Font("Arial", 10F);
                        patternButton.TextAlign = ContentAlignment.MiddleCenter;
                        patternButton.BackColor = selectedPatterns.Contains(patternText) ? Color.LightBlue : Color.White;
                        patternButton.FlatStyle = FlatStyle.Flat;
                        patternButton.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);

                        patternButton.Click += (s, e) =>
                        {
                            Button clickedButton = (Button)s;
                            if (selectedPatterns.Contains(clickedButton.Text))
                            {
                                selectedPatterns.Remove(clickedButton.Text); // Xóa mẫu khỏi danh sách
                                clickedButton.BackColor = Color.White;
                            }
                            else
                            {
                                selectedPatterns.Add(clickedButton.Text); // Thêm mẫu vào danh sách
                                clickedButton.BackColor = Color.LightBlue;
                            }
                        };

                        int row = i / columns;
                        int col = i % columns;
                        tablePanel.Controls.Add(patternButton, col, row);
                    }

                    // Nút Xác nhận để lưu các mẫu đã chọn
                    Button btnConfirm = new Button();
                    btnConfirm.Text = "Xác nhận";
                    btnConfirm.Size = new Size(100, 40);
                    btnConfirm.Location = new Point(180, 515);
                    btnConfirm.BackColor = Color.White;
                    btnConfirm.Font = new Font("Arial", 12F);
                    btnConfirm.FlatStyle = FlatStyle.Flat;
                    btnConfirm.FlatAppearance.BorderSize = 1;
                    btnConfirm.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 204);
                    selectionForm.Controls.Add(btnConfirm);

                    // Nút Hủy bỏ để đóng form
                    Button btnCancel = new Button();
                    btnCancel.Text = "Hủy bỏ";
                    btnCancel.Size = new Size(100, 40);
                    btnCancel.Location = new Point(300, 515);
                    btnCancel.BackColor = Color.White;
                    btnCancel.Font = new Font("Arial", 12F);
                    btnCancel.FlatStyle = FlatStyle.Flat;
                    btnCancel.FlatAppearance.BorderSize = 1;
                    btnCancel.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 204);
                    selectionForm.Controls.Add(btnCancel);

                    btnConfirm.Click += (s, e) =>
                    {
                        if (selectedPatterns.Count > 0)
                        {
                            targetTextBox.Text = string.Join(", ", selectedPatterns.OrderBy(p => p)); // Cập nhật TextBox
                        }
                        else
                        {
                            targetTextBox.Text = "";
                        }
                        selectionForm.Close();
                    };

                    btnCancel.Click += (s, e) =>
                    {
                        selectionForm.Close();
                    };
                }
                else if (type == "TÊN LỖI")
                {
                    int columns = 6;
                    int rows = 10;

                    // Tạo bảng để hiển thị các lỗi
                    TableLayoutPanel tablePanel = new TableLayoutPanel();
                    tablePanel.Size = new Size(740, rows * 65);
                    tablePanel.Location = new Point(5, 5);
                    tablePanel.ColumnCount = columns;
                    tablePanel.RowCount = rows;
                    tablePanel.Padding = new Padding(5);

                    for (int i = 0; i < columns; i++)
                    {
                        tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
                    }
                    for (int i = 0; i < rows; i++)
                    {
                        tablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
                    }

                    selectionForm.Controls.Add(tablePanel);
                    selectionForm.Size = new Size(760, (rows * 65) + 50);

                    for (int i = 0; i < ErrorNames.Length; i++)
                    {
                        Button errorButton = new Button();
                        errorButton.Text = ErrorNames[i];
                        errorButton.Size = new Size(110, 60);
                        errorButton.AutoSize = false;
                        errorButton.Margin = new Padding(5);
                        errorButton.Font = new Font("Arial", 9F);
                        errorButton.TextAlign = ContentAlignment.MiddleCenter;
                        errorButton.Padding = new Padding(2);
                        errorButton.BackColor = Color.White;
                        errorButton.FlatStyle = FlatStyle.Flat;
                        errorButton.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
                        errorButton.Click += (s, e) =>
                        {
                            targetTextBox.Text = errorButton.Text; // Cập nhật TextBox với lỗi được chọn
                            selectionForm.Close();
                        };

                        int row = i / columns;
                        int col = i % columns;
                        tablePanel.Controls.Add(errorButton, col, row);
                    }
                }

                selectionForm.ShowDialog(this); // Hiển thị form chọn
            }
        }

        // Xử lý sự kiện click vào nút chọn vị trí lỗi
        private void BtnSelectPosition_Click(object sender, EventArgs e)
        {
            using (PositionSelectionForm positionForm = new PositionSelectionForm())
            {
                if (positionForm.ShowDialog(this) == DialogResult.OK)
                {
                    txtSelectedPosition.Text = string.Join(", ", positionForm.SelectedPositions.OrderBy(p => p)); // Cập nhật TextBox với các vị trí được chọn
                }
            }
        }

        // Cập nhật số lượng cell đã lưu trong file CSV
        private void UpdateAPNCount()
        {
            try
            {
                string fileDate = DateTime.Now.ToString("yyyyMMdd");
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "FAB_CONFIRM");
                string filePath = Path.Combine(folderPath, $"CONFIRM_{fileDate}.csv");

                int count = 0;
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] columns = lines[i].Split(',');
                        if (columns.Length > 0 && !string.IsNullOrWhiteSpace(columns[0].Trim('"')))
                        {
                            count++;
                        }
                    }
                }

                lblAPNCount.Text = $"Số cell đã lưu: {count}";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi khi đếm APN: " + ex.Message;
                lblStatus.LinkArea = new LinkArea(0, 0);
                lblStatus.ForeColor = Color.Red;
            }
        }

        // Xử lý sự kiện lưu dữ liệu vào file CSV
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dữ liệu nào được nhập không
                bool hasData = !string.IsNullOrEmpty(txtAPN.Text) ||
                               txtCoordinatesX.Any(txt => !string.IsNullOrEmpty(txt.Text)) ||
                               txtCoordinatesY.Any(txt => !string.IsNullOrEmpty(txt.Text)) ||
                               !string.IsNullOrEmpty(txtSelectedError.Text) ||
                               !string.IsNullOrEmpty(txtSelectedLevel.Text) ||
                               !string.IsNullOrEmpty(txtSelectedPattern.Text) ||
                               !string.IsNullOrEmpty(txtSelectedPosition.Text);

                if (!hasData)
                {
                    lblStatus.Text = "Vui lòng nhập ít nhất một dữ liệu!";
                    lblStatus.LinkArea = new LinkArea(0, 0);
                    lblStatus.ForeColor = Color.Red;
                    return;
                }

                string currentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                string fileDate = DateTime.Now.ToString("yyyyMMdd");
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "FAB_CONFIRM");
                string filePath = Path.Combine(folderPath, $"CONFIRM_{fileDate}.csv");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath); // Tạo thư mục nếu chưa tồn tại
                }

                // Tạo danh sách các cột dữ liệu để lưu vào CSV
                List<string> csvColumns = new List<string>
                {
                    $"\"{txtAPN.Text.Replace("\"", "\"\"")}\""
                };

                for (int i = 0; i < 4; i++)
                {
                    string xyPair = string.IsNullOrEmpty(txtCoordinatesX[i].Text) && string.IsNullOrEmpty(txtCoordinatesY[i].Text)
                        ? ""
                        : $"{txtCoordinatesX[i].Text},{txtCoordinatesY[i].Text}";
                    csvColumns.Add($"\"{xyPair}\"");
                }
                csvColumns.Add($"\"{txtSelectedError.Text.Replace("\"", "\"\"")}\"");
                csvColumns.Add($"\"{txtSelectedPosition.Text.Replace("\"", "\"\"")}\"");
                csvColumns.Add($"\"{txtSelectedLevel.Text.Replace("\"", "\"\"")}\"");
                csvColumns.Add($"\"{txtSelectedPattern.Text.Replace("\"", "\"\"")}\"");
                csvColumns.Add($"\"{currentTime}\"");

                string csvLine = string.Join(",", csvColumns);

                bool fileExists = File.Exists(filePath);
                using (var stream = new FileStream(filePath, fileExists ? FileMode.Append : FileMode.Create, FileAccess.Write))
                using (var writer = new StreamWriter(stream, new UTF8Encoding(true)))
                {
                    if (!fileExists)
                    {
                        writer.WriteLine("\"APN\",\"X1,Y1\",\"X2,Y2\",\"X3,Y3\",\"X4,Y4\",\"DEFECT\",\"MAPPING\",\"LEVEL\",\"PATTERN\",\"EVENTTIME\""); // Viết tiêu đề CSV
                    }
                    writer.WriteLine(csvLine); // Viết dòng dữ liệu
                }

                lblDateTime.Text = "Đã lưu gần đây: " + currentTime;
                string statusText = $"Dữ liệu đã được ghi lại vào file.\nĐường dẫn: {filePath}";
                lblStatus.Text = statusText;
                int linkStart = statusText.IndexOf(filePath);
                int linkLength = filePath.Length;
                lblStatus.LinkArea = new LinkArea(linkStart, linkLength);
                lblStatus.ForeColor = Color.Green;
                lblStatus.Tag = folderPath;

                UpdateAPNCount();

                // Xóa các trường dữ liệu sau khi lưu
                txtAPN.Clear();
                foreach (var txt in txtCoordinatesX) txt.Clear();
                foreach (var txt in txtCoordinatesY) txt.Clear();
                txtSelectedError.Clear();
                txtSelectedPattern.Clear();
                txtSelectedLevel.Clear();
                txtSelectedPosition.Clear();
                txtAPN.Focus();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi khi lưu dữ liệu: " + ex.Message;
                lblStatus.LinkArea = new LinkArea(0, 0);
                lblStatus.ForeColor = Color.Red;
            }
        }

        // Xử lý sự kiện click vào nhãn trạng thái để mở thư mục chứa file CSV
        private void LblStatus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string folderPath = lblStatus.Tag as string;
                if (!string.IsNullOrEmpty(folderPath) && Directory.Exists(folderPath))
                {
                    Process.Start("explorer.exe", folderPath); // Mở thư mục
                }
                else
                {
                    MessageBox.Show("Thư mục không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở thư mục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý sự kiện xóa toàn bộ dữ liệu nhập
        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtAPN.Clear();
            foreach (var txt in txtCoordinatesX) txt.Clear();
            foreach (var txt in txtCoordinatesY) txt.Clear();
            txtSelectedError.Clear();
            txtSelectedPattern.Clear();
            txtSelectedLevel.Clear();
            txtSelectedPosition.Clear();
            lblStatus.Text = "Đã xóa tất cả dữ liệu";
            lblStatus.LinkArea = new LinkArea(0, 0);
            lblStatus.ForeColor = Color.Red;
            txtAPN.Focus();
        }

        // Xử lý sự kiện click vào TextBox để kích hoạt bàn phím số
        private void TextBox_Click(object sender, EventArgs e)
        {
            activeTextBox = (TextBox)sender;
        }

        // Xử lý sự kiện click vào nút chọn Tên lỗi
        private void BtnSelectError_Click(object sender, EventArgs e)
        {
            ShowSelectionForm("TÊN LỖI", txtSelectedError);
        }

        // Xử lý sự kiện click vào nút chọn Mức độ
        private void BtnSelectLevel_Click(object sender, EventArgs e)
        {
            ShowSelectionForm("MỨC ĐỘ", txtSelectedLevel);
        }

        // Xử lý sự kiện click vào nút chọn Pattern
        private void BtnSelectPattern_Click(object sender, EventArgs e)
        {
            ShowSelectionForm("PATTERN", txtSelectedPattern);
        }

        // Sự kiện click chung cho nút DK và BR
        private void BtnDK_BR_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            // Kiểm tra xem txtSelectedLevel đã có giá trị gì chưa
            if (string.IsNullOrEmpty(txtSelectedLevel.Text))
            {
                txtSelectedLevel.Text = clickedButton.Text;
            }
            else
            {
                // Nếu đã có, thêm vào sau dấu phẩy (hoặc thay thế nếu muốn)
                // Hiện tại, tôi sẽ nối thêm vào nếu chưa có, hoặc thay thế nếu đã có DK/BR
                if (txtSelectedLevel.Text.Contains("DK") || txtSelectedLevel.Text.Contains("BR"))
                {
                    // Nếu đã có DK hoặc BR, hãy thay thế chúng
                    string currentText = txtSelectedLevel.Text;
                    currentText = currentText.Replace("DK", "").Replace("BR", "").Trim();
                    if (!string.IsNullOrEmpty(currentText) && !currentText.EndsWith(","))
                    {
                        txtSelectedLevel.Text = currentText + " " + clickedButton.Text;
                    }
                    else if (!string.IsNullOrEmpty(currentText))
                    {
                        txtSelectedLevel.Text = currentText + clickedButton.Text;
                    }
                    else
                    {
                        txtSelectedLevel.Text = clickedButton.Text;
                    }
                }
                else
                {
                    txtSelectedLevel.Text += " " + clickedButton.Text;
                }
            }
        }

        // Kiểm tra đầu vào số cho các TextBox tọa độ
        private void ValidateNumericInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Vui lòng chỉ nhập số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
            {
                e.Handled = true; // Ngăn nhập nhiều dấu chấm
            }
        }
    }

    // Lớp tùy chỉnh để hiển thị nhãn theo chiều dọc
    public class VerticalLabel : Label
    {
        private string _authorText;

        public VerticalLabel(string authorText)
        {
            _authorText = authorText;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            SizeF textSize = pe.Graphics.MeasureString(_authorText, Font);
            pe.Graphics.TranslateTransform(Width / 2f, Height / 2f);
            pe.Graphics.RotateTransform(270); // Xoay văn bản 270 độ
            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                pe.Graphics.DrawString(
                    _authorText,
                    Font,
                    new SolidBrush(ForeColor),
                    new RectangleF(-textSize.Width / 2f, -textSize.Height / 2f, textSize.Width, textSize.Height),
                    format);
            }
            pe.Graphics.ResetTransform();
        }
    }
}