
using ReportApp.Models.Enums;

namespace ReportApp.Models.Extensions;

public static class ScaleClassExtension
{

    public static string ScaleClassToString(this ScaleClass scaleClass)
    {
        switch (scaleClass)
        {
            case ScaleClass.ClassI:
                return "Classe I";
            case ScaleClass.ClassII:
                return "Classe II";
            case ScaleClass.ClassIII:
                return "Classe III";
            case ScaleClass.ClassIV:
                return "Classe IV";
            case ScaleClass.NA:
                return "Não se aplica";
            default:
                return "Não se aplica";
        }
    }
    public static List<string> GetAllScaleClass()
    {
        return Enum.GetValues(typeof(ScaleClass))
                   .Cast<ScaleClass>()
                   .Select(sc => sc.ScaleClassToString())
                   .ToList();
    }
}
