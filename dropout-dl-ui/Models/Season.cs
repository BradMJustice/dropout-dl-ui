namespace dropout_dl_ui.Models
{
    public record Season
    (
        int Number,
        string Name,
        string Url,
        List<Episode> Episodes
    );
}
