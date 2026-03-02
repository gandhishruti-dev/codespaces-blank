using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Controllers;
using MyWebApi.Model;
using MyWebApi.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApi.Tests
{
    public class PatientControllerTests
    {
        private readonly Mock<IPatientService> _mockPatientService;
        private readonly PatientController _controller;

        public PatientControllerTests()
        {
            _mockPatientService = new Mock<IPatientService>();
            _controller = new PatientController(_mockPatientService.Object);
        }

        #region GetPatient Tests
        
        [Fact]
        public async Task GetPatient_WithValidId_ReturnsOkResultWithPatient()
        {
            // Arrange
            int patientId = 1;
            var patient = new Patient 
            { 
                Id = 1, 
                FirstName = "John", 
                LastName = "Doe", 
                Age = 30, 
                Gender = "Male" 
            };
            _mockPatientService.Setup(s => s.GetPatient(patientId)).ReturnsAsync(patient);

            // Act
            var result = await _controller.GetPatient(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPatient = Assert.IsType<Patient>(okResult.Value);
            Assert.Equal(patientId, returnedPatient.Id);
            Assert.Equal("John", returnedPatient.FirstName);
            _mockPatientService.Verify(s => s.GetPatient(patientId), Times.Once);
        }

        [Fact]
        public async Task GetPatient_WithInvalidId_ReturnsOkResultWithNull()
        {
            // Arrange
            int patientId = 999;
            _mockPatientService.Setup(s => s.GetPatient(patientId)).ReturnsAsync((Patient)null);

            // Act
            var result = await _controller.GetPatient(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Null(okResult.Value);
            _mockPatientService.Verify(s => s.GetPatient(patientId), Times.Once);
        }

        #endregion

        #region CreatePatient Tests

        [Fact]
        public async Task CreatePatient_WithValidPatient_ReturnsOkResultWithNewPatient()
        {
            // Arrange
            var newPatient = new Patient 
            { 
                FirstName = "Jane", 
                LastName = "Smith", 
                Age = 25, 
                Gender = "Female" 
            };
            var createdPatient = new Patient 
            { 
                Id = 4, 
                FirstName = "Jane", 
                LastName = "Smith", 
                Age = 25, 
                Gender = "Female" 
            };
            _mockPatientService.Setup(s => s.CreatePatient(newPatient)).ReturnsAsync(createdPatient);

            // Act
            var result = await _controller.CreatePatient(newPatient);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPatient = Assert.IsType<Patient>(okResult.Value);
            Assert.Equal(4, returnedPatient.Id);
            Assert.Equal("Jane", returnedPatient.FirstName);
            _mockPatientService.Verify(s => s.CreatePatient(newPatient), Times.Once);
        }

        [Fact]
        public async Task CreatePatient_WithNullPatient_ThrowsException()
        {
            // Arrange
            _mockPatientService.Setup(s => s.CreatePatient(null))
                .ThrowsAsync(new ArgumentNullException(nameof(Patient)));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.CreatePatient(null));
        }

        #endregion

        #region UpdatePatient Tests

        [Fact]
        public async Task UpdatePatient_WithValidPatient_ReturnsOkResultWithUpdatedPatient()
        {
            // Arrange
            int patientId = 1;
            var updatePatient = new Patient 
            { 
                FirstName = "John", 
                LastName = "Updated", 
                Age = 31, 
                Gender = "Male" 
            };
            var updatedPatient = new Patient 
            { 
                Id = 1, 
                FirstName = "John", 
                LastName = "Updated", 
                Age = 31, 
                Gender = "Male" 
            };
            _mockPatientService.Setup(s => s.UpdatePatient(patientId, updatePatient))
                .ReturnsAsync(updatedPatient);

            // Act
            var result = await _controller.UpdatePatient(patientId, updatePatient);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPatient = Assert.IsType<Patient>(okResult.Value);
            Assert.Equal("Updated", returnedPatient.LastName);
            Assert.Equal(31, returnedPatient.Age);
            _mockPatientService.Verify(s => s.UpdatePatient(patientId, updatePatient), Times.Once);
        }

        [Fact]
        public async Task UpdatePatient_WithNonExistentId_ReturnsOkResultWithNull()
        {
            // Arrange
            int patientId = 999;
            var updatePatient = new Patient 
            { 
                FirstName = "Test", 
                LastName = "Patient" ,
                Gender = "Male"
            };
            _mockPatientService.Setup(s => s.UpdatePatient(patientId, updatePatient))
                .ReturnsAsync((Patient)null);

            // Act
            var result = await _controller.UpdatePatient(patientId, updatePatient);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Null(okResult.Value);
            _mockPatientService.Verify(s => s.UpdatePatient(patientId, updatePatient), Times.Once);
        }

        #endregion

        #region DeletePatient Tests

        [Fact]
        public async Task DeletePatient_WithValidId_ReturnsOkResultTrue()
        {
            // Arrange
            int patientId = 1;
            _mockPatientService.Setup(s => s.DeletePatient(patientId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeletePatient(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
            _mockPatientService.Verify(s => s.DeletePatient(patientId), Times.Once);
        }

        [Fact]
        public async Task DeletePatient_WithNonExistentId_ReturnsOkResultFalse()
        {
            // Arrange
            int patientId = 999;
            _mockPatientService.Setup(s => s.DeletePatient(patientId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeletePatient(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.False((bool)okResult.Value);
            _mockPatientService.Verify(s => s.DeletePatient(patientId), Times.Once);
        }

        #endregion
    }
}