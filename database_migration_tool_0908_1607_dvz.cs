// 代码生成时间: 2025-09-08 16:07:32
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

// 定义数据库迁移工具组件
public partial class DatabaseMigrationTool : ComponentBase
{
    [Inject]
    private IServiceProvider ServiceProvider { get; set; }

    private bool IsMigrationRunning { get; set; }
    private string MigrationStatus { get; set; } = "Ready";

    // 执行数据库迁移的方法
    private async Task MigrateDatabaseAsync()
    {
        IsMigrationRunning = true;
        MigrationStatus = "Running...";

        try
        {
            // 获取数据库上下文
            var dbContext = ServiceProvider.GetRequiredService<MyDbContext>();
            // 执行迁移
            await dbContext.Database.MigrateAsync();
            MigrationStatus = "Migration Successful";
        }
        catch (Exception ex)
        {
            // 错误处理
            MigrationStatus = $"Error: {ex.Message}";
        }
        finally
        {
            IsMigrationRunning = false;
        }
    }

    // 重置数据库迁移的方法
    private async Task ResetDatabaseAsync()
    {
        IsMigrationRunning = true;
        MigrationStatus = "Resetting...";

        try
        {
            // 获取数据库上下文
            var dbContext = ServiceProvider.GetRequiredService<MyDbContext>();
            // 删除所有迁移文件
            var migrationFiles = Directory.GetFiles(dbContext.GetService<MySqliteDbContext>().Database.GetDbConnection().DataSource, "*.sql");
            foreach (var file in migrationFiles)
            {
                File.Delete(file);
            }
            // 重置数据库
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
            MigrationStatus = "Database Reset Successful";
        }
        catch (Exception ex)
        {
            // 错误处理
            MigrationStatus = $"Error: {ex.Message}";
        }
        finally
        {
            IsMigrationRunning = false;
        }
    }
}

// 示例数据库上下文
public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    // 定义数据库模型
    public DbSet<MigrationModel> MigrationModels { get; set; }
}

// 定义迁移模型
public class MigrationModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}