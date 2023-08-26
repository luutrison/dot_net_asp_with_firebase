namespace BAN_BANH.Model
{
    public class SanPham{
        public int id { get; set; }
        public string? ten { get; set; }
        public int? gia { get; set; }
        public bool? khadung { get; set; }
        public string? tag { get; set; }
        public string? anh { get; set; }
        public bool? hienthi { get; set; }
        public string? mota { get; set; }
        public string? nsx { get; set; }
        public bool? hot { get; set; }
        public DateTime? ngaytao { get; set; }
    }

    public class Comment
    {
        public string id { get; set; }
        public int SanPhamID { get; set; }
        public int Rate { get; set; }
        public string Ten { get; set; }
        public string BinhLuan { get; set; }
        public int Time { get; set; }
    }

    public class SoSao
    {
        public float Tong { get; set; }
        public float Sao1 { get; set; }
        public float Sao2 { get; set; }
        public float Sao3 { get; set; }
        public float Sao4 { get; set; }
        public float Sao5 { get; set; }

    }

    public class CalStar
    {
        public float Total { get; set; }
        public double AvgStar { get; set; }
    }

    public class StarCountImage
    {
        public double number { get; set; }
        public string mode { get; set; }
    }

}
