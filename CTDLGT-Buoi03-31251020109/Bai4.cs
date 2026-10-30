using System;
using System.Collections.Generic;
using System.Text;

namespace CTDLGT_Buoi03_31251020109
{
    internal class Bai4
    {
        public class TwoStackQueue<T>
        {
            // Ngăn chứa phần tử mới
            private readonly Stack<T> instack = new Stack<T>();
            // Ngăn chứa phần tử chuẩn bị Dequeue
            private readonly Stack<T> outstack = new Stack<T>();
            // Tổng số phần tử đã đổ từ instack sang outstack
            public long transfers { get; private set; } = 0;   
            public int Count => instack.Count + outstack.Count;
            public void Enqueue(T value)
            {
                instack.Push(value);
            }
            public T Dequeue()
            {
                if (Count == 0)
                {
                    throw new InvalidOperationException("Hang doi rong");
                }
                // Khi ngăn lấy ra rỗng, mới chuyển toàn bộ phần tử từ ngăn nhận sang
                if (outstack.Count == 0)
                {
                    while (instack.Count > 0)
                    {
                        outstack.Push(instack.Pop());
                        transfers++;
                    }
                }
                return outstack.Pop();
            }
        }
        public class NaiveTwoStack<T>
        {
            private readonly Stack<T> mainstack = new Stack<T>();
            private readonly Stack<T> tempstack = new Stack<T>();
            public int Count => mainstack.Count;
            public void Enqueue(T value)
            {
                while (mainstack.Count > 0)
                {
                    tempstack.Push(mainstack.Pop());
                }
                mainstack.Push(value);
                while (tempstack.Count > 0)
                {
                    mainstack.Push(tempstack.Pop());
                }
            }
            public T Dequeue()
            {
                if (mainstack.Count == 0)
                {
                    throw new InvalidOperationException("Hang doi rong");
                }
                return mainstack.Pop();
            }
        }
    }
}
