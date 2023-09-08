using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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


    public class OrderLsAdd
    {
        [Required(ErrorMessage = "Number is empty")]
        public int number { get; set; }
        [Required(ErrorMessage = "MSP is empty")]
        public string msp { get; set; }
    }

    public class AddOrder
    {
        [Required(ErrorMessage = "Name is empty")]
        [MinLength(1, ErrorMessage = "MinLength is invalid")]
        [MaxLength(50, ErrorMessage = "MaxLength is invalid")]
        public string name { get; set; }
        [Required(ErrorMessage = "Phonenumber is empty")]
        [Phone(ErrorMessage = "Phonenumber is invalid")]
        [MaxLength(20, ErrorMessage = "MaxLength is invalid")]
        public string phoneNumber { get; set; }
        [Required(ErrorMessage = "Address is empty")]
        [MinLength(1, ErrorMessage = "MinLength is invalid")]
        public string address { get; set; }
        [Required(ErrorMessage = "Order is empry")]
        public string order { get; set; }
        public List<OrderLsAdd>? orderls { get; set; }

        [Required(ErrorMessage = "Human check is invalid")]
        public string human { get; set; }
    }

    public class HumanVerify
    {
        public bool success { get; set; }
    }

    public class HumanVerifyForm
    {
        public string response { get; set; }
        public string secret { get; set; }
    }


    public class OrderDelete
    {
        public string msp { get; set; }
        public string sessionId { get; set; }
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

    public class VTOSTATUS
    {
        public int code { get; set; }
        public string note { get; set; }
    }

    public class VTO_CHECKED
    {
        public bool isGood { get; set; }

        public List<VTOSTATUS> ls { get; set; }
    }

    public class IResponse
    {
        public string? response { get; set; }
        public VTO_CHECKED? check { get; set; }
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

    public class OrderLs
    {
        public SessionOrder sOrder { get; set; }
        public SanPham sSanPham { get; set; }
    }


    public class PieObject
    {
        public string sessionId { get; set; }
        public List<SessionOrder> listOrder { get; set; }
    }

    public class OrderListGetter
    {
        public string timestamp { get; set; }
        public PieObject pieObject { get; set; }
    }

    public class DanhMuc
    {
        public string? Id { get; set; }
        public bool? isHot { get; set; }
        public bool? isShow { get; set; }
        public string? name { get; set; }
        public int? numberItem { get; set; }
        public int? orderIndexOnHomePage { get; set; }
        public bool? showOnHomePage { get; set; }
        public string? url { get; set; }
    }

    public class BlockCateOnHomePage
    {
        public DanhMuc danhMuc { get; set; }
        public List<SanPham> sanPham { get; set; }
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
        public string? sessionId { get; set; }
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
