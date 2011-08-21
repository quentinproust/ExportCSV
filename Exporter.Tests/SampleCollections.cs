using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exporter.Tests.SampleModel;

namespace Exporter.Tests
{
    public static class SampleCollections
    {

        public static IEnumerable<Student> Students
        {
            get
            {
                yield return new Student()
                {
                    Id = Guid.NewGuid(), 
                    OfficialId = "108TX338CS2",
                    FirstName = "Jean",
                    Surname = "Dupond",
                    Birthday = 23.YearsFromNow()
                };
                yield return new Student()
                {
                    Id = Guid.NewGuid(),
                    OfficialId = "109ZA647CS2",
                    FirstName = "Pierre",
                    Surname = "Dupont",
                    Birthday = 21.YearsFromNow()
                };
                yield return new Student()
                {
                    Id = Guid.NewGuid(),
                    OfficialId = "300KK617BF1",
                    FirstName = "Michel",
                    Surname = "Petit",
                    Birthday = 22.YearsFromNow()
                };
            }
        }

        public static IEnumerable<Student> StudentsWithComma
        {
            get
            {
                yield return new Student()
                {
                    Id = Guid.NewGuid(),
                    OfficialId = "300;KK;617BF1",
                    FirstName = "Mich;el",
                    Surname = "Peti;t",
                    Birthday = 22.YearsFromNow()
                };
            }
        }

        public static IEnumerable<dynamic> StudentsAsDynamic
        {
            get
            {
                yield return new {
                    FullName = "Jack Dynamic",
                    Birthday = 25.YearsFromNow()
                                 };
                yield return new
                {
                    FullName = "Test Dynamic2",
                    Birthday = 28.YearsFromNow()
                };
            }
        }

        private static DateTime YearsFromNow(this int howManyYears)
        {
            var timeSpan = TimeSpan.FromDays(howManyYears*365);
            return DateTime.Now.Subtract(timeSpan);
        }
    }
}
