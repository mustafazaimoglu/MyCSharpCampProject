using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi!";
        public static string ProductNameInvalid = "Ürün ismi geçersiz.";
        public static string UnitPriceInvalid = "Ürün fiyatı geçersiz.";
        public static string MaintenanceTime = "Sistem şuan bakımda.";
        public static string ProductListed = "Ürünler listelendi!";
        public static string ProductCountOfCategoryError = "Kategori limitini aştınız!";
        public static string ProductNameAlreadyExists = "Urun ismi zaten var";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor.";
        public static string AuthorizationDenied = "Yetkilendirme reddedildi!";
        public static string UserRegistered = "Kullanıcı Kaydedildi.";
        public static string UserNotFound = "Kullanıcı Bulunamadı.";
        public static string PasswordError = "Şifre Hatalıç";
        public static string SuccessfulLogin = "Giriş Başarılı.";
        public static string AccessTokenCreated = "Token Oluşturuldu.";
        public static string UserAlreadyExists = "Kullanıcı Zaten Var.";
    }
}
