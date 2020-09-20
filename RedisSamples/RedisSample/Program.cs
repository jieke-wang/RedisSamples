using System;

namespace RedisSample
{
    class Program
    {
        static void Main(string[] args)
        {
            HashDemo();
            Console.WriteLine("Hello World!");
        }
        static void HashDemo()
        {
            Person person = new Person();
            person.SetPerson();
            string key = "person";

            HashSample hashSample = new HashSample("192.168.199.129:6379,password=password", 0);
            hashSample.HashSet(key, person);

            Person person2 = hashSample.HashGet<Person>(key);
        }
    }

    public class Person
    {
        public string Name { get; set; } 
        public string Address { get; set; }
        public int Age { get; set; } 
        public double Salary { get; set; }
        public bool Adult { get; set; } 
        public DateTime Birthday { get; set; }

        public void SetPerson()
        {
            Name = "jieke";
            Address = null;
            Age = 10;
            Salary =  0.01;
            Adult = false;
            Birthday = DateTime.Now;
        }
    }
}
