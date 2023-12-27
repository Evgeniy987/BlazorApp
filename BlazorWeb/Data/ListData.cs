namespace BlazorWeb.Data;

public static class ListData
{

    public static readonly Dictionary<string, string> Roles = new()
{
    {"Admin", "Админ" },
    {"Logist", "Логист" },
    {"Manager", "Оформитель" },
    {"Account", "Бухгалтер" },
    {"Customer", "Заказчик" },
    {"Сontractor", "Исполнитель" }
};
}
