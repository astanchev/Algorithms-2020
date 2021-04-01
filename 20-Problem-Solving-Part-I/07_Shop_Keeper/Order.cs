namespace _07_Shop_Keeper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Order : IComparable<Order>
    {
        private static int dummyId = int.MaxValue;

        public int Id { get; set; }

        public Stack<int> Indexes { get; set; }

        public int PeekNextIndex()
        {
            if (this.Indexes.Count <= 0)
            {
                this.Indexes.Push(dummyId--);
            }

            return this.Indexes.Peek();
        }

        public Order(int id)
        {
            this.Id = id;
            this.Indexes = new Stack<int>();
        }

        public Order()
        {
            this.Indexes = new Stack<int>();
            this.Indexes.Push(dummyId);
            this.Id = dummyId--;
        }

        public int CompareTo(Order other)
        {
            return this.PeekNextIndex().CompareTo(other.PeekNextIndex());
        }

        public override string ToString()
        {
            return $"{this.Id} : {string.Join(", ", this.Indexes)}";
        }
    }
}
