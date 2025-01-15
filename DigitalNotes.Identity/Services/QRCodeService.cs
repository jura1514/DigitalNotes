using QRCoder;

namespace DigitalNotes.Identity.Services;

public class QrCodeService
{
    private readonly QRCodeGenerator _generator;

    public QrCodeService(QRCodeGenerator generator)
    {
        _generator = generator;
    }

    public string GetQrCodeAsBase64(string textToEncode)
    {
        using var qrCodeData = _generator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrCodeData);

        return Convert.ToBase64String(qrCode.GetGraphic(4));
    }
}