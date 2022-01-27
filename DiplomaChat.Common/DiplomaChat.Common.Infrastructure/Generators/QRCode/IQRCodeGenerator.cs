
namespace DiplomaChat.Common.Infrastructure.Generators.QRCode
{
    public interface IQRCodeGenerator
    {
        byte[] GenerateQRCode(string payload,
            int pixelsPerModule = 10,
            QRCoder.QRCodeGenerator.ECCLevel eccLevel = QRCoder.QRCodeGenerator.ECCLevel.Q);
    }
}
