using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SiteDevelopment.Models;

namespace SiteDevelopment.Repository
{
    public static class ServiceFunctions
    {
        public static List<string> GetResultField()
        {
            var list = new List<TypeOfResult>
            {
                TypeOfResult.FT,
                TypeOfResult.OT,
                TypeOfResult.SO,
            };

            var result = new List<string>();

            foreach (var item in list)
            {
                switch (item)
                {
                    case TypeOfResult.FT:
                        result.Add("FT");
                        break;
                    case TypeOfResult.OT:
                        result.Add("OT");
                        break;
                    case TypeOfResult.SO:
                        result.Add("SO");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return result;
        }
    }
}