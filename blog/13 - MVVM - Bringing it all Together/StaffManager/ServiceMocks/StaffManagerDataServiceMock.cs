﻿using StaffManager.Model;
using StaffManager.ServiceImplementations;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using StaffManager.ViewModel.ServiceContracts;

namespace StaffManager.ServiceMocks
{
    using System.Collections.Generic;

    using Peregrine.WPF.Model;

    /// <summary>
    /// Mock implementation of the IStaffManagerDataService for use in design-mode.
    /// </summary>
    /// <remarks>
    /// In a real application this would often return some hard coded data - eg static json or xml.
    /// To simplify this proof of concept application, just use the "Live" implementation of the service as a data source.
    /// </remarks> 
    public class StaffManagerDataServiceMock : IStaffManagerDataService
    {
        private IStaffManagerDataService _dataService;

        public Task InitialiseAsync()
        {
            _dataService = new StaffManagerDataService();
            return _dataService.InitialiseAsync();
        }

        public ReadOnlyCollection<Person> GetAllPeople()
        {
            var allPeople = _dataService.GetAllPeople();
            var result = new List<Person>();

            foreach (var person in allPeople)
            {
                var mockPerson = person.CloneAllFields();
                mockPerson.LastName += " (MOCK)";
                result.Add(mockPerson);
            }

            return result.AsReadOnly();
        }

        public ReadOnlyCollection<Person> GetAllPeopleMatchingSearch(string searchText)
        {
            var allPeopleMatchingSearch = _dataService.GetAllPeopleMatchingSearch(searchText);
            var result = new List<Person>();

            foreach (var person in allPeopleMatchingSearch)
            {
                var mockPerson = person.CloneAllFields();
                mockPerson.LastName += " (MOCK)";
                result.Add(mockPerson);
            }

            return result.AsReadOnly();
        }

        public ReadOnlyCollection<Person> GetPeopleForDepartment(int departmentId)
        {
            var allPeopleForDepartment = _dataService.GetPeopleForDepartment(departmentId);
            var result = new List<Person>();

            foreach (var person in allPeopleForDepartment)
            {
                var mockPerson = person.CloneAllFields();
                mockPerson.LastName += " (MOCK)";
                result.Add(mockPerson);
            }

            return result.AsReadOnly();
        }

        public ReadOnlyCollection<Department> GetAllDepartments()
        {
            var allDepartments = _dataService.GetAllDepartments();

            var result = new List<Department>();

            foreach (var department in allDepartments)
            {
                var mockDepartment = department.CloneAllFields();
                mockDepartment.Description += " (MOCK)";
                result.Add(mockDepartment);
            }

            return result.AsReadOnly();
        }

        public void SavePerson(Person person)
        {
            _dataService.SavePerson(person);
        }

        public void SaveDepartment(Department department)
        {
            _dataService.SaveDepartment(department);
        }
    }
}
