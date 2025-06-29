# Halı Saha Randevu Sistemi

Bu proje, halı saha randevularının yönetimi için geliştirilmiş bir web API uygulamasıdır. .NET 8.0 kullanılarak geliştirilmiştir.

## 🚀 Özellikler

- Kullanıcı kimlik doğrulama ve yetkilendirme
- Halı saha randevu oluşturma ve yönetme
- E-posta bildirimleri
- JWT tabanlı güvenlik
- RESTful API mimarisi

## 🛠️ Teknolojiler

- .NET 8.0
- BCrypt.Net-Next (Şifreleme)
- MailKit (E-posta işlemleri)
- JWT Bearer Authentication

## 📋 Gereksinimler

- .NET 8.0 SDK
- SMTP sunucu bilgileri (e-posta gönderimi için)

## 🔧 Kurulum

1. Projeyi klonlayın:
```bash
git clone https://github.com/kullaniciadi/hali-saha-randevu.git
```

2. Proje dizinine gidin:
```bash
cd hali-saha-randevu
```

3. `appsettings.json` dosyasını düzenleyin:
   - SMTP ayarlarını yapılandırın
   - JWT secret key'i ayarlayın

4. Uygulamayı çalıştırın:
```bash
dotnet run
```

## 📝 API Dokümantasyonu

API endpoint'leri ve kullanımları hakkında detaylı bilgi için [API Dokümantasyonu](docs/api.md) sayfasını inceleyebilirsiniz.

## 🤝 Katkıda Bulunma

1. Bu depoyu fork edin
2. Yeni bir branch oluşturun (`git checkout -b feature/yeniOzellik`)
3. Değişikliklerinizi commit edin (`git commit -am 'Yeni özellik: Açıklama'`)
4. Branch'inizi push edin (`git push origin feature/yeniOzellik`)
5. Pull Request oluşturun

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Daha fazla bilgi için `LICENSE` dosyasına bakın.

## 📞 İletişim

Proje Sahibi - [@github_username](https://github.com/github_username)

Proje Linki: [https://github.com/kullaniciadi/hali-saha-randevu](https://github.com/kullaniciadi/hali-saha-randevu) 