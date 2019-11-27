using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maverick_Home_work
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void calculate_LessThanOneYearEmployee_Bonus()
		{
			//if my monthly salary is 1200, working year is 0.5, my bonus should be 600
			var lessThanOneYearEmployee = new LessThanOneYearEmployee()
			{
				Id = 91,
				//Console.WriteLine("your StartDate should be :{0}", DateTime.Today.AddDays(365/2*-1));
				Today = new DateTime(2018, 1, 27),
				StartWorkingDate = new DateTime(2017, 7, 29)
			};

			var actual = lessThanOneYearEmployee.GetYearlyBonus();
			Assert.AreEqual(600, actual);
		}
	}

	public abstract class Employee
	{
		public DateTime StartWorkingDate { get; set; }
		public DateTime Today { get; set; }

		protected decimal GetMonthlySalary()
		{
			DebugHelper.Info($"query monthly salary id:{Id}");
			return SalaryRepo.Get(this.Id);
		}

		public abstract decimal GetYearlyBonus();

		public int Id { get; set; }
	}

	public class LessThanOneYearEmployee : Employee
	{
		public override decimal GetYearlyBonus()
		{
			DebugHelper.Info("--get yearly bonus--");
			var salary = this.GetMonthlySalary();
			DebugHelper.Info($"id:{Id}, his monthly salary is:{salary}");
			return Convert.ToDecimal(this.WorkingYear()) * salary;
		}

		private double WorkingYear()
		{
			DebugHelper.Info("--get working year--");
			var year = (Today - StartWorkingDate).TotalDays / 365;
			return year > 1 ? 1 : Math.Round(year, 2);
		}
	}

	public static class DebugHelper
	{
		public static void Info(string message)
		{
			//you can't modified this function
			throw new NotImplementedException();
		}
	}

	public static class SalaryRepo
	{
		internal static decimal Get(int id)
		{
			//you can't modified this function
			throw new NotImplementedException();
		}
	}
}