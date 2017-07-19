using System;
using System.Collections.Generic;

namespace XamarinManageErrors.Services.StarWars.DTO
{
    public class Person
    {
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; }
        public string HairColor { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public string BirthYear { get; set; }
        public string Gender { get; set; }
        public string Homeworld { get; set; }
        public IList<string> Films { get; set; }
        public IList<string> Species { get; set; }
        public IList<string> Vehicles { get; set; }
        public IList<string> Starships { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public string Url { get; set; }
    }
}