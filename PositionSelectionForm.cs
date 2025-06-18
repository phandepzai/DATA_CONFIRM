// PositionSelectionForm.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq; // Thêm namespace để sử dụng phương thức .Any() và .Join()
using System.Reflection;
using System.Windows.Forms;

namespace DATA_CONFIRM
{
    public class PositionSelectionForm : Form
    {
        private Panel productPanel; // Bảng điều khiển để hiển thị lưới 4x4
        public List<string> SelectedPositions { get; private set; } // Danh sách các vị trí được chọn trên lưới

        // Mảng 4x4 chứa tên các ô (A1, A2, ..., D4) để xác định vị trí trên lưới
        private static readonly string[,] CellNames = new string[4, 4]
        {
            { "A1", "A2", "B1", "B2" },
            { "A3", "A4", "B3", "B4" },
            { "C1", "C2", "D1", "D2" },
            { "C3", "C4", "D3", "D4" }
        };

        public PositionSelectionForm()
        {
            InitializeComponent(); // Khởi tạo các thành phần giao diện
            this.Text = "CHỌN VỊ TRÍ LỖI"; // Tiêu đề của form
            this.StartPosition = FormStartPosition.CenterParent; // Căn giữa form so với form cha
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Không cho phép thay đổi kích thước form
            this.MaximizeBox = false; // Ẩn nút phóng to
            this.MinimizeBox = false; // Ẩn nút thu nhỏ
            this.Size = new Size(340, 420); // Đặt kích thước form

            SelectedPositions = new List<string>(); // Khởi tạo danh sách lưu các vị trí được chọn

            // Tạo panel để vẽ lưới 4x4 và cho phép người dùng chọn vị trí
            productPanel = new Panel();
            productPanel.Size = new Size(300, 300); // Kích thước của panel
            productPanel.Location = new Point((this.ClientSize.Width - productPanel.Width) / 2, 10); // Căn giữa panel theo chiều ngang
            productPanel.BorderStyle = BorderStyle.FixedSingle; // Đặt viền cho panel
            productPanel.Click += ProductPanel_Click; // Gán sự kiện click để xử lý chọn ô
            productPanel.Paint += ProductPanel_Paint; // Gán sự kiện vẽ để hiển thị lưới và đánh dấu ô
            this.Controls.Add(productPanel); // Thêm panel vào form

            // Tạo nút "Xác nhận" để xác nhận các vị trí đã chọn
            Button btnConfirm = new Button();
            btnConfirm.Text = "Xác nhận";
            btnConfirm.Size = new Size(80, 30);
            btnConfirm.Location = new Point(productPanel.Location.X + 45, productPanel.Bottom + 20); // Đặt vị trí nút dưới panel
            btnConfirm.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.OK; // Đặt kết quả trả về là OK
                this.Close(); // Đóng form
            };
            this.Controls.Add(btnConfirm); // Thêm nút vào form

            // Tạo nút "Hủy bỏ" để hủy thao tác chọn vị trí
            Button btnCancel = new Button();
            btnCancel.Text = "Hủy bỏ";
            btnCancel.Size = new Size(80, 30);
            btnCancel.Location = new Point(btnConfirm.Right + 10, btnConfirm.Location.Y); // Đặt cạnh nút Xác nhận
            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel; // Đặt kết quả trả về là Cancel
                this.Close(); // Đóng form
            };
            this.Controls.Add(btnCancel); // Thêm nút vào form

            // Cố gắng tải biểu tượng (icon) cho form từ tài nguyên
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

        private void InitializeComponent()
        {
            // Phương thức này hiện không được sử dụng vì các thành phần được khởi tạo thủ công
        }

        // Xử lý sự kiện khi người dùng click vào panel để chọn/xóa vị trí
        private void ProductPanel_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e; // Lấy thông tin sự kiện chuột
            Point coordinates = me.Location; // Lấy tọa độ click

            int panelWidth = productPanel.Width; // Chiều rộng panel
            int panelHeight = productPanel.Height; // Chiều cao panel
            int cellWidth = panelWidth / 4; // Chiều rộng mỗi ô trong lưới 4x4
            int cellHeight = panelHeight / 4; // Chiều cao mỗi ô trong lưới 4x4

            int col = coordinates.X / cellWidth; // Tính cột dựa trên tọa độ X
            int row = coordinates.Y / cellHeight; // Tính hàng dựa trên tọa độ Y

            // Kiểm tra xem tọa độ có nằm trong lưới 4x4 không
            if (row >= 0 && row < 4 && col >= 0 && col < 4)
            {
                string clickedPosition = CellNames[row, col]; // Lấy tên ô được click (A1, A2, ...)

                // Nếu ô đã được chọn thì xóa khỏi danh sách, nếu chưa thì thêm vào
                if (SelectedPositions.Contains(clickedPosition))
                {
                    SelectedPositions.Remove(clickedPosition); // Xóa vị trí khỏi danh sách
                }
                else
                {
                    SelectedPositions.Add(clickedPosition); // Thêm vị trí vào danh sách
                }
                productPanel.Invalidate(); // Yêu cầu vẽ lại panel để cập nhật giao diện
            }
        }

        // Xử lý sự kiện vẽ panel để hiển thị lưới và các ô đã chọn
        private void ProductPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; // Đối tượng đồ họa để vẽ
            int panelWidth = productPanel.Width; // Chiều rộng panel
            int panelHeight = productPanel.Height; // Chiều cao panel
            int cellWidth = panelWidth / 4; // Chiều rộng mỗi ô
            int cellHeight = panelHeight / 4; // Chiều cao mỗi ô

            // Vẽ lưới 4x4
            using (Pen gridPen = new Pen(Color.LightGray, 1)) // Bút vẽ màu xám nhạt
            {
                for (int i = 0; i <= 4; i++)
                {
                    // Vẽ các đường ngang
                    g.DrawLine(gridPen, 0, i * cellHeight, panelWidth, i * cellHeight);
                    // Vẽ các đường dọc
                    g.DrawLine(gridPen, i * cellWidth, 0, i * cellWidth, panelHeight);
                }
            }

            // Vẽ tên ô (A1, A2, ...) vào giữa mỗi ô
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center; // Căn giữa ngang
                sf.LineAlignment = StringAlignment.Center; // Căn giữa dọc
                using (Font cellFont = new Font("Arial", 12, FontStyle.Bold)) // Font chữ cho tên ô
                using (Brush textBrush = new SolidBrush(Color.DarkBlue)) // Màu chữ xanh đậm
                {
                    for (int r = 0; r < 4; r++)
                    {
                        for (int c = 0; c < 4; c++)
                        {
                            Rectangle cellRect = new Rectangle(c * cellWidth, r * cellHeight, cellWidth, cellHeight); // Hình chữ nhật của ô
                            g.DrawString(CellNames[r, c], cellFont, textBrush, cellRect, sf); // Vẽ tên ô
                        }
                    }
                }
            }

            // Vẽ khung đỏ xung quanh các ô đã được chọn
            if (SelectedPositions.Any()) // Kiểm tra xem có vị trí nào được chọn không
            {
                foreach (string position in SelectedPositions)
                {
                    int selectedRow = -1;
                    int selectedCol = -1;

                    // Tìm hàng và cột của ô đã chọn trong mảng CellNames
                    for (int r = 0; r < 4; r++)
                    {
                        for (int c = 0; c < 4; c++)
                        {
                            if (CellNames[r, c] == position)
                            {
                                selectedRow = r;
                                selectedCol = c;
                                break;
                            }
                        }
                        if (selectedRow != -1) break; // Thoát vòng lặp nếu tìm thấy
                    }

                    if (selectedRow != -1 && selectedCol != -1)
                    {
                        Rectangle selectedRect = new Rectangle(
                            selectedCol * cellWidth,
                            selectedRow * cellHeight,
                            cellWidth,
                            cellHeight); // Hình chữ nhật của ô đã chọn

                        // Vẽ khung đỏ quanh ô
                        using (Pen highlightPen = new Pen(Color.Red, 2)) // Bút vẽ màu đỏ, độ dày 2px
                        {
                            g.DrawRectangle(highlightPen, selectedRect); // Vẽ khung
                        }
                    }
                }
            }
        }
    }
}