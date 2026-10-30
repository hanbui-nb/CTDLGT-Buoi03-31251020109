using System;
using System.Collections.Generic;
using System.Text;

namespace CTDLGT_Buoi03_31251020109
{
    internal class Bai3
    {
        // Lớp CircularQueue biểu diễn hàng đợi vòng sử dụng mảng với ba biến front, rear và count
        public class CircularQueue<T>
        {
            private T[] array;
            private int front;
            private int rear;
            private int count;
            // Khởi tạo hàng đợi vòng với dung lượng ban đầu là 4
            public CircularQueue()
            {
                array = new T[4];
                front = 0;
                rear = 0;
                count = 0;
            }
            // Trả về số phần tử hiện có trong hàng đợi
            public int Count
            {
                get { return count; }
            }
            // Trả về dung lượng mảng hiện tại
            public int Capacity
            {
                get { return array.Length; }
            }
            // Thêm một phần tử vào cuối hàng, tự động mở rộng gấp đôi nếu mảng đầy
            public void Enqueue(T value)
            {
                if (count == array.Length)
                {
                    Resize();
                }
                array[rear] = value;
                rear = (rear + 1) % array.Length;
                count++;
            }
            // Lấy và xóa phần tử ở đầu hàng; ném InvalidOperationException nếu hàng rỗng
            public T Dequeue()
            {
                if (count == 0)
                {
                    throw new InvalidOperationException("Hang doi rong");
                }

                T value = array[front];
                array[front] = default(T)!;
                front = (front + 1) % array.Length;
                count--;
                return value;
            }
            // Xem phần tử ở đầu hàng không xóa, ném InvalidOperationException nếu hàng rỗng
            public T Peek()
            {
                if (count == 0)
                {
                    throw new InvalidOperationException("Hang doi rong");
                }
                return array[front];
            }
            // Phương thức nội bộ mở rộng mảng gấp đôi và sao chép theo thứ tự logic bắt đầu từ front
            private void Resize()
            {
                int newcapacity = array.Length * 2;
                T[] newarray = new T[newcapacity];
                if (count > 0)
                {
                    if (front + count <= array.Length)
                    {
                        Array.Copy(array, front, newarray, 0, count);
                    }
                    else
                    {
                        int firstpart = array.Length - front;
                        Array.Copy(array, front, newarray, 0, firstpart);
                        Array.Copy(array, 0, newarray, firstpart, count - firstpart);
                    }
                }
                array = newarray;
                front = 0;
                rear = count;
            }
        }
        // Chương trình chính xử lý đầu vào động để chạy các ca kiểm thử tùy ý
        public static void Main(string[] args)
        {
            CircularQueue<string> queue = new CircularQueue<string>();
            string line;
            // Đọc các lệnh từ luồng nhập chuẩn cho đến khi kết thúc
            while ((line = Console.ReadLine()) != null)
            {
                line = line.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.Equals("EXIT", StringComparison.OrdinalIgnoreCase) || line.Equals("QUIT", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                string[] parts = line.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string cmd = parts[0].ToUpper();
                try
                {
                    switch (cmd)
                    {
                        case "ENQUEUE":
                            if (parts.Length > 1)
                            {
                                queue.Enqueue(parts[1]);
                                Console.WriteLine($"Enqueued: {parts[1]} | Count: {queue.Count} | Capacity: {queue.Capacity}");
                            }
                            else
                            {
                                Console.WriteLine("Error: Missing value");
                            }
                            break;

                        case "DEQUEUE":
                            string dequeued = queue.Dequeue();
                            Console.WriteLine($"Dequeued: {dequeued} | Count: {queue.Count} | Capacity: {queue.Capacity}");
                            break;

                        case "PEEK":
                            string peeked = queue.Peek();
                            Console.WriteLine($"Peeked: {peeked} | Count: {queue.Count} | Capacity: {queue.Capacity}");
                            break;

                        case "COUNT":
                            Console.WriteLine($"Count: {queue.Count}");
                            break;

                        case "CAPACITY":
                            Console.WriteLine($"Capacity: {queue.Capacity}");
                            break;

                        default:
                            Console.WriteLine($"Unknown command: {cmd}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.GetType().Name}: {ex.Message}");
                }
            }
        }
    }
}
