using System.Collections;
using System.Collections.Generic;

namespace Recommender.Controls
{
    public class CircularList<T> : List<T>, IEnumerable<T>
    {
        public new IEnumerator<T> GetEnumerator()
        {
            return new CircularEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CircularEnumerator<T>(this);
        }
    }

    internal class CircularEnumerator<T> : IEnumerator<T>
    {
        private readonly List<T> list;
        int i = 0;

        public CircularEnumerator(List<T> list){
            this.list = list;
        }

        public T Current => list[i];

        object IEnumerator.Current => this;

        public void Dispose()
        {
        
        }

        public bool MoveNext()
        {
            //i = (i + 1) % list.Count;
            return true;
        }

        public void Reset()
        {
            i = 0;
        }
    }
}