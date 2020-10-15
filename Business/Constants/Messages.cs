using Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Concrete.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün başarıyla eklendi";
        public static string ProductDeleted = "Ürün başarıyla silindi";
        public static string ProductUpdated = "Ürün başarıyla güncellendi";

        public static string UserNotFound = "Kullanıcı bulunmadı";
        public static string PasswordError = "Şifre Hatalı";

        public static string SuccessfulLogin = "Sisteme giriş başarılı";

        public static string UserAlreadyExists = "Kullanıcı zaten mevcut";

        public static string SuccessResult = "Başarı ile giriş yapıldı";

        public static string UserRegistered = "Kullanıcı başarı ile kaydedildi";

        public static string AccessTokenCreated = "Access Token Başarı ile oluşturldu";

        public static string AuthorizationDenied = "Yetkiniz yok";
    }
}
