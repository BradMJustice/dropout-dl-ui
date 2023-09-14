namespace dropout_dl.Common.Models
{
    public record Season
    (
        int Number,
        string Name,
        string Url,
        List<Episode> Episodes
    );
}
