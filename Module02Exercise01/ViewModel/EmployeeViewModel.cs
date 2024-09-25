using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module02Exercise01.Model;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Module02Exercise01.Services;


namespace Module02Exercise01.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> Employees { get; set; }

        public string ManagerName { get; set; }
        public string NewEmployeeFirstName { get; set; }
        public string NewEmployeeLastName { get; set; }
        public string NewEmployeeFullName { get; set; }
        public string NewEmployeePosition { get; set; }
        public string NewEmployeeDepartment { get; set; }
        public string NewEmployeeMunicipality { get; set; }
        public string NewEmployeeProvince { get; set; }
        public string LocationCoordinates { get; set; }
        public ImageSource CapturedPhoto { get; set; }

        public ICommand LoadEmployeeDataCommand { get; }
        public ICommand TakePhotoCommand { get; }
        public ICommand GetLocationCommand { get; }
        public ICommand AddEmployeeCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public EmployeeViewModel()
        {
            // Sample data
            ManagerName = "John Doe";
            Employees = new ObservableCollection<Employee>
            {
                new Employee { FirstName = "Alice", LastName = "Johnson", Position = "Engineer" },
                new Employee { FirstName = "Bob", LastName = "Smith", Position = "Manager" }
            };

            // Commands
            LoadEmployeeDataCommand = new Command(LoadEmployeeData);
            TakePhotoCommand = new Command(async () => await CapturePhotoAsync());
            GetLocationCommand = new Command(async () => await GetLocationAsync());
            AddEmployeeCommand = new Command(AddEmployee);
        }


        private void LoadEmployeeData()
        {
            // Load the employee data here
            OnPropertyChanged(nameof(Employees));
        }

        private async Task CapturePhotoAsync()
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                CapturedPhoto = ImageSource.FromStream(() => stream);
                OnPropertyChanged(nameof(CapturedPhoto));
            }
        }

        private async Task GetLocationAsync()
        {
            try
            {
                var location = await Geolocation.Default.GetLocationAsync();
                if (location != null)
                {
                    LocationCoordinates = $"{location.Latitude}, {location.Longitude}";
                    await GetGeocodeAsync(location);
                    OnPropertyChanged(nameof(LocationCoordinates));
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        private async Task GetGeocodeAsync(Location location)
        {
            var placemarks = await Geocoding.Default.GetPlacemarksAsync(location);
            var placemark = placemarks?.FirstOrDefault();
            if (placemark != null)
            {
                NewEmployeeMunicipality = placemark.Locality;
                NewEmployeeProvince = placemark.AdminArea;
                OnPropertyChanged(nameof(NewEmployeeMunicipality));
                OnPropertyChanged(nameof(NewEmployeeProvince));
            }
        }

        private void AddEmployee()
        {
            NewEmployeeFullName = $"{NewEmployeeFirstName} {NewEmployeeLastName}";
            Employees.Add(new Employee
            {
                FirstName = NewEmployeeFirstName,
                LastName = NewEmployeeLastName,
                Position = NewEmployeePosition,
                Department = NewEmployeeDepartment
            });
            OnPropertyChanged(nameof(Employees));
            OnPropertyChanged(nameof(NewEmployeeFullName));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}