namespace Mango.Web.Utility
{
    public class StaticDetails
    {
        public  static string CouponAPIBase { get; set; }   // base url for couponAPI => populated in program.cs, when services are being configured
        public  static string ProductAPIBase { get; set; }   // base url for couponAPI => populated in program.cs, when services are being configured
        public static string AuthAPIBase { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string TokenCookie = "JWTToken";
        public enum ApiType{
            GET, POST, PUT, DELETE
        }
    }
}
