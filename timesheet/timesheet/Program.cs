// Program.cs
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using timesheet.Models;

namespace TimesheetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TimesheetsDbContext>();
            optionsBuilder
                .UseSqlServer("Server=127.0.0.1,1435;Database=TimesheetsDb;uid=sa;pwd=Password#123;TrustServerCertificate=True;", options =>
                {
                    options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                })
                .LogTo(Console.WriteLine, LogLevel.Information);  // Logging to console

            try
            {
                using (var context = new TimesheetsDbContext(optionsBuilder.Options))
                {
                    context.Database.OpenConnection();  // Test connection
                    Console.WriteLine("Database connection successful.");

                    // Loop untuk logging timesheet
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Employee Timesheet Management System");

                            Console.Write("Enter Employee ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int employeeId))
                            {
                                Console.WriteLine("Invalid Employee ID. Please enter a valid number.");
                                continue;
                            }

                            var employee = context.Employees.Find(employeeId);
                            if (employee == null)
                            {
                                Console.WriteLine("Employee not found. Please try again.");
                                continue;
                            }

                            Console.Write("Enter Work Date (YYYY-MM-DD): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                            {
                                Console.WriteLine("Invalid date format. Please enter in YYYY-MM-DD format.");
                                continue;
                            }

                            Console.Write("Enter Hours Worked: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal hoursWorked))
                            {
                                Console.WriteLine("Invalid hours worked format. Please enter a valid number.");
                                continue;
                            }

                            var timesheet = new Timesheet
                            {
                                EmployeeId = employee.EmployeeId,
                                Date = date,
                                HoursWorked = hoursWorked,
                                Status = "Pending"
                            };

                            context.Timesheets.Add(timesheet);
                            context.SaveChanges();
                            Console.WriteLine("Timesheet logged successfully.");
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Input format is incorrect: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }

                        Console.WriteLine("Do you want to log another timesheet? (y/n)");
                        if (Console.ReadLine().ToLower() != "y")
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection error: {ex.Message}");
            }
        }
    }
}
