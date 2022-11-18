using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CodeLouisvilleUnitTestProject
{
    public  class Car : Vehicle
    {
        public int NumberOfPassengers { get; private set; }
        private HttpClient _client = new HttpClient()

        {
            BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/")
        };

       

        public Car()
            : this(0, "", "", 0)
        {
             NumberOfTires = 4;
    }

        public Car(double gasTankCapacity, string make, string model, double milesPerGallon)
        {
            //GasTankCapacity = gasTankCapacity;
           // Make = make;
            //Model = model;
           // MilesPerGallon = milesPerGallon;
        }

        public async Task<bool > IsValidModelForMakeAsync()
        {
            var model = this.Model;
            var make = this.Make;
            string urlsiffix = $"vehicles/getmodelsformake/{Make}?Format=Json";
            var response = await _client.GetAsync(urlsiffix);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<GetModelIsForMakeResponseModel>(jsonContent);
            return data.Results.Any(r => r.Model_Name == model);
        }
        public async Task<bool> WasModelMadeInYearAsync(int year)
        {
            var model = this.Model;
            if (year < 1995) throw new ArgumentException("No data is available for years before 1995");
            string utlsuffix = $"vehicle/getModelsformakeyear/make/{Make}/modelyear/{year}?format=json";
            var response = await _client.GetAsync(utlsuffix);
            var jsonContent = await response.Content.ReadAsStringAsync();   
            var data = JsonSerializer.Deserialize<GetModelIsForMakeYearResponseModel>(jsonContent);
            return data.Results.Any(r => r.Model_Name == Model);
        }

        public void AddPassengers(int addingPassengers)
        {
            NumberOfPassengers = NumberOfPassengers + addingPassengers;
            MilesPerGallon = MilesPerGallon - (addingPassengers * .2);
            if (MilesPerGallon < 0)
            {
                MilesPerGallon = 0;
            }
        }

        public void RemovePassengers(int removingPassengers)
        {
            
        }
    }
}
