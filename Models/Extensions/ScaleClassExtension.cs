
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
            default:
                return "Não se aplica";
        }
    }
}
