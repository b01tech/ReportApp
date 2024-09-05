using ReportApp.Models.Enums;

namespace ReportApp.Models.Extensions;

public static class ReportStatusExtension
{
    public static string StatusToString(this ReportStatus status)
    {
        switch (status)
        {
            case ReportStatus.Aprovado:
                return "Aprovado";
            case ReportStatus.Reprovado:
                return "Reprovado";
            default:
                return "";
        }
    }

    public static List<string> GetAllStatus()
    {
        return Enum.GetValues(typeof(ReportStatus))
            .Cast<ReportStatus>()
            .Select(s => s.StatusToString())
            .ToList();
    }
}
