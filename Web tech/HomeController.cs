using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Web_tech
{
    public class HomeController : Controller
    {
        // 注入MySqlConnection对象
        private readonly MySqlConnection _connection;

        public HomeController(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IActionResult> Index()
        {
            // 打开连接
            await _connection.OpenAsync();

            // 创建一个命令对象
            using var command = new MySqlCommand("SELECT 'Hello World'", _connection);

            // 执行查询并获取结果
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                // 将结果赋值给ViewData，用于在视图中显示
                ViewData["Message"] = reader.GetString(0);
            }

            // 关闭连接
            await _connection.CloseAsync();

            // 返回视图
            return View();
        }
    }
}