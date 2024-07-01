
using Microsoft.AspNetCore.Mvc;
using payrollCalculatorApi.Models;
namespace payrollCalculatorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryCalculatorController : ControllerBase
    {
        [HttpPost]
        public ActionResult<SalaryCalculationResponse> CalculateSalary(SalaryCalculationRequest request)
        {
            var response = new SalaryCalculationResponse();

            // Define the managementLevels dictionary here
            var managementLevels = new Dictionary<string, int>
    {
        { "management level 1", 1 },
        { "management level 2", 2 },
        { "management level 3", 3 },
        { "management level 4", 4 }
    };

            // Basic salary calculation
            decimal basicSalaryPerHour = request.ProfessionalLevel switch
            {
                "beginner" => 100,
                "experienced" => 120,
                _ => 0
            };

            int managementLevelIncrement = 0;
            if (request.ManagementLevel != "none" && managementLevels.ContainsKey(request.ManagementLevel.ToLower()))
            {
                managementLevelIncrement = managementLevels[request.ManagementLevel.ToLower()] * 20;
            }

            basicSalaryPerHour += managementLevelIncrement;

            decimal monthlyHours = request.PartTimeJob switch
            {
                "100%" => 170,
                "75%" => 127.5m,
                "50%" => 85,
                _ => 170
            };

            decimal basicSalary = basicSalaryPerHour * monthlyHours;
            response.BasicSalary = basicSalary;

            // Seniority increment calculation
            decimal seniorityIncrementRate = request.YearsOfSeniority * 1.25m;
            response.SeniorityIncrementRate = seniorityIncrementRate;
            decimal totalSeniorityAddition = (seniorityIncrementRate / 100) * basicSalary;
            response.TotalSeniorityAddition = totalSeniorityAddition;

            // Overtime work calculation
            decimal overtimeAdditionRate = request.OvertimeEligible == "yes"
                ? request.OvertimeGroup switch
                {
                    "group A" => 1,
                    "group B" => 0.5m,
                    _ => 0
                }
                : 0;

            decimal totalOvertimeAddition = (overtimeAdditionRate / 100) * basicSalary;
            response.TotalOvertimeAddition = totalOvertimeAddition;

            // Total base salary before increase
            decimal totalBaseSalaryBeforeIncrease = basicSalary + totalSeniorityAddition + totalOvertimeAddition;
            response.TotalBaseSalaryBeforeIncrease = totalBaseSalaryBeforeIncrease;

            // Salary increase supplement calculation
            decimal salaryIncreaseRate = totalBaseSalaryBeforeIncrease switch
            {
                <= 20000 => 1.5m,
                <= 30000 => 1.25m,
                _ => 1
            };

            if (managementLevels.ContainsKey(request.ManagementLevel.ToLower()))
            {
                salaryIncreaseRate += managementLevels[request.ManagementLevel.ToLower()] * 0.1m;
            }

            decimal totalSalaryIncrease = (salaryIncreaseRate / 100) * totalBaseSalaryBeforeIncrease;
            response.SalaryIncreaseRate = salaryIncreaseRate;
            response.TotalSalaryIncrease = totalSalaryIncrease;

            // New base salary calculation
            decimal totalBaseSalaryAfterIncrease = totalBaseSalaryBeforeIncrease + totalSalaryIncrease;
            response.TotalBaseSalaryAfterIncrease = totalBaseSalaryAfterIncrease;

            return Ok(response);
        }
    }
}
