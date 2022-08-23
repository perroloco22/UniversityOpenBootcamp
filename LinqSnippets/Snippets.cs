using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LinqSnippets
{
    public class Snippets
    {


        public static void BasicLinq()
        {
            string[] cars =
            {
                "Ford FIesta",
                "VW Golf",
                "VW Gol Trend",
                "Ford Focus",
                "Audi A3",
                "Audi A4",
                "Fiat Cronos"
            };

            // select * from 
            var carsSelect = from car in cars
                             select car;
            var carSelect = cars.Select(car => car);
            foreach (var item in carsSelect)
            {
                Console.WriteLine(item);
            }

            //select car where is audi

            var audiCar = from car in cars
                          where car.Contains("Audi")
                          select car;
            foreach (var item in audiCar)
            {
                Console.WriteLine(item);
            }


        }

        public static void LinqNumbers()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //Each number multiplied  by 3
            //take all number, but 9 
            // Orders number by ascending value


            var processedNumbersList = numbers.
                                            Select(x => x * 3). //{3,6,9,etc...}
                                            Where(x => x != 9).//all number but 9
                                            OrderBy(x => x);//order ascending
            foreach (var number in processedNumbersList)
            {
                Console.WriteLine(number.ToString());
            }
                                                
        }

        public static void SearchExample()
        {
            List<string> textList = new List<string>()
            {
                "a",
                "b",
                "cx", 
                "d",
                "e",
                "cj",
                "f",
                "c"
            };


            //first element of all list
            var element = textList.First();
            //firs element thats is "C"
            var cText = textList.First(x => x.Equals("c"));
            //firs element contains "J"
            var jText = textList.First(x => x.Contains("j"));
            //firs element contains "z" or return default value
            var firstOrDefault = textList.FirstOrDefault(x => x.Contains("z"));
            //single elements
            var uniqueTexts = textList.Single();
            var uniqueOrDefault = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers);// => 4,8 

        }

        public static void MultipleSelect()
        {
            //SELECT MANY
            string[] myOptions =
            {
                "options1 , text1 ",
                "options2 , text2 ",
                "options3 , text3 ",
            };
            //var miOptionsSelectMany = myOptions.SelectMany(x => x.Split(","));
            //var miOptionsSelectMany = myOptions.SelectMany(x=> x.Select(y=>y));
            //var mioptions2 = myOptions.SelectMany();  

            var enterprise = new[]
            {
                new Enterprise
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id=1,
                            Name="Matias",
                            Email="matias@gmail.com",
                            Salary = 5000,

                        },
                        new Employee
                        {
                            Id=2,
                            Name="Emanuel",
                            Email="Emanuel@gmail.com",
                            Salary = 4000,

                        },
                        new Employee
                        {
                            Id=3,
                            Name="Abril",
                            Email="Abril@gmail.com",
                            Salary = 2000,

                        },
                    }
                },
                new Enterprise
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id=4,
                            Name="Roma",
                            Email="Roma@gmail.com",
                            Salary = 1600,

                        },
                        new Employee
                        {
                            Id=5,
                            Name="Maria",
                            Email="Maria@gmail.com",
                            Salary = 1000,

                        },
                        new Employee
                        {
                            Id=6,
                            Name="Mica",
                            Email="Mica@gmail.com",
                            Salary = 3000,

                        },
                    }
                },
            };

            //get all Employees
            var employeesList = enterprise.Select(enterprise => enterprise.Employees);

            //know if ana list is empty
            bool hasEnterprise = enterprise.Any();

            //know if any enterpise has employees 
            bool hasEmployee = enterprise.Any(enterprise => enterprise.Employees.Any());
            //know if has any employees with minor $1000 of salary
            bool hasEmployeesWithSalaryMore1000 =
                enterprise.Any(enterprise => enterprise.Employees.Any
                (emp => emp.Salary <= 1000)
                );
        }

        static void LinqCollection()
        {
            var firstList = new List<string>{"a","b","c"};
            var secondList = new List<string>{"a","c","d"};

            //inner join => common elements
            //return =>

            //    {
            //        element = "a",
            //        secondElement = "c" 
            //    }


            var commonElementList = from element in firstList
                                    join secondElement in secondList
                                    on element equals secondElement
                                    select new
                                    {
                                        ElementList1 = element,
                                        ElementList2 = secondElement
                                    };

            //another way

            var commonElementSecondList = firstList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element,secondElement) => new { ElementList1 = element, ElementList2 = secondElement }
                );

            //OUTER JOIN - LEFT

            var queryLeftOuterJoin = from element in firstList
                                     join secondElement in secondList
                                     on element equals secondElement
                                     into temporalList
                                     from temporalElement in temporalList.DefaultIfEmpty()
                                     where element != temporalElement
                                     select new { Element = element };

            var leftOuterJoin = firstList.
                                Where(element => !firstList.
                                Join(secondList, element => element,
                                     secondElement => secondElement,
                                     (element, secondElement) => new { ElementList1 = element, ElementList2 = secondElement }).
                                Select(x => x.ElementList1.ToString()).
                                Contains(element)
                                );
            //OUTER JOIN - RIGHT

            var queryRightOuterJoin = from element in secondList
                                     join secondElement in firstList
                                     on element equals secondElement
                                     into temporalList
                                     from temporalElement in temporalList.DefaultIfEmpty()
                                     where element != temporalElement
                                     select new { Element = element };

            //UNION

            var unionList = queryRightOuterJoin.Union(queryLeftOuterJoin);

        }

        static void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9
            };

            var skipTwoFirstValues = myList.Skip(2); // {3,4,5,6,7,8,9}
            var skipTwoLastValues = myList.SkipLast(2); // {1,2,3,4,5,6,7}
            var skipWhile = myList.SkipWhile(num => num < 4); // {4,5,6,7,8,9}

            //TAKE
            var takeFirstTwo = myList.Take(2); // {1,2}
            var takeLastTwo = myList.Take(2); // {8,9}
            var TakeWhile = myList.TakeWhile(num => num < 4); // {1,2,3}

        }

        //PAGING

        public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int starIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(starIndex).Take(resultsPerPage);

        }

        //VARIABLES

        static void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 6, 7, 8, 9 };

            var aboveAverage = from number in numbers
                               let avg = numbers.Average()
                               let nSquared = Math.Pow(number,2)
                               where nSquared > avg
                               select number;
        }

        //ZIP

        //REPEAT

        //ALL

        //AGGREGATE

        static void agreggateQuery()
        {
            int[] numbers = {1,2, 3, 4, 5, 6, 7,8,9,10};
            int sum = numbers.Aggregate((prev, current) => prev + current);
        }


        //DISTINCT

        //GROUPBY
    }



}