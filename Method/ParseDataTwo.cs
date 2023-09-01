using BAN_BANH.Model;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace BAN_BANH.Method
{

    public class ParseTwoMethod
    {
        public string? ParseDateTime(string date)
        {
            try
            {
                var dateString = date.Replace(VARIBLE.TIMESTAMPSPLIT, "");
                var dateOut = DateTime.Parse(dateString).ToUniversalTime().ToString(Environment.GetEnvironmentVariable(VARIBLE.DATE_TIME_FORMAT));
                return dateOut;
            }
            catch (Exception err)
            {
                new MethodOne().LogsError(err.ToString());
                throw;
                return null;
            }
        }
    }

    public class ParseDataTwo
    {

        private List<T> ToList<T>(List<object> item)
        {

            try
            {
                List<T> list = new List<T>();

                foreach (var i in item)
                {
                    list.Add(i as dynamic);
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private DateTime ToDate(string dateString)
        {
            try
            {
                string date = dateString.Split(' ')[1];

                var dt = DateTime.Parse(date).ToString(Environment.GetEnvironmentVariable(VARIBLE.DATE_TIME_FORMAT));

                return DateTime.Now;

            }
            catch (Exception)
            {

                throw;
            }

        }

        //SanPham

        private SanPham ParseSanPham(DocumentSnapshot item)
        {
            var reader = item.ToDictionary();
            var sanpham = new SanPham()
            {
                pid = item.Id,
                ten = reader["ten"].ToString(),
                gia = Convert.ToInt32(reader["gia"]),
                anh = reader["anh"].ToString(),
                hienthi = Convert.ToBoolean(reader["hienthi"]),
                hot = Convert.ToBoolean(reader["hot"]),
                khadung = Convert.ToBoolean(reader["khadung"]),
                mota = reader["mota"].ToString(),
                ngaySanXuat = Convert.ToInt32(reader["ngaySanXuat"]),
                hanSuDung = Convert.ToInt32(reader["hanSuDung"]),
                ngayNhapLieu = Convert.ToInt32(reader["ngayNhapLieu"]),
                cm = ToList<string>(reader["cm"] as dynamic),
                sukien = reader["sukien"].ToString(),
                msp = reader["msp"].ToString(),
            };

            return sanpham;
        }

      


        public async Task<List<SanPham>> ListSanPham(Task<QuerySnapshot> task)
        {
            var ls = await task;

            List<SanPham> list = new List<SanPham>();


            foreach (var item in ls.Documents)
            {
                var itemOut = ParseSanPham(item);
                list.Add(itemOut);
            }

            return list;

        }


        public async Task<SanPham> SanPham(Task<DocumentSnapshot> task)
        {
            return ParseSanPham(await task);
        }
        // Comment
        private Comment ParseComment(DocumentSnapshot item)
        {
            var reader = item.ToDictionary();
            var comment = new Comment()
            {
                id = item.Id.ToString(),
                binhLuan = reader["binhLuan"].ToString(),
                sao = Convert.ToInt32(reader["sao"]),
                ten = reader["ten"].ToString(),
                thoiGian = new ParseTwoMethod().ParseDateTime(item.CreateTime.ToString()),
                msp = reader["msp"].ToString(),
            };
            return comment;
        }

        public async Task<List<Comment>> ListComment(Task<QuerySnapshot> task)
        {
            var ls = await task;

            List<Comment> list = new List<Comment>();


            foreach (var item in ls.Documents)
            {
                var itemOut = ParseComment(item);
                list.Add(itemOut);
            }

            return list;

        }

        public async Task<Comment> Comment(Task<DocumentSnapshot> task)
        {
            return ParseComment(await task);
        }

        //Image

        private ImageGalleryProduct ParseImageGallery(DocumentSnapshot item)
        {
            var reader = item.ToDictionary();
            var outItem = new ImageGalleryProduct()
            {
                id = item.Id.ToString(),
                imageUrl = reader["imageUrl"].ToString(),
                msp = reader["msp"].ToString(),
                

            };
            return outItem;
        }
        public async Task<List<ImageGalleryProduct>> ListImageGallery(Task<QuerySnapshot> task)
        {
            var ls = await task;

            List<ImageGalleryProduct> list = new List<ImageGalleryProduct>();


            foreach (var item in ls.Documents)
            {
                var itemOut = ParseImageGallery(item);
                list.Add(itemOut);
            }

            return list;

        }

        public async Task<ImageGalleryProduct> ImageGallery(Task<DocumentSnapshot> task)
        {
            return ParseImageGallery(await task);
        }


        public async Task<SanPhamLink> ListSanPhamLink(Task<DocumentSnapshot> task, int time) {

            var sanphamlink = await task;

            var reader = sanphamlink.ToDictionary();
            var sanpham = new SanPhamLink()
            {
                suKien = reader["tag"].ToString(),
                timeStamp = time
            };

            return sanpham;
        }
    }
}
