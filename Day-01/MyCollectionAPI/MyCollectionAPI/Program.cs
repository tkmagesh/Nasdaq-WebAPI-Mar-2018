using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace MyCollectionAPI
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
    public class ProductsCollection : IEnumerator, IEnumerable
    {
        private ArrayList list = new ArrayList();

        public void Add(Product product)
        {
            list.Add(product);
        }
        public void Remove(int index)
        {
            list.RemoveAt(index);
        }
        /*
        public Product Get(int index)
        {
            return (Product)list[index];
        }
         * */
        public Product this[int index]
        {
            get
            {
                return (Product)list[index];
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
            get { return this.list[this.index];  }
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

        public void Sort()
        {
            for(var i=0; i < this.list.Count-1; i++)
                for (var j = i + 1; j < this.list.Count; j++)
                {
                    var p1 = (Product) this.list[i];
                    var p2 = (Product) this.list[j];
                    if (p1.Id > p2.Id)
                    {
                        var temp = this.list[i];
                        this.list[i] = this.list[j];
                        this.list[j] = temp;
                    }
                }
        }

        public void Sort(IProductComparer comparer)
        {
            for (var i = 0; i < this.list.Count - 1; i++)
                for (var j = i + 1; j < this.list.Count; j++)
                {
                    var p1 = (Product)this.list[i];
                    var p2 = (Product)this.list[j];
                    if ( comparer.Compare(p1, p2) > 0)
                    {
                        var temp = this.list[i];
                        this.list[i] = this.list[j];
                        this.list[j] = temp;
                    }
                }
        }

        public void Sort(ProductCompareDelegate comparer)
        {
            for (var i = 0; i < this.list.Count - 1; i++)
                for (var j = i + 1; j < this.list.Count; j++)
                {
                    var p1 = (Product)this.list[i];
                    var p2 = (Product)this.list[j];
                    if (comparer(p1, p2) > 0)
                    {
                        var temp = this.list[i];
                        this.list[i] = this.list[j];
                        this.list[j] = temp;
                    }
                }
        }
        public ProductsCollection Filter(IProductCriteria criteria)
        {
            var result = new ProductsCollection();
            foreach (var item in this.list)
            {
                var product = (Product)item;
                if (criteria.IsSatisfiedBy(product))
                    result.Add(product);
            }
            return result;
        }

        public ProductsCollection Filter(ProductCriteriaDelegate criteria)
        {
            var result = new ProductsCollection();
            foreach (var item in this.list)
            {
                var product = (Product)item;
                if (criteria(product))
                    result.Add(product);
            }
            return result;
        }
    }

    public class CostlyProductCriteria : IProductCriteria
    {

        public bool IsSatisfiedBy(Product product)
        {
            return product.Cost > 25;
        }
    }

    public class StationaryProductCriteria : IProductCriteria
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
    public delegate bool ProductCriteriaDelegate(Product product);

    public interface IProductComparer
    {
        int Compare(Product p1, Product p2);
    }

    public delegate int ProductCompareDelegate(Product p1, Product p2);

    public class DescendingProductComparer : IProductComparer
    {
        private IProductComparer _comparer = null;
        public DescendingProductComparer(IProductComparer comparer)
        {
            this._comparer = comparer;
        }
        public int Compare(Product p1, Product p2)
        {
            return this._comparer.Compare(p1, p2) * -1;
        }
    }

    public class ProductComparerById : IProductComparer
    {
        public int Compare(Product p1, Product p2)
        {
            if (p1.Id < p2.Id) return -1;
            if (p1.Id == p2.Id) return 0;
            return 1;
        }
    }
    public class ProductComparerByCost : IProductComparer
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
            var products = new ProductsCollection();
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
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
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
            var understockedProducts = products.Filter(product => product.Units < 50 );
            foreach (var product in understockedProducts)
            {
                Console.WriteLine(product);
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



