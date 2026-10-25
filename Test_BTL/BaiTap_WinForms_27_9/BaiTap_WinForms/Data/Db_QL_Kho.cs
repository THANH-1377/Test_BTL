using System.Data;
using Microsoft.Data.SqlClient;

namespace BaiTap_WinForms.Data
{
    /// <summary>Lớp mỏng bọc DatabaseHelper, dùng cho module Kho.</summary>
    public static class Db_QL_Kho
    {
        public static SqlParameter P(string name, object? value)
            => new(name, value ?? DBNull.Value);

        public static SqlConnection Open()
        {
            var c = DatabaseHelper.GetConnection();
            c.Open();
            return c;
        }

        // ---------- Không transaction ----------
        public static DataTable Query(string sql, params SqlParameter[] ps)
            => DatabaseHelper.ExecuteQuery(sql, ps);

        public static int Exec(string sql, params SqlParameter[] ps)
            => DatabaseHelper.ExecuteNonQuery(sql, ps);

        public static object? Scalar(string sql, params SqlParameter[] ps)
        {
            var r = DatabaseHelper.ExecuteScalar(sql, ps);
            return r == DBNull.Value ? null : r;
        }

        // ---------- Dùng chung connection + transaction ----------
        public static DataTable Query(SqlConnection c, SqlTransaction? t, string sql, params SqlParameter[] ps)
            => DatabaseHelper.ExecuteQuery(sql, ps, c, t);

        public static int Exec(SqlConnection c, SqlTransaction? t, string sql, params SqlParameter[] ps)
            => DatabaseHelper.ExecuteNonQuery(sql, ps, c, t);

        public static object? Scalar(SqlConnection c, SqlTransaction? t, string sql, params SqlParameter[] ps)
        {
            var r = DatabaseHelper.ExecuteScalar(sql, ps, c, t);
            return r == DBNull.Value ? null : r;
        }
    }
}