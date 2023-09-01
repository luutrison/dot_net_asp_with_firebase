using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace BAN_BANH.Model
{
    public class SanPham
    {
        public string? pid { get; set; }


        public string? ten { get; set; }
        public int? gia { get; set; }
        public bool? khadung { get; set; }
        public List<String>? cm { get; set; }
        public string? anh { get; set; }
        public bool? hienthi { get; set; }
        public string? mota { get; set; }
        public int ngaySanXuat { get; set; }
        public int ngayNhapLieu { get; set; }
        public int hanSuDung { get; set; }
        public bool? hot { get; set; }

        public string? sukien { get; set; }
        public string? msp { get; set; }

    }

    public class SameProduct
    {
        public List<string> cm { get; set; }
        public string msp { get; set; }
    }

    public class Comment
    {
        public string? id { get; set; }
        public string? msp { get; set; }
        public string? binhLuan { get; set; }
        public string? ten { get; set; }
        public int? sao { get; set; }
        public string? thoiGian { get; set; }
    }



    public class SoSao
    {



        public float Tong
        {
            get;
            set;
        }
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

    public class ImageGalleryProduct
    {
        public string id { get; set; }
        public string imageUrl { get; set; }
        public string msp { get; set; }
    }


    public class SessionCard
    {
        public int addCardTimestamp { get; set; }
        public string sessionId { get; set; }
    }

    public class SessionOrder
    {
        public string msp { get; set; }
        public int number { get; set; }
        public string sessionId { get; set; }
    }

    public class NewOrder
    {
        public SessionCard sessionCard { get; set; }
        public SessionOrder sessionOrder { get; set; }
    }



    public class ProductOrder
    {
        [Required(ErrorMessage = "null")]
        public int numberOrder { get; set; }
        [Required(ErrorMessage = "null")]
        public string msp { get; set; }
    }

    public class DB_DOCUMENT
    {
        private readonly FirestoreDb _db;
        public DB_DOCUMENT(FirestoreDb db)
        {
            _db = db;
        }

        public DocumentReference DASH()
        {
            return _db.Collection(FIREBASE_DB_COLLECTION.BANBANH).Document(FIREBASE_DB_DOCUMENT.DASH);
        }

        public DocumentReference CLIENT()
        {
            return _db.Collection(FIREBASE_DB_COLLECTION.BANBANH).Document(FIREBASE_DB_DOCUMENT.CLIENT);
        }

        public DocumentReference BB_ORDER()
        {
            return _db.Collection(FIREBASE_DB_COLLECTION.BB_ORDER_NC).Document(FIREBASE_DB_DOCUMENT.BB_ORDER);
        }

        public DocumentReference BB_SESSION_ORDER()
        {
            return _db.Collection(FIREBASE_DB_COLLECTION.BB_ORDER_NC).Document(FIREBASE_DB_DOCUMENT.BB_SESSION);
        }
    }

}
