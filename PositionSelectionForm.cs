// PositionSelectionForm.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
            // Đặt ClientSize theo kích thước 300x500 (sẽ được điều chỉnh lại sau)
            this.ClientSize = new Size(300, 500);

            SelectedPositions = new List<string>(); // Khởi tạo danh sách lưu các vị trí được chọn

            // Tạo panel để vẽ lưới 4x4 với các ô có tỷ lệ rộng 4, cao 6
            productPanel = new Panel();
            int panelWidth = this.ClientSize.Width - 20; // Chiều rộng panel dựa trên form
            // Tính toán panelHeight để các ô có tỷ lệ 4x6 (panelHeight = panelWidth * (chiều cao ô / chiều rộng ô))
            // Tỷ lệ (4x6) cho từng ô trong lưới 4x4 tổng thể sẽ làm cho panel có tỷ lệ chiều cao lớn hơn chiều rộng.
            // (4 hàng * 6 đơn vị chiều cao) / (4 cột * 4 đơn vị chiều rộng) = 24 / 16 = 1.5
            int panelHeight = (int)(panelWidth * (6.0 / 4.0));
            productPanel.Size = new Size(panelWidth, panelHeight);
            // Căn giữa panel ở phía trên
            productPanel.Location = new Point((this.ClientSize.Width - productPanel.Width) / 2, 20);
            productPanel.BorderStyle = BorderStyle.FixedSingle; // Đặt viền cho panel
            productPanel.Click += ProductPanel_Click; // Gán sự kiện click để xử lý chọn ô
            productPanel.Paint += ProductPanel_Paint; // Gán sự kiện vẽ để hiển thị lưới và đánh dấu ô
            this.Controls.Add(productPanel); // Thêm panel vào form

            // --- Giữ nguyên bố cục các nút bấm như ban đầu ---

            // Tạo nút "Xác nhận"
            Button btnConfirm = new Button();
            btnConfirm.Text = "Xác nhận";
            btnConfirm.Size = new Size(80, 30);

            // Tạo nút "Hủy bỏ"
            Button btnCancel = new Button();
            btnCancel.Text = "Hủy bỏ";
            btnCancel.Size = new Size(80, 30);

            // Vị trí Y được đặt bên dưới panel
            int buttonY = productPanel.Bottom + 20;

            // Tính toán vị trí X để căn giữa cả hai nút nằm ngang
            int totalButtonWidth = btnConfirm.Width + btnCancel.Width + 10; // 10 là khoảng cách
            int buttonStartX = (this.ClientSize.Width - totalButtonWidth) / 2;

            btnConfirm.Location = new Point(buttonStartX, buttonY);
            btnCancel.Location = new Point(btnConfirm.Right + 10, buttonY);

            // Gán sự kiện và thêm các nút vào form
            btnConfirm.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            this.Controls.Add(btnConfirm);

            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            this.Controls.Add(btnCancel);

            // Điều chỉnh lại chiều cao form cho vừa vặn với panel và các nút
            this.ClientSize = new Size(this.ClientSize.Width, btnCancel.Bottom + 20);

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
            using (Pen gridPen = new Pen(Color.FromArgb(181,181,181), 2)) // Bút vẽ màu xám nhạt
            {
                for (int i = 0; i <= 4; i++) // 4 đường dọc cho 4 cột
                {
                    g.DrawLine(gridPen, i * cellWidth, 0, i * cellWidth, panelHeight);
                }
                for (int i = 0; i <= 4; i++) // 4 đường ngang cho 4 hàng
                {
                    g.DrawLine(gridPen, 0, i * cellHeight, panelWidth, i * cellHeight);
                }
            }

            // Vẽ tên ô (A1, A2, ...) vào giữa mỗi ô
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center; // Căn giữa ngang
                sf.LineAlignment = StringAlignment.Center; // Căn giữa dọc
                // Tự động điều chỉnh kích thước font chữ cho vừa với ô
                float fontSize = Math.Max(12, cellWidth / 5);
                using (Font cellFont = new Font("Arial", fontSize, FontStyle.Bold))
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
                            // Điều chỉnh hình chữ nhật để vẽ đường viền nhỏ hơn một chút
                            // và căn giữa trong ô để tránh bị cắt.
                            // Độ dày của bút là 2, vì vậy chúng ta điều chỉnh 1 pixel mỗi bên.
                            g.DrawRectangle(highlightPen,
                                selectedRect.X + highlightPen.Width / 2,
                                selectedRect.Y + highlightPen.Width / 2,
                                selectedRect.Width - highlightPen.Width,
                                selectedRect.Height - highlightPen.Width);
                        }
                    }
                }
            }
        }
    }
}