using ChartCsApp.Modules;
using SqlKata;
using System.Security.Policy;
using System.Xml.Linq;

namespace ChartCsApp.Tables
{
    internal class Cereal : TableBase
    {
        private Cereal()
        {
            // 自由にインスタンスを作成させないよう対処
        }

        public override Dictionary<string, object> GetPrimaryKeys() => new Dictionary<string, object> { { PrimaryKeyColumn, id } };
        public override string GetTableName() => TableName;

        [Ignore]
        public static string PrimaryKeyColumn => "id";
        [Ignore]
        public static string TableName => "cereals";
        [Ignore]
        public static string AllTypeName => "未選択";

        public static Cereal Create()
        {
            var instance = new Cereal();

            instance.id = DatabaseAccess.GetNewID<Cereal>(TableName, "id", "", 1);

            return instance;
        }

        public static List<string> GetMfrs()
        {
            return DatabaseAccess.Distinct(TableName, "mfr");

        }

        public static List<string> GetTypes()
        {
            return DatabaseAccess.Distinct(TableName, "type");

        }

        public static List<Cereal> GetCereals(string mfr, string type)
        {
            var list = new List<KeyValuePair<string, object>>();
            if (mfr != string.Empty && mfr != AllTypeName)
                list.Add(new KeyValuePair<string, object>(nameof(Cereal.mfr), mfr));
            if (type != string.Empty && type != AllTypeName)
                list.Add(new KeyValuePair<string, object>(nameof(Cereal.type), type));
            return DatabaseAccess.GetCereals(list);
        }

        public string id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string mfr { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public double calories { get; set; } = 0.0;
        public double protein { get; set; } = 0.0;
        public double fat { get; set; } = 0.0;
        public double sodium { get; set; } = 0.0;
        public double fiber { get; set; } = 0.0;
        public double carbo { get; set; } = 0.0;
        public double sugars { get; set; } = 0.0;
        public double potass { get; set; } = 0.0;
        public double vitamins { get; set; } = 0.0;
        public double shelf { get; set; } = 0.0;
        public double weight { get; set; } = 0.0;
        public double cups { get; set; } = 0.0;
        public double rating { get; set; } = 0.0;
        public override bool deleted { get; set; } = false;
    }
}
