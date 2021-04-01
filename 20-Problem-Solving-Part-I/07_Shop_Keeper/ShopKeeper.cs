namespace _07_Shop_Keeper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShopKeeper
    {
        private static Dictionary<int, Order> orders;
        private static int[] initialStock;
        private static int[] ordersList;
        private static SortedSet<Order> shop;

        public static void Main(string[] args)
        {
            initialStock = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            ordersList = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            if (!initialStock.Contains(ordersList[0]))
            {
                Console.WriteLine("impossible");
                return;
            }

            orders = new Dictionary<int, Order>();
            FillOrders();

            shop = new SortedSet<Order>();
            FillShop();

            var stockChanges = 0;

            foreach (var order in ordersList)
            {
                var currentOrder = orders[order];

                if (!shop.Contains(currentOrder))
                {
                    // Console.WriteLine($"{shop.Last().Id} <<< {order}");
                    shop.Remove(shop.Last());
                    shop.Add(currentOrder);
                    stockChanges++;
                }

                shop.Remove(currentOrder);
                currentOrder.Indexes.Pop();
                shop.Add(currentOrder);
            }

            Console.WriteLine(stockChanges);
        }

        private static void FillShop()
        {
            foreach (var product in initialStock.Distinct())
            {
                shop.Add(!orders.ContainsKey(product) ?
                                            new Order() :
                                            orders[product]);
            }
        }

        private static void FillOrders()
        {
            for (var index = ordersList.Length - 1; index >= 0; index--)
            {
                var order = ordersList[index];

                if (!orders.ContainsKey(order))
                {
                    orders.Add(order, new Order(order));
                }

                orders[order].Indexes.Push(index);
            }
        }
    }
}
