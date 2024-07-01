namespace payrollCalculatorApi.Models
{
    public class SalaryCalculationResponse
    {
        public decimal BasicSalary { get; set; }
        public decimal SeniorityIncrementRate { get; set; }
        public decimal TotalSeniorityAddition { get; set; }
        public decimal TotalOvertimeAddition { get; set; }
        public decimal TotalBaseSalaryBeforeIncrease { get; set; }
        public decimal SalaryIncreaseRate { get; set; }
        public decimal TotalSalaryIncrease { get; set; }
        public decimal TotalBaseSalaryAfterIncrease { get; set; }
    }
}
