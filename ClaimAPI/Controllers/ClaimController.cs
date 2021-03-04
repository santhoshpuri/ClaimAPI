using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ClaimAPI.Repository;
using ClaimAPI.Repository.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClaimAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;

        public ClaimController(IWebHostEnvironment environment)
        {
            _hostEnvironment = environment;
        }

        [HttpGet]
        public IActionResult GetClaimsByDate(string startDate, string endDate)
        {
            // Declare Model
            List<MemberClaimModel> memberClaimModels = new List<MemberClaimModel>();
            List<Claim> filterClaims = new List<Claim>();
            // reading Member and claim data from csv files
            string claimPath = Path.Combine(_hostEnvironment.ContentRootPath, "Claim.csv");
            string memberPath = Path.Combine(_hostEnvironment.ContentRootPath, "Member.csv");
            //Getting Claims and Members list from repository
            List<Claim> claims = new SampleData().GetClaims(claimPath);
            List<Member> members = new SampleData().GetMembers(memberPath);
            // if both date params null then show all claims with member details
            if (string.IsNullOrEmpty(startDate)  && string.IsNullOrEmpty(endDate))
            {
                filterClaims = claims;
            }
            else
            {
                // Getting filtered claims depend on provided start date and end date(between dates)
                var endDateTime = Convert.ToDateTime(endDate);
                var startDateTime = Convert.ToDateTime(startDate);
                filterClaims = (from row in claims
                                where row.ClaimDate < endDateTime && row.ClaimDate > startDateTime
                                select row).ToList();
            }
            if (filterClaims.Count > 0)
            {
                var distinctMemberIds = filterClaims.Select(p => p.MemberID).Distinct().ToList();
                if (distinctMemberIds.Count > 0)
                {

                    for (var i = 0; i < distinctMemberIds.Count; i++)
                    {
                        // Adding multiple members with cliams if multiple members exists in filterClaims Object
                        memberClaimModels.Add(new MemberClaimModel()
                        {
                            Member = members.Where(p => p.MemberID == distinctMemberIds[i]).FirstOrDefault(),
                            ClaimDetails = filterClaims.Where(p => p.MemberID == distinctMemberIds[i]).ToList()
                        });
                    }
                }
            }
            return Ok(memberClaimModels);
        }
    }
}
