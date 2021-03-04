using ClaimAPI.Repository.Model;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//using System.Text;

namespace ClaimAPI.Repository
{
    public class SampleData
    {
        public List<Member> GetMembers(string path)
        {

           return ReadInCSVForMembers(path).ToList();
        }

        public List<Claim> GetClaims(string path)
        {
            return ReadInCSVForClaims(path).ToList();
        }

        public static List<Member> ReadInCSVForMembers(string absolutePath)
        {
            List<Member> members = new List<Member>();
            using (var reader = new StreamReader(absolutePath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CreateSpecificCulture("en")))
            {
                members = csv.GetRecords<Member>().ToList();
            }
            return members;
        }
        public static List<Claim> ReadInCSVForClaims(string absolutePath)
        {
            List<Claim> claims = new List<Claim>();
            
            using (var reader = new StreamReader(absolutePath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CreateSpecificCulture("en")))
            {
                claims = csv.GetRecords<Claim>().ToList();
            }
            return claims;
        }
    }
}
