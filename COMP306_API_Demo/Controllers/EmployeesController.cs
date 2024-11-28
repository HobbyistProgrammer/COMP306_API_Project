using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProductLibrary.Entities;
using COMP306_API_Demo.Models;
using COMP306_API_Demo.Services;

namespace COMP306_API_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeSummaryDto>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            var employeeSummaries = _mapper.Map<IEnumerable<EmployeeSummaryDto>>(employees);
            return Ok(employeeSummaries);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDetailDto>> GetEmployee(long id)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDetails = _mapper.Map<EmployeeSummaryDto>(employee);
            return Ok(employeeDetails);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, EmployeeCreateUpdateDto employeeDto)
        {
            // Map the DTO to the Product entity
            var employee = _mapper.Map<Employee>(employeeDto);
            // Set the ID directly, as it is passed in the route
            employee.Id = id;

            await _employeeRepository.UpdateEmployeeAsync(employee);
            return NoContent();
        }

        // PATCH: api/Products/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEmployee(long id, [FromBody] JsonPatchDocument<EmployeeCreateUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            // Retrieve the existing product
            var employee = await _employeeRepository.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Map the product entity to a DTO for patching
            var employeeDto = _mapper.Map<EmployeeCreateUpdateDto>(employee);

            // Apply the patch to the DTO
            patchDoc.ApplyTo(employeeDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map the patched DTO back to the product entity
            _mapper.Map(employeeDto, employee);

            // Save the changes to the database
            await _employeeRepository.UpdateEmployeeAsync(employee);

            return NoContent();
        }


        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<EmployeeDetailDto>> PostEmployee(EmployeeCreateUpdateDto employeeDto)
        {
            var existingEmployees = await _employeeRepository.GetAllEmployeesAsync();
            var newId = existingEmployees.Count() + 1;

            // Map the DTO to the Product entity
            var employee = _mapper.Map<Employee>(employeeDto);
            employee.Id = newId;

            await _employeeRepository.AddEmployeeAsync(employee);

            // Map back to ProductDetailsDto to return detailed information in response
            var employeesDetails = _mapper.Map<EmployeeDetailDto>(employee);
            return CreatedAtAction(nameof(GetEmployees), new { id = employeesDetails.Id }, employeesDetails);
        }


        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
