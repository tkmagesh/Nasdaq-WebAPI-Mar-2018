﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace IEnumerableExtensionsDemo
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int Units { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return String.Format("\t{0}\t{1}\t{2}\t{3}\t{4}\t", this.Id, this.Name, this.Cost, this.Units, this.Category);
        }
    }

    public static class MyExtensions
    {
        /*
        public static void Sort<T>(this IEnumerable<T> list, IItemComparer<T> comparer)
        {
            
            for (var i = 0; i < list.Count - 1; i++)
                for (var j = i + 1; j < this.list.Count; j++)
                {
                    var p1 = (T)this.list[i];
                    var p2 = (T)this.list[j];
                    if (comparer.Compare(p1, p2) > 0)
                    {
                        var temp = this.list[i];
                        this.list[i] = this.list[j];
                        this.list[j] = temp;
                    }
                }
        }

        public void Sort(ItemCompareDelegate<T> comparer)
        {
            for (var i = 0; i < this.list.Count - 1; i++)
                for (var j = i + 1; j < this.list.Count; j++)
                {
                    var p1 = (T)this.list[i];
                    var p2 = (T)this.list[j];
                    if (comparer(p1, p2) > 0)
                    {
                        var temp = this.list[i];
                        this.list[i] = this.list[j];
                        this.list[j] = temp;
                    }
                }
        }
         */ 
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, IItemCriteria<T> criteria)
        {

            foreach (var item in list)
            {
                var tItem = (T)item;
                if (criteria.IsSatisfiedBy(tItem))
                    yield return tItem;
            }

        }

        //public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, ItemCriteriaDelegate<T> criteria)
        //public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Func<T, bool> criteria)
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Predicate<T> criteria)
        {
            foreach (var item in list)
            {
                var tItem = (T)item;
                if (criteria(tItem))
                    yield return tItem;
            }

        }

        //public static IDictionary<TKey, List<T>> GroupBy<T, TKey>(this IEnumerable<T> list, KeySelectorDelegate<T, TKey> keySelector)
        /*public static IDictionary<TKey, List<T>> GroupBy<T, TKey>(this IEnumerable<T> list, Func<T, TKey> keySelector)
        {
            var result = new Dictionary<TKey, List<T>>();
            foreach (var item in list)
            {
                var tItem = (T)item;
                var key = keySelector(tItem);
                if (!result.ContainsKey(key))
                {
                    result.Add(key, new List<T>());
                }
                result[key].Add(tItem);
            }
            return result;
        }*/

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
                action(item);
        }
    }
   

    public class MyCollection<T> : IEnumerator, IEnumerable, IEnumerable<T>, IEnumerator<T>
    {
        private ArrayList list = new ArrayList();

        public void Add(T item)
        {
            list.Add(item);
        }
        public void Remove(int index)
        {
            list.RemoveAt(index);
        }

        public T Get(int index)
        {
            return (T)list[index];
        }

        public T this[int index]
        {
            get
            {
                return (T)list[index];
            }
        }
        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        //IEnumerable Members
        private int index = -1;
        public object Current
        {
            get { return this.list[this.index]; }
        }

        public bool MoveNext()
        {
            ++this.index;
            if (this.index >= list.Count)
            {
                this.Reset();
                return false;
            }
            return true;
        }

        public void Reset()
        {
            this.index = -1;
        }

        //IEnumerator Member
        public IEnumerator GetEnumerator()
        {
            return this;
        }



        public void Sort(IItemComparer<T> comparer)
        {
            for (var i = 0; i < this.list.Count - 1; i++)
                for (var j = i + 1; j < this.list.Count; j++)
                {
                    var p1 = (T)this.list[i];
                    var p2 = (T)this.list[j];
                    if (comparer.Compare(p1, p2) > 0)
                    {
                        var temp = this.list[i];
                        this.list[i] = this.list[j];
                        this.list[j] = temp;
                    }
                }
        }

        //public void Sort(ItemCompareDelegate<T> comparer)
        public void Sort(Func<T,T, int> comparer)
        {
            for (var i = 0; i < this.list.Count - 1; i++)
                for (var j = i + 1; j < this.list.Count; j++)
                {
                    var p1 = (T)this.list[i];
                    var p2 = (T)this.list[j];
                    if (comparer(p1, p2) > 0)
                    {
                        var temp = this.list[i];
                        this.list[i] = this.list[j];
                        this.list[j] = temp;
                    }
                }
        }
       

       

        T IEnumerator<T>.Current
        {
            get { return (T) this.Current; }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this;
        }
    }

    //public delegate TKey KeySelectorDelegate<T, TKey>(T item);

    public class CostlyProductCriteria : IItemCriteria<Product>
    {

        public bool IsSatisfiedBy(Product product)
        {
            return product.Cost > 25;
        }
    }

    public class StationaryProductCriteria : IItemCriteria<Product>
    {
        public bool IsSatisfiedBy(Product product)
        {
            return product.Category == "Stationary";
        }
    }


    public interface IProductCriteria
    {
        bool IsSatisfiedBy(Product product);
    }

    public interface IItemCriteria<T>
    {
        bool IsSatisfiedBy(T item);
    }
    //public delegate bool ProductCriteriaDelegate(Product product);

    //public delegate bool ItemCriteriaDelegate<T>(T item);

    public interface IProductComparer
    {
        int Compare(Product p1, Product p2);
    }

    public interface IItemComparer<T>
    {
        int Compare(T p1, T p2);
    }

    //public delegate int ProductCompareDelegate(Product p1, Product p2);

    //public delegate int ItemCompareDelegate<T>(T p1, T p2);

    public class DescendingProductComparer : IItemComparer<Product>
    {
        private IItemComparer<Product> _comparer = null;
        public DescendingProductComparer(IItemComparer<Product> comparer)
        {
            this._comparer = comparer;
        }
        public int Compare(Product p1, Product p2)
        {
            return this._comparer.Compare(p1, p2) * -1;
        }
    }

    public class ProductComparerById : IItemComparer<Product>
    {
        public int Compare(Product p1, Product p2)
        {
            if (p1.Id < p2.Id) return -1;
            if (p1.Id == p2.Id) return 0;
            return 1;
        }
    }
    public class ProductComparerByCost : IItemComparer<Product>
    {
        public int Compare(Product p1, Product p2)
        {
            if (p1.Cost < p2.Cost) return -1;
            if (p1.Cost == p2.Cost) return 0;
            return 1;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var numbers = new int[] {10,20,30,40};
            for (int index = 0, count = numbers.Length; index < count; index++)
            {
                Console.WriteLine(numbers[index]);
            }
            Console.ReadLine();
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
            Console.ReadLine();
             * */
            var numbers = new int[] { 10, 20, 30, 40 };
            
           

            var products = new MyCollection<Product>();
            products.Add(new Product { Id = 3, Name = "Pen", Cost = 5, Units = 50, Category = "Stationary" });
            products.Add(new Product { Id = 5, Name = "Len", Cost = 50, Units = 30, Category = "Grocery" });
            products.Add(new Product { Id = 2, Name = "Ten", Cost = 10, Units = 60, Category = "Stationary" });
            products.Add(new Product { Id = 9, Name = "Den", Cost = 25, Units = 20, Category = "Grocery" });
            products.Add(new Product { Id = 7, Name = "Zen", Cost = 35, Units = 30, Category = "Stationary" });

            //Console.WriteLine("Product # : {0}", products.Count);
            /*
             * for (int index = 0, count = products.Count; index < count; index++)
            {
                Console.WriteLine(products[index]);
            }
             * */
            Console.WriteLine("Default List");
            /*
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }*/
            products.ForEach(product => Console.WriteLine(product));
            Console.WriteLine();

            Console.WriteLine("After sorting (By Id)");
            products.Sort(new ProductComparerById());
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            Console.WriteLine("After sorting (By Cost)");
            products.Sort(new ProductComparerByCost());
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            Console.WriteLine("After sorting (By Cost - descending)");
            products.Sort(new DescendingProductComparer(new ProductComparerByCost()));
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            Console.WriteLine("All costly products (cost > 25)");
            var costlyProducts = products.Filter(new CostlyProductCriteria());
            foreach (var product in costlyProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            Console.WriteLine("All stationary products");
            var stationaryProducts = products.Filter(new StationaryProductCriteria());
            foreach (var product in stationaryProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();


            Console.WriteLine("All understocked products (units < 50)");
            //var understockedProducts = products.Filter(Program.IsUnderstocked);
            /*
            var understockedProducts = products.Filter(delegate(Product product)
            {
                return product.Units < 50;
            });
            */
            /*
            var understockedProducts = products.Filter((product) =>
            {
                return product.Units < 50;
            });
             * */
            var understockedProducts = products.Where(product => product.Units < 50);
            foreach (var product in understockedProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            Console.WriteLine("Group By category");
            var productsByCategory = products.GroupBy(product => product.Category);

            foreach (var groupedItem in productsByCategory)
            {
                Console.WriteLine("Key - {0}", groupedItem.Key);
                foreach (var item in groupedItem)
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine();

            Console.WriteLine("Group By cost");
            var productsByCost = products.GroupBy(product => product.Cost > 25 ? "costly" : "affordable");

            foreach (var groupedItem in productsByCost)
            {
                Console.WriteLine("Key - {0}", groupedItem.Key);
                foreach (var item in groupedItem)
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine();

            Console.ReadLine();
        }
        /*
        public static bool IsUnderstocked(Product product)
        {
            return product.Units < 50;
        }
        */
    }
}
