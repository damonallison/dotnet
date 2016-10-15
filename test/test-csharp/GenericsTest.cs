using System;
using System.Collections.Generic;

namespace DamonAllison.CSharpTests
{

    // Write a printer class 
    internal class Stack<T> {
        private List<T> internalStack = new List<T>();
        public Stack() {}

        public void Push(T obj)
        {
            internalStack.Add(obj);
        }
        public T Pop() 
        {
            if (internalStack.Count == 0) 
            {
                throw new InvalidOperationException();
            }
            T temp = internalStack[internalStack.Count - 1];
            internalStack.RemoveAt(internalStack.Count - 1);
            return temp;
        }
    }

    }
    public class GenericsTests
    {

    }
}