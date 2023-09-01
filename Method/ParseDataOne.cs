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
                    pid = reader["pid"].ToString(),
                    ten = reader["ten"].ToString(),
                    gia = Convert.ToInt32(reader["gia"]),
                    anh = reader["anh"].ToString(),
                    hienthi = Convert.ToBoolean(reader["hienthi"]),
                    hot = Convert.ToBoolean(reader["hot"]),
                    khadung = Convert.ToBoolean(reader["khadung"]),
                    mota = reader["mota"].ToString(),
                    //nsx = reader["nsx"].ToString(),
                    //tag = reader["tag"].ToString(),
                };

                list.Add(item);
            }
            reader.Close();

            return list;

        }

        public List<Comment> ListComment(SqlDataReader reader)
        {
            List<Comment> list = new List<Comment>();
            while (reader.Read())
            {
                var item = new Comment()
                {
                    id = reader["pid"].ToString(),
                    binhLuan = reader["binhLuan"].ToString(),
                    sao = Convert.ToInt32(reader["sao"]),
                    ten = reader["ten"].ToString(),
                    //thoiGian = Convert.ToInt32(reader["Time"]),
                    msp = reader["msp"].ToString(),
                };

                list.Add(item);
            }
            reader.Close();

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
            reader.Close();

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
                    msp = reader["msp"].ToString(),
                };

                listImage.Add(item);
            }
            reader.Close();

            return listImage;

        }

        public List<UserCard> ParseUserCard(SqlDataReader reader)
        {
            var listUserCard = new List<UserCard>();
            while (reader.Read())
            {
                var item = new UserCard()
                {
                    id = reader["id"].ToString(),
                    timeStamp = Convert.ToInt32(reader["timeStamp"]),

                };

                listUserCard.Add(item);
            }
            reader.Close();

            return listUserCard;
        }

        public List<OrderCard> ParseOrderCard(SqlDataReader reader)
        {
            var listOrderCard = new List<OrderCard>();
            while (reader.Read())
            {
                var item = new OrderCard()
                {
                    userId = reader["userId"].ToString(),
                    number = Convert.ToInt32(reader["number"]),
                    propductId = Convert.ToInt32(reader["propductId"]),

                };

                listOrderCard.Add(item);
            }
            reader.Close();

            return listOrderCard;
        }

    }
}
