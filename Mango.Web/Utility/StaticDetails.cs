namespace Mango.Web.Utility
{
    public class StaticDetails
    {
        public  static string CouponAPIBase { get; set; }   // base url for couponAPI => populated in program.cs, when services are being confirgured
        public enum ApiType{
            GET, POST, PUT, DELETE
        }
    }
}
