using System;
using System.Collections.Generic;
using System.Text;

namespace CTDLGT_Buoi03_31251020109
{
    internal class Bai1
    {
        static string check(string input)
        {
            // Xử lí trường hợp chuỗi rỗng hoặc null
            if (string.IsNullOrEmpty(input))
            {
                return "HOP LE";
            }
            // Sử dụng Stack chứa tuple gồm ký tự ngoặc và index gốc của nó
            var st = new Stack<(char ch, int pos)>();
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                // Nếu là ngoặc mở, đẩy vào Stack kèm vị trí hiện tại
                if (c == '(' || c == '[' || c == '{')
                {
                    st.Push((c, i));
                }
                // Nếu là ngoặc đóng
                else if (c == ')' || c == ']' || c == '}')
                {
                    // Trường hợp đóng thừa 
                    if (st.Count == 0)
                    {
                        return $"SAI tai vi tri {i}";
                    }
                    var top = st.Pop();
                    // Kiểm tra xem ngoặc đóng có khớp loại với ngoặc mở trên đỉnh Stack hay không
                    if ((c == ')' && top.ch != '(') || (c == ']' && top.ch != '[') || (c == '}' && top.ch != '{'))
                    {
                        return $"SAI tai vi tri {i}";
                    }
                }
                // Các ký tự khác ngoài ngoặc sẽ tự động bị bỏ qua
            }
            // Sau khi duyệt xong, nếu Stack còn phần tử thì nghĩa là có ngoặc mở chưa được đóng
            if (st.Count > 0)
            {
                int minpos = int.MaxValue;
                foreach (var item in st)
                {
                    if (item.pos < minpos)
                    {
                        minpos = item.pos;
                    }
                }
                return $"SAI tai vi tri {minpos}";
            }
            return "HOP LE";
        }
        public static void Main1(string[] args)
        {
            StringBuilder output = new StringBuilder();

            using (StreamReader reader = new StreamReader(Console.OpenStandardInput()))
            {
                string input = reader.ReadLine();
                // Xử lý chuỗi đầu vào
                string result = check(input);
                // Ghi kết quả vào StringBuilder
                output.AppendLine(result);
            }
            // In toàn bộ kết quả ra màn hình
            Console.Write(output.ToString());
        }
    }
}
