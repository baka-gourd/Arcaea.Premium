namespace Arcaea.Premium.Models.StModels;

public partial class Purchaseentry
{
    public long Id { get; set; }

    public string? Checksum { get; set; }

    public string? Sku { get; set; }

    public string? Receipt { get; set; }

    public string? ReceiptCipheredPayload { get; set; }
}
