using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Enums
{
    public static class Genders
    {
        public const string Male = "Male";
        public const string Female = "Female";

        public static bool IsGenderValid(string gender)
        {
            if (Male == gender) return true;
            if (Female == gender) return true;
            return false;
        }
    }
}
