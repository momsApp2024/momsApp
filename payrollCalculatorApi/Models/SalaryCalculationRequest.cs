namespace payrollCalculatorApi.Models
{
    public class SalaryCalculationRequest
    {
        public string PartTimeJob { get; set; }
        public string ProfessionalLevel { get; set; }
        public string ManagementLevel { get; set; }
        public int YearsOfSeniority { get; set; }
        public string OvertimeEligible { get; set; }
        public string OvertimeGroup { get; set; }
    }
}
