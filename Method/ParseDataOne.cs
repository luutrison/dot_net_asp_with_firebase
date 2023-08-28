using BAN_BANH.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BAN_BANH.Method
{
    public class ParseDataOne
    {
        public List<SanPham> ListSanPham(SqlDataReader reader) {
            List<SanPham> list = new List<SanPham>();
            while (reader.Read())
            {
                var item = new SanPham()
                {
                    id = Convert.ToInt32(reader["id"]),
                    ten = reader["ten"].ToString(),
                    gia = Convert.ToInt32(reader["gia"]),
                    anh = reader["anh"].ToString(),
                    hienthi = Convert.ToBoolean(reader["hienthi"]),
                    hot = Convert.ToBoolean(reader["hot"]),
                    khadung = Convert.ToBoolean(reader["khadung"]),
                    mota = reader["mota"].ToString(),
                    ngaytao = Convert.ToDateTime(reader["ngaytao"]),
                    nsx = reader["nsx"].ToString(),
                    tag = reader["tag"].ToString(),
                };

                list.Add(item);
            }

            return list;

        }

        public List<Comment> ListComment(SqlDataReader reader)
        {
            List<Comment> list = new List<Comment>();
            while (reader.Read())
            {
                var item = new Comment()
                {
                    id = reader["id"].ToString(),
                    BinhLuan = reader["BinhLuan"].ToString(),
                    Rate = Convert.ToInt32(reader["Rate"]),
                    SanPhamID = Convert.ToInt32(reader["SanPhamID"]),
                    Ten = reader["Ten"].ToString(),
                    Time = Convert.ToInt32(reader["Time"]),
                };

                list.Add(item);
            }

            return list;

        }

        public SoSao DemSao(SqlDataReader reader) {
            var obj = new SoSao();
            while (reader.Read())
            {
                var item = new SoSao()
                {
                    Tong = Convert.ToInt32(reader["Tong"]),
                    Sao1 = Convert.ToInt32(reader["Sao1"]),
                    Sao2 = Convert.ToInt32(reader["Sao2"]),
                    Sao3 = Convert.ToInt32(reader["Sao3"]),
                    Sao4 = Convert.ToInt32(reader["Sao4"]),
                    Sao5 = Convert.ToInt32(reader["Sao5"]),

                };

                obj = item;
            }
            return obj;
        }
        public List<ImageGalleryProduct> ParseImageProduct(SqlDataReader reader) {
            var listImage = new List<ImageGalleryProduct>();
            while (reader.Read())
            {
                var item = new ImageGalleryProduct()
                {
                    id = reader["id"].ToString(),
                    imageUrl = reader["imageUrl"].ToString(),
                    productID = Convert.ToInt32(reader["productID"]),
                    time = Convert.ToInt32(reader["time"]),


                };

                listImage.Add(item);
            }
            return listImage;

        }
    
    }
}
