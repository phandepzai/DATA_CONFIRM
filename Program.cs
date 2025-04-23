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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        private static readonly string[] ErrorNames = new string[]
        {
            "Lỗi hàn thiếu", "Lỗi hàn thừa", "Lỗi hàn lệch", "Lỗi hàn nguội",
            "Lỗi hàn bị vỡ", "Lỗi hàn bong tróc", "Lỗi hàn không sáng", "Lỗi hàn bị oxy hóa",
            "Lỗi hàn bị chảy", "Lỗi hàn bị khô", "Lỗi hàn bị đen", "Lỗi hàn bị xỉ",
            "Lỗi hàn bị bẩn", "Lỗi hàn không đều", "Lỗi hàn bị rỗ", "Lỗi hàn bị bọt khí",
            "Lỗi linh kiện sai", "Lỗi linh kiện thiếu", "Lỗi linh kiện lệch", "Lỗi linh kiện ngược",
            "Lỗi linh kiện giả", "Lỗi linh kiện kém chất lượng", "Lỗi linh kiện bị vỡ", "Lỗi linh kiện bị xước",
            "Lỗi linh kiện bị oxy hóa", "Lỗi linh kiện bị cong", "Lỗi linh kiện bị gãy", "Lỗi linh kiện bị rơ",
            "Lỗi PCB bị nứt", "Lỗi PCB bị cong", "Lỗi PCB bị xước", "Lỗi PCB bị vỡ",
            "Lỗi PCB bị ố vàng", "Lỗi PCB bị ẩm", "Lỗi PCB bị bong", "Lỗi PCB bị chảy",
            "Lỗi PCB bị biến dạng", "Lỗi PCB sai kích thước", "Lỗi PCB sai layout", "Lỗi PCB thiếu lỗ",
            "Lỗi đứt mạch", "Lỗi chập mạch", "Lỗi thông mạch", "Lỗi ngắn mạch",
            "Lỗi mạch hở", "Lỗi mạch bị đứt", "Lỗi mạch bị lỗi", "Lỗi mạch không thông",
            "Lỗi mạch bị nhiễu", "Lỗi mạch bị sai", "Lỗi mạch bị yếu", "Lỗi mạch không ổn định",
            "Lỗi bẩn", "Lỗi xước", "Lỗi biến dạng", "Lỗi kích thước",
            "Lỗi màu sắc", "Lỗi bề mặt", "Lỗi độ dày", "Lỗi độ phẳng",
            "Lỗi vết ố", "Lỗi vết bẩn", "Lỗi vết xước", "Lỗi vết lõm"
        };

        private static readonly string[] PatternNames = new string[]
        {
            "Pattern 1", "Pattern 2", "Pattern 3", "Pattern 4", "Pattern 5",
            "Pattern 6", "Pattern 7", "Pattern 8", "Pattern 9", "Pattern 10",
            "Pattern 11", "Pattern 12", "Pattern 13", "Pattern 14", "Pattern 15",
            "Pattern 16", "Pattern 17", "Pattern 18", "Pattern 19", "Pattern 20",
            "Pattern 21", "Pattern 22", "Pattern 23", "Pattern 24", "Pattern 25",
            "Pattern 26", "Pattern 27", "Pattern 28", "Pattern 29", "Pattern 30"
        };

        private TextBox txtAPN;
        private TextBox[] txtCoordinatesX;
        private TextBox[] txtCoordinatesY;
        private TextBox txtSelectedError;
        private TextBox txtSelectedPattern;
        private TextBox txtSelectedLevel;
        private Button btnSelectError;
        private Button btnSelectPattern;
        private Button btnSelectLevel;
        private Button btnSave;
        private Button btnReset;
        private Panel numericKeyboard;
        private LinkLabel lblStatus;
        private Label lblDateTime;
        private Label lblAPNCount;

        public MainForm()
        {
            InitializeComponent();
            InitializeNumericKeyboard();
            UpdateAPNCount();
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FORM_NHAP_DATA.icon.ico"))
            {
                if (stream != null)
                {
                    this.Icon = new Icon(stream);
                }
            }
            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FORM_NHAP_DATA.icon.ico"))
                {
                    if (stream != null)
                    {
                        this.Icon = new Icon(stream);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài nguyên icon.ico");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải biểu tượng: " + ex.Message);
            }
        }

        private void InitializeComponent()
        {
            this.Text = "DATA CONFIRM";
            this.Size = new Size(450, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Ngăn kéo dãn cửa sổ 
            this.MaximizeBox = false; // Vô hiệu hóa nút phóng to

            Label lblAPN = new Label();
            lblAPN.Text = "APN:";
            lblAPN.Location = new Point(20, 18);
            lblAPN.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblAPN.AutoSize = true;
            this.Controls.Add(lblAPN);

            txtAPN = new TextBox();
            txtAPN.Location = new Point(60, 10);
            txtAPN.Size = new Size(340, 40);
            txtAPN.Font = new Font("Arial", 16F);
            txtAPN.MaxLength = 300;
            txtAPN.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtCoordinatesX[0].Focus();
                    e.SuppressKeyPress = true;
                }
            };
            this.Controls.Add(txtAPN);

            txtCoordinatesX = new TextBox[5];
            txtCoordinatesY = new TextBox[5];

            for (int i = 0; i < 5; i++)
            {
                Label lblX = new Label();
                lblX.Text = $"X{i + 1}:";
                lblX.Location = new Point(25, 67 + i * 40);
                lblX.AutoSize = true;
                lblX.Font = new Font("Arial", 9F, FontStyle.Bold);
                this.Controls.Add(lblX);

                txtCoordinatesX[i] = new TextBox();
                txtCoordinatesX[i].Location = new Point(50, 60 + i * 40);
                txtCoordinatesX[i].Size = new Size(60, 40);
                txtCoordinatesX[i].Font = new Font("Arial", 12F, FontStyle.Bold);
                txtCoordinatesX[i].TextAlign = HorizontalAlignment.Center;
                txtCoordinatesX[i].Click += TextBox_Click;
                txtCoordinatesX[i].KeyPress += ValidateNumericInput;
                this.Controls.Add(txtCoordinatesX[i]);

                Label lblY = new Label();
                lblY.Text = $"Y{i + 1}:";
                lblY.Location = new Point(125, 67 + i * 40);
                lblY.AutoSize = true;
                lblY.Font = new Font("Arial", 9F, FontStyle.Bold);
                this.Controls.Add(lblY);

                txtCoordinatesY[i] = new TextBox();
                txtCoordinatesY[i].Location = new Point(150, 60 + i * 40);
                txtCoordinatesY[i].Size = new Size(60, 40);
                txtCoordinatesY[i].Font = new Font("Arial", 12F, FontStyle.Bold);
                txtCoordinatesY[i].TextAlign = HorizontalAlignment.Center;
                txtCoordinatesY[i].Click += TextBox_Click;
                txtCoordinatesY[i].KeyPress += ValidateNumericInput;
                this.Controls.Add(txtCoordinatesY[i]);
            }

            CreateSelectionControls("TÊN LỖI", 260, ref txtSelectedError, ref btnSelectError);
            CreateSelectionControls("LEVEL", 300, ref txtSelectedLevel, ref btnSelectLevel);
            CreateSelectionControls("PATTERN", 340, ref txtSelectedPattern, ref btnSelectPattern);

            btnSave = new Button();
            btnSave.Text = "XÁC NHẬN";
            btnSave.Location = new Point(100, 400);
            btnSave.Size = new Size(100, 40);
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnReset = new Button();
            btnReset.Text = "RESET";
            btnReset.Location = new Point(240, 400);
            btnReset.Size = new Size(100, 40);
            btnReset.Click += BtnReset_Click;
            this.Controls.Add(btnReset);

            lblDateTime = new Label();
            lblDateTime.Location = new Point(20, 480);
            lblDateTime.Size = new Size(320, 20);
            lblDateTime.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(lblDateTime);

            lblStatus = new LinkLabel();
            lblStatus.Location = new Point(20, 500);
            lblStatus.Size = new Size(320, 40);
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.AutoSize = false;
            lblStatus.LinkColor = Color.Blue;
            lblStatus.ActiveLinkColor = Color.Red;
            lblStatus.LinkBehavior = LinkBehavior.HoverUnderline;
            lblStatus.LinkClicked += LblStatus_LinkClicked;
            this.Controls.Add(lblStatus);

            lblAPNCount = new Label();
            lblAPNCount.Location = new Point(50, 450);
            lblAPNCount.Size = new Size(320, 20);
            lblAPNCount.TextAlign = ContentAlignment.MiddleCenter;
            lblAPNCount.Text = "Số cell đã lưu: 0";
            this.Controls.Add(lblAPNCount);

            btnSelectError.Click += BtnSelectError_Click;
            btnSelectLevel.Click += BtnSelectLevel_Click;
            btnSelectPattern.Click += BtnSelectPattern_Click;

            // Thêm Label cho tên tác giả
            VerticalLabel lblAuthor = new VerticalLabel("Tác giả: Nông Văn Phấn");
            lblAuthor.Font = new Font("Tahoma", 7F, FontStyle.Regular);
            lblAuthor.ForeColor = Color.Gray;
            lblAuthor.BackColor = Color.Transparent;
            lblAuthor.AutoSize = false;
            lblAuthor.Size = new Size(20, 150);
            lblAuthor.Location = new Point(this.ClientSize.Width - 20, this.ClientSize.Height - 130);
            this.Controls.Add(lblAuthor);
            lblAuthor.BringToFront();
            this.Resize += (s, e) =>
            {
                lblAuthor.Location = new Point(this.ClientSize.Width - 15, this.ClientSize.Height - 140);
            };
        }

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
            textBox.ReadOnly = true;
            this.Controls.Add(textBox);
        }

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
                btn.Click += NumericButton_Click;
                numericKeyboard.Controls.Add(btn);
            }
        }

        private void NumericButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "Xóa")
            {
                if (activeTextBox != null && !string.IsNullOrEmpty(activeTextBox.Text))
                {
                    activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
                }
            }
            else
            {
                if (activeTextBox != null)
                {
                    activeTextBox.Text += btn.Text;
                }
            }
        }

        private TextBox activeTextBox = null;
        private void ShowSelectionForm(string type, TextBox targetTextBox)
        {
            using (Form selectionForm = new Form())
            {
                selectionForm.Text = "CHỌN " + type;
                selectionForm.StartPosition = FormStartPosition.CenterParent;
                selectionForm.ShowInTaskbar = false;
                selectionForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                selectionForm.MaximizeBox = false;
                selectionForm.MinimizeBox = false;
                selectionForm.TopMost = true;

                if (type == "MỨC ĐỘ") // Thêm 'if' vào đây
                {
                    TableLayoutPanel tablePanel = new TableLayoutPanel();
                    tablePanel.Dock = DockStyle.Fill;
                    tablePanel.ColumnCount = 5;
                    tablePanel.RowCount = 11; // Tăng số hàng lên để thêm các nút mới
                    tablePanel.Padding = new Padding(5);

                    for (int i = 0; i < 5; i++)
                    {
                        tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                    }
                    for (int i = 0; i < 11; i++) // Tăng số hàng để chứa các nút mới
                    {
                        tablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
                    }

                    selectionForm.Controls.Add(tablePanel);
                    selectionForm.Size = new Size(500, 640); // Tăng chiều cao để phù hợp với nội dung mới

                    int buttonIndex = 0;

                    // Thêm các nút "Rất yếu", "Yếu", "Vừa", "Mạnh", "Rất Mạnh"
                    string[] additionalLevels = { "Rất yếu", "Yếu", "Vừa", "Mạnh", "Rất Mạnh" };
                    foreach (string level in additionalLevels)
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
                            targetTextBox.Text = levelButton.Text;
                            selectionForm.Close();
                        };

                        int row = buttonIndex / 5;
                        int col = buttonIndex % 5;
                        tablePanel.Controls.Add(levelButton, col, row);
                        buttonIndex++;
                    }

                    // Thêm các nút mức độ từ Lv0.1 đến Lv5.0
                    for (double i = 0.1; i <= 5.0; i += 0.1)
                    {
                        Button levelButton = new Button();
                        levelButton.Text = $"Lv{i.ToString("F1", CultureInfo.InvariantCulture)}";
                        levelButton.Dock = DockStyle.Fill;
                        levelButton.Margin = new Padding(2);
                        levelButton.Font = new Font("Arial", 11F);
                        levelButton.BackColor = Color.White;
                        levelButton.FlatStyle = FlatStyle.Flat;
                        levelButton.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
                        levelButton.Click += (s, e) =>
                        {
                            targetTextBox.Text = levelButton.Text;
                            selectionForm.Close();
                        };

                        int row = buttonIndex / 5;
                        int col = buttonIndex % 5;
                        tablePanel.Controls.Add(levelButton, col, row);
                        buttonIndex++;
                    }
                }
            
        
                else if (type == "PATTERN")
                {
                    int columns = 5;
                    int rows = 6;

                    TableLayoutPanel tablePanel = new TableLayoutPanel();
                    tablePanel.Dock = DockStyle.Top;
                    tablePanel.Height = 310;
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

                    selectionForm.Size = new Size(500, 400);

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
                                selectedPatterns.Remove(clickedButton.Text);
                                clickedButton.BackColor = Color.White;
                            }
                            else
                            {
                                selectedPatterns.Add(clickedButton.Text);
                                clickedButton.BackColor = Color.LightBlue;
                            }
                        };

                        int row = i / columns;
                        int col = i % columns;
                        tablePanel.Controls.Add(patternButton, col, row);
                    }

                    Button btnConfirm = new Button();
                    btnConfirm.Text = "Xác nhận";
                    btnConfirm.Size = new Size(100, 40);
                    btnConfirm.Location = new Point(130, 315);
                    btnConfirm.BackColor = Color.White;
                    btnConfirm.Font = new Font("Arial", 12F);
                    btnConfirm.FlatStyle = FlatStyle.Flat;
                    btnConfirm.FlatAppearance.BorderSize = 1;
                    btnConfirm.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 204);
                    selectionForm.Controls.Add(btnConfirm);

                    Button btnCancel = new Button();
                    btnCancel.Text = "Hủy bỏ";
                    btnCancel.Size = new Size(100, 40);
                    btnCancel.Location = new Point(250, 315);
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
                            targetTextBox.Text = string.Join(", ", selectedPatterns.OrderBy(p => p));
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

                    int formWidth = 760;
                    int formHeight = (rows * 65) + 50;
                    selectionForm.Size = new Size(formWidth, formHeight);

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
                            targetTextBox.Text = errorButton.Text;
                            selectionForm.Close();
                        };

                        int row = i / columns;
                        int col = i % columns;
                        tablePanel.Controls.Add(errorButton, col, row);
                    }
                }

                selectionForm.ShowDialog(this);
            }
        }

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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool hasData = !string.IsNullOrEmpty(txtAPN.Text) ||
                               txtCoordinatesX.Any(txt => !string.IsNullOrEmpty(txt.Text)) ||
                               txtCoordinatesY.Any(txt => !string.IsNullOrEmpty(txt.Text)) ||
                               !string.IsNullOrEmpty(txtSelectedError.Text) ||
                               !string.IsNullOrEmpty(txtSelectedLevel.Text) ||
                               !string.IsNullOrEmpty(txtSelectedPattern.Text);

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
                    Directory.CreateDirectory(folderPath);
                }

                List<string> csvColumns = new List<string>
                {
                    $"\"{txtAPN.Text.Replace("\"", "\"\"")}\""
                };

                for (int i = 0; i < 5; i++)
                {
                    string xyPair = string.IsNullOrEmpty(txtCoordinatesX[i].Text) && string.IsNullOrEmpty(txtCoordinatesY[i].Text)
                        ? ""
                        : $"{txtCoordinatesX[i].Text},{txtCoordinatesY[i].Text}";
                    csvColumns.Add($"\"{xyPair}\"");
                }

                csvColumns.Add($"\"{txtSelectedError.Text.Replace("\"", "\"\"")}\"");
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
                        writer.WriteLine("\"APN\",\"X1,Y1\",\"X2,Y2\",\"X3,Y3\",\"X4,Y4\",\"X5,Y5\",\"DEFECT\",\"LEVEL\",\"PATTERN\",\"EVENTTIME\"");
                    }
                    writer.WriteLine(csvLine);
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

                txtAPN.Clear();
                foreach (var txt in txtCoordinatesX) txt.Clear();
                foreach (var txt in txtCoordinatesY) txt.Clear();
                txtSelectedError.Clear();
                txtSelectedPattern.Clear();
                txtSelectedLevel.Clear();

                txtAPN.Focus();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi khi lưu dữ liệu: " + ex.Message;
                lblStatus.LinkArea = new LinkArea(0, 0);
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void LblStatus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string folderPath = lblStatus.Tag as string;
                if (!string.IsNullOrEmpty(folderPath) && Directory.Exists(folderPath))
                {
                    Process.Start("explorer.exe", folderPath);
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

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtAPN.Clear();
            foreach (var txt in txtCoordinatesX) txt.Clear();
            foreach (var txt in txtCoordinatesY) txt.Clear();
            txtSelectedError.Clear();
            txtSelectedPattern.Clear();
            txtSelectedLevel.Clear();
            lblStatus.Text = "Đã xóa tất cả dữ liệu";
            lblStatus.LinkArea = new LinkArea(0, 0);
            lblStatus.ForeColor = Color.Red;
        }

        private void TextBox_Click(object sender, EventArgs e)
        {
            activeTextBox = (TextBox)sender;
        }

        private void BtnSelectError_Click(object sender, EventArgs e)
        {
            ShowSelectionForm("TÊN LỖI", txtSelectedError);
        }

        private void BtnSelectLevel_Click(object sender, EventArgs e)
        {
            ShowSelectionForm("MỨC ĐỘ", txtSelectedLevel);
        }

        private void BtnSelectPattern_Click(object sender, EventArgs e)
        {
            ShowSelectionForm("PATTERN", txtSelectedPattern);
        }

        private void ValidateNumericInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Vui lòng chỉ nhập số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
    }

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
            pe.Graphics.RotateTransform(270); // Đổi từ 90 thành 270 để xoay từ dưới lên
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