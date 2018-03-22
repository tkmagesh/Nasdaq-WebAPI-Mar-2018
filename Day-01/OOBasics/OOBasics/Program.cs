using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOBasics
{
    interface ICricketer
    {
        void Play();
    }
    abstract class Person
    {
        //state & behavior
        //state = fields & properties

        //public string FirstName;
        /*
        private string _firstName;

        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                this._firstName = value;
            }
        }
         */
        public string FirstName { get; set; }

        public string LastName;
        private DateTime _dob;
        public DateTime DOB
        {
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Invalid DOB. DOB has to be in the past!");
                    
                }
                this._dob = value;
            }
            get
            {
                return this._dob;
            }
        }

        public virtual void  ()
        {
           Console.WriteLine("{0}\t{1}\t{2}", this.FirstName, this.LastName, this.DOB);
        }
    }

    class Employee : Person
    {
        public decimal Salary { get; set; }

        public override void Display()
        {           
            Console.WriteLine("{0}\t{1}\t{2}\t{3}", this.FirstName, this.LastName, this.DOB, this.Salary);
        }
    }

     class CricketPlayer : Employee, ICricketer
    {
        public void Play()
        {
            Console.WriteLine("{0} {1} playing in the field", this.FirstName, this.LastName);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            Person x = new Person();

            Employee e = new Employee();
            e.FirstName = "Magesh";
            e.LastName = "Kuppan";
            e.DOB = new DateTime(1997, 3, 10);
            e.Salary = 20000;
            Console.WriteLine("As an Employee");
            e.Display();


            Person p = e;
            Console.WriteLine("As a Person");
            p.Display();

            var cricketPlayer1 = new CricketPlayer { FirstName = "Sachin", LastName = "Tendulkar" };
            cricketPlayer1.Play();

            Console.ReadKey();
        }
    }
}
