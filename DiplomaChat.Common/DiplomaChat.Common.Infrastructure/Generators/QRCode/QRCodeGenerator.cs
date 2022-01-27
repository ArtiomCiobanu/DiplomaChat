

namespace DiplomaChat.Common.Infrastructure.Generators.QRCode
{
    public class QRCodeGenerator : IQRCodeGenerator
    {
        public byte[] GenerateQRCode(string payload,
            int pixelsPerModule = 10,
            QRCoder.QRCodeGenerator.ECCLevel eccLevel = QRCoder.QRCodeGenerator.ECCLevel.Q)
        {
            return null;
            /*var qrCodeGenerator = new QRCoder.QRCodeGenerator();
            var qrCodeData = qrCodeGenerator.CreateQrCode(payload, eccLevel);
            var qrCode = new QRCoder.QRCode(qrCodeData);
            var qrCodeBitmap = qrCode.GetGraphic(pixelsPerModule);

            var byteArray = (byte[])new ImageConverter().ConvertTo(qrCodeBitmap, typeof(byte[]));

            return byteArray;*/
        }
    }
}
