using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CTDLGT_Buoi03_31251020109
{
    internal class Bai2
    {
        // Cấu trúc dữ liệu lưu trữ thông tin khách hàng
        record Customer(int id, long thoidiem, long phucvu);
        public static void Main2(string[] args)
        {
            // Thiết lập bộ đọc và xuất dữ liệu tối ưu hiệu năng
            using (StreamReader reader = new StreamReader(Console.OpenStandardInput()))
            {
                StringBuilder output = new StringBuilder();
                // Đọc dòng đầu tiên chứa n (số khách) và k (số quầy)
                string firstline = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(firstline)) return;
                string[] value = firstline.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                int n = int.Parse(value[0]);
                int k = int.Parse(value[1]);
                // Dùng Queue<Customer> để quản lý hàng chờ theo đúng thứ tự đến (FIFO)
                Queue<Customer> queue = new Queue<Customer>(n);
                for (int i = 1; i <= n; i++)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] tt = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    // Thời điểm đến
                    long a = long.Parse(tt[0]);
                    // Thời gian phục vụ
                    long s = long.Parse(tt[1]); 
                    queue.Enqueue(new Customer(i, a, s));
                }
                // Mảng lưu thời điểm rảnh của từng quầy (được đánh số từ 0 đến k-1)
                // Tương ứng với quầy 1 đến quầy k trong thực tế
                long[] tgianranh = new long[k];
                // Dùng kiểu long để tránh tràn số khi n lớn
                long tongcho = 0; 
                long maxcho = 0;
                long finish = 0;
                // Xử lý từng khách hàng trong Queue
                while (queue.Count > 0)
                {
                    Customer customer = queue.Dequeue();
                    // Tìm quầy rảnh sớm nhất
                    // Nếu nhiều quầy có cùng thời điểm rảnh, chọn quầy có số thứ tự nhỏ nhất
                    int quay = 0;
                    long ranh = tgianranh[0];
                    for (int q = 1; q < k; q++)
                    {
                        if (tgianranh[q] < ranh)
                        {
                            ranh = tgianranh[q];
                            quay = q;
                        }
                    }
                    // Thời điểm bắt đầu phục vụ = MAX(Thời điểm khách đến, Thời điểm quầy chọn rảnh)
                    long start = Math.Max(customer.thoidiem, ranh);
                    long wait = start - customer.thoidiem;
                    long end = start + customer.phucvu;
                    // Cập nhật thời điểm quầy vừa chọn trở nên rảnh tiếp theo
                    tgianranh[quay] = end;
                    // Cập nhật các chỉ số thống kê tổng quan
                    tongcho += wait;
                    if (wait > maxcho)
                    {
                        maxcho = wait;
                    }
                    if (end > finish)
                    {
                        finish = end;
                    }
                    // Xuất dòng thông tin chi tiết của khách hàng i
                    // Định dạng: KH i: den a, quay q, bat dau b, cho w, xong e
                    output.AppendLine($"KH {customer.id}: den {customer.thoidiem}, quay {quay + 1}, bat dau {start}, cho {wait}, xong {end}");
                }
                // Tính thời gian chờ trung bình
                double avg = n > 0 ? (double)tongcho / n : 0.0;
                // In ra 3 dòng tổng kết cuối cùng với định dạng số thực 2 chữ số thập phân
                output.AppendLine(string.Format(CultureInfo.InvariantCulture, "Cho trung binh: {0:F2}", avg));
                output.AppendLine($"Cho lau nhat: {maxcho}");
                output.AppendLine($"Hoan thanh luc: {finish}");
                // Đưa toàn bộ kết quả ra Console
                Console.Write(output.ToString());
            }
        }
    }
}
