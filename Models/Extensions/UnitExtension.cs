using ReportApp.Models.Enums;

namespace ReportApp.Models.Extensions;

public static class UnitExtension
{
    public static string UnitToString(this Unit unit)
    {
        switch (unit)
        {
            case Unit.G:
                return "g";
            case Unit.Kg:
                return "kg";
            case Unit.Mg:
                return "mg";           
            default:
                return "";
        }
    }
    public static List<string> GetAllUnit()
    {
        return Enum.GetValues(typeof(Unit))
            .Cast<Unit>()
            .Select(u => u.UnitToString())
            .ToList();
    } 

}
