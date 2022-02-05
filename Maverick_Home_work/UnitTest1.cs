using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maverick_Home_work
{
	[TestClass]
	public class UnitTest1
	{
		private partial class Mock<T> : ISalaryRepo
		{
			public decimal Get(int id)
			{
				return 1200;
			}
		}

		private partial class Mock<T> : IDebugHelper
		{
			public void Info(string message)
			{
				Console.WriteLine(message);
			}
		}
		

		private Mock<ISalaryRepo> _salaryRepo;
		private Mock<IDebugHelper> _debugHelper;

		[TestMethod]
		public void calculate_LessThanOneYearEmployee_Bonus()
		{
			_salaryRepo = new Mock<ISalaryRepo>();
			_debugHelper = new Mock<IDebugHelper>();
			//if my monthly salary is 1200, working year is 0.5, my bonus should be 600
			var lessThanOneYearEmployee = new LessThanOneYearEmployee(_salaryRepo, _debugHelper)
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

		protected decimal GetMonthlySalary(ISalaryRepo salaryRepo, IDebugHelper debugHelper)
		{
			debugHelper.Info($"query monthly salary id:{Id}");
			return salaryRepo.Get(this.Id);
		}

		public abstract decimal GetYearlyBonus();

		public int Id { get; set; }
	}

	public class LessThanOneYearEmployee : Employee
	{
		private readonly ISalaryRepo _salaryRepo;
		private readonly IDebugHelper _debugHelper;

		public LessThanOneYearEmployee(ISalaryRepo salaryRepo, IDebugHelper debugHelper)
		{
			_salaryRepo = salaryRepo;
			_debugHelper = debugHelper;
		}
		

		public override decimal GetYearlyBonus()
		{
			_debugHelper.Info("--get yearly bonus--");
			var salary = this.GetMonthlySalary(_salaryRepo, _debugHelper);
			_debugHelper.Info($"id:{Id}, his monthly salary is:{salary}");
			return Convert.ToDecimal(this.WorkingYear()) * salary;
		}

		private double WorkingYear()
		{
			_debugHelper.Info("--get working year--");
			var year = (Today - StartWorkingDate).TotalDays / 365;
			return year > 1 ? 1 : Math.Round(year, 2);
		}
	}
}