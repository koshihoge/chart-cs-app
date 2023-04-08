using ChartCsApp.Tables;
using Npgsql;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Transactions;

namespace ChartCsApp.Modules
{
    internal class DatabaseAccess
    {
        private static string GetConnectionString()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host= "localhost",
                Username= "postgres",
                Password = "password",
                Database= "chart_cs_app",
                Port= 5432
            };
            return builder.ToString();
        }

        private static QueryFactory? _database = null;

        private static QueryFactory GetQuery()
        {
            if (_database == null)
            {
                _database =
                    new QueryFactory(
                        new Npgsql.NpgsqlConnection(GetConnectionString()),
                        new PostgresCompiler());
            }
            return _database;
        }
        private static Dictionary<Guid, QueryFactory> queryFactories = new Dictionary<Guid, QueryFactory>();

        public static void Initialize()
        {
            ConfigureTransactionTimeoutCore(TimeSpan.FromSeconds(0));   // 無期限
        }

        static void ConfigureTransactionTimeoutCore(TimeSpan timeout)
        {
            TransactionManager.DefaultTimeout = timeout;
            TransactionManager.MaximumTimeout = timeout;
        }


        private static System.Data.IDbTransaction? dbTransaction = null;
        private static bool Replace(TableBase value)
        {
            try
            {
                string tableName = value.GetTableName();               
                var query = GetQuery().Query(tableName).Where(value.GetPrimaryKeys()).Get();
                if (0 < query.Count())
                {
                    GetQuery().Query(tableName).Where(value.GetPrimaryKeys()).Update(value, dbTransaction);
                }
                else
                {
                    GetQuery().Query(tableName).Insert(value, dbTransaction);
                }

                // キャッシュとデータベースの内容が変わるのでキャッシュを削除
                DeleteQueryCache();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool SetValue(TableBase value)
        {
            return Replace(value);
        }

        public static bool DeleteValue(TableBase value)
        {
            try
            {
                string tableName = value.GetTableName();

                var query = GetQuery().Query(tableName).Where(value.GetPrimaryKeys()).Get();
                if (0 < query.Count())
                {
                    // 削除は削除フラグを立ててUPDATE
                    value.deleted = true;
                    GetQuery().Query(tableName).Where(value.GetPrimaryKeys()).Update(value);

                    // キャッシュとデータベースの内容が変わるのでキャッシュを削除
                    DeleteQueryCache();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static void BeginTransaction(QueryFactory query)
        {
            if (dbTransaction == null)
            {
                query.Connection.Open();
                dbTransaction = query.Connection.BeginTransaction();
            }
        }

        public static void BeginTransaction()
        {
            BeginTransaction(GetQuery());
        }

        private static void RollbackTransaction(QueryFactory query)
        {
            if (dbTransaction == null)
                return;
            dbTransaction.Rollback();
            dbTransaction.Dispose();
            dbTransaction = null;
            query.Connection.Close();
        }

        public static void RollbackTransaction()
        {
            RollbackTransaction(GetQuery());
        }

        private static void CommitTransaction(QueryFactory query)
        {
            if (dbTransaction == null)
                return;

            try
            {
                dbTransaction.Commit();
                dbTransaction.Dispose();
                dbTransaction = null;
                query.Connection.Close();
            }
            catch(Exception ex)
            {
                RollbackTransaction(query);
            }
        }

        public static void CommitTransaction()
        {
            CommitTransaction(GetQuery());
        }


        private static readonly Guid databaseID = Guid.NewGuid();
        private static Dictionary<Guid, Dictionary<string, object>> queryCache = new Dictionary<Guid, Dictionary<string, object>>();

        private static void DeleteQueryCache()
        {
            queryCache.Remove(databaseID);
        }

        private static Type GetValue<Type>(Query query, Func<Query, Type> doQuery)
        {
            var compiler = new SqliteCompiler();
            var result = compiler.Compile(query);
            string sql = result.ToString();

            if (queryCache.TryGetValue(databaseID, out Dictionary<string, object>? queryValue))
            {
                if (queryValue.ContainsKey(sql))
                {
                    return (Type)queryValue[sql];
                }
            }
            else
            {
                queryValue = new Dictionary<string, object>();
                queryCache.Add(databaseID, queryValue);
            }

            // キャッシュヒットしなかった場合の処理
            Type value = doQuery(query);
            queryValue[sql] = value!;

            return value;
        }

        public static List<Cereal> GetCereals(IEnumerable<KeyValuePair<string, object>> pairs, bool isValidOnly = false)
        {
            var query = isValidOnly ? GetQuery().Query(Cereal.TableName).Where("deleted", false) : GetQuery().Query(Cereal.TableName);
            if(pairs.Count() != 0)
                query.Where(pairs);
            return GetValue(query, (q) => query.Get<Cereal>().ToList());
        }

        public static Cereal? GetCereal(string id)
        {
            var query = GetQuery().Query(Cereal.TableName).Where(Cereal.PrimaryKeyColumn, id).Limit(1);
            return GetValue(query, (q) => q.Get<Cereal>().FirstOrDefault());
        }

        public static List<string> Distinct(string tableName, string columnName)
        {
            return GetQuery().Query(tableName).Select(columnName).Distinct().Get<string>().ToList();
        }

        public static string GetNewID<Type>(string tableName, string field, string prefix, int digits)
        {
            int num = 0;

            // 常に新しいIDが欲しいのでキャッシュしてはいけない
            var record = GetQuery().Query(tableName).OrderByDesc(field).Limit(1).Get<Type>().ToList();
            if(0 < record.Count())
            {
                // プロパティ情報の取得
                var property = typeof(Type).GetProperty(field);

                // インスタンスの値を取得
                var _id = property!.GetValue(record[0]) as string;

                if (_id != null)
                {
                    int.TryParse(_id.Replace(prefix, ""), out num);
                }
            }

            return string.Format($"{prefix}{{0:D{digits}}}", num+1);
        }
    }
}
