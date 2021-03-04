using System;
using System.Collections.Generic;
using System.Text;

namespace ClaimAPI.Repository.Model
{
    public class MemberClaimModel
    {
        public Member Member { get; set; }
        public List<Claim> ClaimDetails { get; set; }

    }

    public class Member
    {
        public int MemberID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Claim
    {
        public int MemberID { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal ClaimAmount { get; set; }
    }
}
