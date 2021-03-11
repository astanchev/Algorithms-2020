namespace _04_Salaries
{
    using System;
    using System.Collections.Generic;

    class Salaries
    {
        private static int[,] employees;
        private static Dictionary<int, int> salaries;

        static void Main(string[] args)
        {
            int employeesCount = int.Parse(Console.ReadLine());
            employees = new int[employeesCount, employeesCount];
            salaries = new Dictionary<int, int>();

            FillEmployees();
            
            int totalSalaries = SumSalaries();

            Console.WriteLine(totalSalaries);
        }

        private static int SumSalaries()
        {
            int salary = 0;

            for (int e = 0; e < employees.GetLength(0); e++)
            {
                salary += GetSalary(e);
            }

            return salary;
        }

        private static int GetSalary(int employee)
        {
            int salary = 0;

            for (int i = 0; i < employees.GetLength(1); i++)
            {
                if (employees[employee, i] != -1)
                {
                    if (!salaries.ContainsKey(i))
                    {
                        salaries[i] = GetSalary(i);
                    }

                    salary += salaries[i];
                }
            }

            return salary == 0 ? 1 : salary;
        }

        private static void FillEmployees()
        {
            for (int row = 0; row < employees.GetLength(0); row++)
            {
                var line = Console.ReadLine();

                for (int col = 0; col < employees.GetLength(1); col++)
                {
                    var letter = line[col];

                    if (letter == 'N')
                    {
                        employees[row, col] = -1;
                    }
                    else if (letter == 'Y')
                    {
                        employees[row, col] = col;
                    }
                }
            }
        }
    }
}
