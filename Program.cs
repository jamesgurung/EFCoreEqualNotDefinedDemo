using Microsoft.EntityFrameworkCore;

using var db = new BloggingContext();

db.Add(new Blog { Id = 1 }); // InvalidOperationException: The binary operator Equal is not defined for the types 'System.Int32' and 'System.Nullable`1[System.Int32]'.


public class BloggingContext : DbContext
{
  public DbSet<Blog> Blogs { get; set; }

  public BloggingContext()
  {
    Database.OpenConnection();
    Database.EnsureDeleted();
    Database.EnsureCreated();
  }
  
  protected override void OnConfiguring(DbContextOptionsBuilder options) =>
    options
      .UseSqlite("DataSource=file::memory:")
      .UseModel(EFCoreEqualNotDefinedDemo.BloggingContextModel.Instance); // Comment this line to fix the error.
  
  public override void Dispose()
  {
    Database.CloseConnection();
    base.Dispose();
  }
}

public class Blog
{
  public int? Id { get; set; }
}