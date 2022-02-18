using MySql.Data.MySqlClient;
using System.Text.Json;

namespace cs_games.api
{
    public class API
    {
        private static MySqlConnection? _connection;
        private static MySqlConnection? Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                if (value == null)
                    return;

                _connection = value;
                _connection.Open();
            }
        }

        public static void ConnectToDB(string user, string password)
        {
            Connection = new MySqlConnection("server=localhost;userid=" + user + ";password=" + password + ";database=fha_cs");
        }

        public static JsonElement SQLQuery(string qry, string error_msg)
        {
            try
            {
                // System.Diagnostics.Debug.WriteLine(qry);
                MySqlDataReader reader = new MySqlCommand(qry, API.Connection).ExecuteReader();

                List<string> header = new List<string>();
                int qry_size = 0;

                for (int i = 0; ; i++)
                {
                    try
                    {
                        header.Add(reader.GetName(i));
                    }
                    catch
                    {
                        qry_size = i;
                        break;
                    }
                }

                string ret = "[";
                string div = "";
                while (reader.Read())
                {
                    ret += div + "{";
                    div = "";
                    for (int i = 0; i < qry_size; i++)
                    {
                        ret += div + "\"" + header[i] + "\"" + ": " + "\"" + reader.GetValue(i).ToString() + "\"";
                        div = ", ";
                    }
                    ret += "}";
                }
                ret += "]";

                reader.Close();

                return JsonDocument.Parse(ret).RootElement;

            }
            catch
            {
                throw new ArgumentException("\n" + error_msg + "\nSQL Query: " + qry);
            }
        }

        public static JsonElement SQLQueryAt(string qry, string error_msg)
        {
            return SQLQueryAt(qry, error_msg, 0);
        }

        public static JsonElement SQLQueryAt(string qry, string error_msg, int index)
        {
            JsonElement ret = SQLQuery(qry, error_msg);
            try
            {
                return ret[index];
            }
            catch
            {
                throw new IndexOutOfRangeException("\n" + error_msg + "\nSQL Query: " + qry + "\nSQL Index: " + index);
            }
        }

        public static void PrintSQLTable(string table)
        {
            PrintJsonArray(
                SQLQuery(
                    "SELECT * FROM " + table,
                    "Kein/e \"" + table + "\" vorhanden"
                ),
                GetFields(table)
            );
        }

        public static List<string> GetFields(string table)
        {
            List<string> ret = new List<string>();
            JsonElement fields = SQLQuery("DESCRIBE " + table, "Tabelle \"" + table + "\" nicht gefunden");
            for (int i = 0; i < fields.GetArrayLength(); i++)
                ret.Add(fields[i].GetProperty("Field").ToString());
            return ret;
        }

        public static void PrintJsonArray(JsonElement json_array, List<string> fields)
        {
            List<int> field_sizes = new List<int>();

            foreach (string field in fields)
            {
                int max_width = field.Length;
                for (int i = 0; i < json_array.GetArrayLength(); i++)
                    max_width = Math.Max(max_width, (json_array[i].TryGetProperty(field, out JsonElement tmp)) ? tmp.ToString().Length : 4);
                field_sizes.Add(max_width);
            }

            Action<List<int>> hline = field_sizes =>
            {
                System.Diagnostics.Debug.Write("+");
                foreach (int size in field_sizes)
                    System.Diagnostics.Debug.Write(new String('-', size + 2) + "+");
                System.Diagnostics.Debug.WriteLine("");
            };

            hline(field_sizes);
            int field_index = 0;
            System.Diagnostics.Debug.Write("| ");
            foreach (string field in fields)
                System.Diagnostics.Debug.Write(String.Format("{0,-" + field_sizes[field_index++] + "} | ", field));
            System.Diagnostics.Debug.WriteLine("");
            hline(field_sizes);
            for (int i = 0; i < json_array.GetArrayLength(); i++)
            {
                field_index = 0;
                System.Diagnostics.Debug.Write("| ");
                foreach (string field in fields)
                    System.Diagnostics.Debug.Write(String.Format("{0,-" + field_sizes[field_index++] + "} | ", (json_array[i].TryGetProperty(field, out JsonElement tmp)) ? tmp.ToString() : "NULL"));
                System.Diagnostics.Debug.WriteLine("");
            }
            hline(field_sizes);
        }
    }
}