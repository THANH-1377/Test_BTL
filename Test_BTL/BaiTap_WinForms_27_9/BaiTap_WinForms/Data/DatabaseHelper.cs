using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;

namespace BaiTap_WinForms.Data
{
    public static class DatabaseHelper
    {
        // Connection string template. Change instance name in one place if needed.
        public static string ConnectionString { get; set; } =
            "Server=LAPTOP-APPVC4J1\\SQLEXPRESS;Database=QuanLyBanhMi;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static bool TestConnection(out string error)
        {
            error = string.Empty;
            try
            {
                using var conn = GetConnection();
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand(query, conn);
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static int ExecuteNonQuery(string query, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text)
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand(query, conn)
            {
                CommandType = commandType
            };
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        // Overload to execute non-query within an existing connection/transaction (useful for transactions)
        public static int ExecuteNonQuery(string query, SqlParameter[]? parameters, SqlConnection conn, SqlTransaction? transaction = null, CommandType commandType = CommandType.Text)
        {
            using var cmd = new SqlCommand(query, conn)
            {
                CommandType = commandType,
                Transaction = transaction
            };
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            var shouldClose = conn.State == ConnectionState.Closed;
            if (shouldClose) conn.Open();
            try
            {
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                if (shouldClose) conn.Close();
            }
        }

        public static object? ExecuteScalar(string query, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text)
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand(query, conn)
            {
                CommandType = commandType
            };
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            conn.Open();
            return cmd.ExecuteScalar();
        }

        // Overload to execute query within an existing connection/transaction
        public static DataTable ExecuteQuery(string query, SqlParameter[]? parameters, SqlConnection conn, SqlTransaction? transaction = null, CommandType commandType = CommandType.Text)
        {
            using var cmd = new SqlCommand(query, conn)
            {
                CommandType = commandType,
                Transaction = transaction
            };
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        // Overload to execute scalar within an existing connection/transaction
        public static object? ExecuteScalar(string query, SqlParameter[]? parameters, SqlConnection conn, SqlTransaction? transaction = null, CommandType commandType = CommandType.Text)
        {
            using var cmd = new SqlCommand(query, conn)
            {
                CommandType = commandType,
                Transaction = transaction
            };
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            var shouldClose = conn.State == ConnectionState.Closed;
            if (shouldClose) conn.Open();
            try
            {
                return cmd.ExecuteScalar();
            }
            finally
            {
                if (shouldClose) conn.Close();
            }
        }

        /// <summary>
        /// Băm mật khẩu bằng SHA-256, trả về chuỗi hex in hoa (khớp với cột TaiKhoan.MatKhauBam,
        /// tức CONVERT(NVARCHAR(255), HASHBYTES('SHA2_256', ...), 2) trong SQL Server)
        /// </summary>
        public static string HashPasswordSHA256(string password)
        {
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }


        ///////********************************************************(Thanh--Kho)
        ///Tạo SqlParameter với giá trị null được chuyển thành DBNull.Value
        public static SqlParameter P(string name, object? value)
        => new(name, value ?? DBNull.Value);
    }
}
