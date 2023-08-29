namespace BAN_BANH.Model
{
    public class Component
    {
        
    }

    public class SameProduct
    {
        public SanPham sanpham { get; set; }
        public IConfiguration config { get; set; }
    }

    public class UserCard
    {
        public string id { get; set; }
        public int timeStamp { get; set; }
    }

    public class OrderCard
    {
        public string userId { get; set; }
        public int propductId { get; set; }
        public int number { get; set; }
    }
}
