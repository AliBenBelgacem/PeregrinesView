﻿using Peregrine.Library;
using StaffManager.Model;
using StaffManager.ViewModel.ServiceContracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManager.ServiceImplementations
{
    public class StaffManagerDataService: IStaffManagerDataService
    {
        public Task InitialiseAsync()
        {
            // In a real application, the call to the data-store would go here.
            _peopleCache.AddRange(new[]
                    {
                        new Person { Id = 1, DepartmentId = 1, FirstName = "Alan", LastName = "Jones", IsManager = false },
                        new Person { Id = 2, DepartmentId = 1, FirstName = "Joseph", LastName = "Preston", IsManager = true },
                        new Person { Id = 3, DepartmentId = 1, FirstName = "Stella", LastName = "Mcbride", IsManager = false },
                        new Person { Id = 4, DepartmentId = 1, FirstName = "Branden", LastName = "Owens", IsManager = false },
                        new Person { Id = 5, DepartmentId = 1, FirstName = "Leonard", LastName = "Marquez", IsManager = false },
                        new Person { Id = 6, DepartmentId = 1, FirstName = "Colin", LastName = "Brady", IsManager = false },
                        new Person { Id = 7, DepartmentId = 2, FirstName = "Callum",  LastName = "Roberts", IsManager = true },
                        new Person { Id = 8, DepartmentId = 2, FirstName = "Jillian", LastName = "Scott", IsManager = false },
                        new Person { Id = 9, DepartmentId = 2, FirstName = "Calvin", LastName = "Moran", IsManager = false },
                        new Person { Id = 10, DepartmentId = 3, FirstName = "Harlan", LastName = "Reid", IsManager = false },
                        new Person { Id = 11, DepartmentId = 3, FirstName = "Felix", LastName = "Schroeder", IsManager = false },
                        new Person { Id = 12, DepartmentId = 3, FirstName = "Joseph", LastName = "Smith", IsManager = true },
                        new Person { Id = 13, DepartmentId = 3, FirstName = "Jasmine", LastName = "Emerson", IsManager = false },
                        new Person { Id = 14, DepartmentId = 3, FirstName = "Lucas", LastName = "Edwards", IsManager = false },
                        new Person { Id = 15, DepartmentId = 3, FirstName = "David", LastName = "Baxter", IsManager = false },
                        new Person { Id = 16, DepartmentId = 4, FirstName = "Kane", LastName = "Foreman", IsManager = false },
                        new Person { Id = 17, DepartmentId = 4, FirstName = "Laurel", LastName = "Curtis", IsManager = false },
                        new Person { Id = 18, DepartmentId = 4, FirstName = "Lucy", LastName = "Tanner", IsManager = true },
                        new Person { Id = 19, DepartmentId = 4, FirstName = "Christian", LastName = "Pittman", IsManager = false },
                        new Person { Id = 20, DepartmentId = 4, FirstName = "Patricia", LastName = "Wilkinson", IsManager = false }
                    });

            _departmentsCache.AddRange(new[]
                    {
                        new Department { Id = 1, Description = "I.T." },
                        new Department { Id = 2, Description = "Accounts" },
                        new Department { Id = 3, Description = "Sales" },
                        new Department { Id = 4, Description = "Logistics" }
                    });

            return Task.CompletedTask;
        }

        // this ensures that there is only once instance for each person entity
        private readonly List<Person> _peopleCache = new List<Person>();

        // this ensures that there is only one instance for each department entity
        private readonly List<Department> _departmentsCache = new List<Department>();

        public ReadOnlyCollection<Person> GetAllPeople()
        {
            var allPeople = _peopleCache.ToList();
            allPeople.Sort();
            return allPeople.AsReadOnly();
        }

        public ReadOnlyCollection<Person> GetAllPeopleMatchingSearch(string searchText)
        {
            // For simplicity in this proof of concept application just do the search in the in-memory list of items.
            // In a larger data-set, this would be done as a database search.
            var peopleMatchingSearch = _peopleCache
                .Where(p => ($" {p.FirstName} | {p.LastName} ").CaseInsensitiveContains(searchText))
                .ToList();
            peopleMatchingSearch.Sort();
            return peopleMatchingSearch.AsReadOnly();
        }

        public ReadOnlyCollection<Person> GetPeopleForDepartment(int departmentId)
        {
            var peopleForDepartment = _peopleCache
                .Where(p => p.DepartmentId == departmentId)
                .ToList();
            peopleForDepartment.Sort();
            return peopleForDepartment.AsReadOnly();
        }

        public ReadOnlyCollection<Department> GetAllDepartments()
        {
            var departments = _departmentsCache.ToList();
            departments.Sort();
            return departments.AsReadOnly();
        }

        // In a real application, this would update the data-store
        public void SavePerson(Person person)
        {
            if (person.Id == 0)
            {
                person.Id = _peopleCache.Count + 1;
                _peopleCache.Add(person);
            }
        }

        // In a real application, this would update the data-store
        public void SaveDepartment(Department department)
        {
            if (department.Id == 0)
            {
                department.Id = _departmentsCache.Count + 1;
                _departmentsCache.Add(department);
            }
        }
    }
}
